using Domain.Exceptions;
using MediatR;

namespace PublicWebSite
{
    public class CreateCustomerCommandHandler : IRequestHandler<CreateCustomerCommand, CustomerResponse>
    {
        private ICreateCustomer _createCustomer;
        public CreateCustomerCommandHandler(ICreateCustomer createCustomer)
        {
            _createCustomer = createCustomer;
        }
        public async Task<CustomerResponse> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var customer = CustomerDTO.MapToDomain(request.CustomerDTO);
                customer.IsValid();

                customer.Id = await _createCustomer.CreateCustomerAsync(request.CustomerDTO);

                return new CustomerResponse()
                {
                    Success = true,
                    Message = "Customer Created Successfully",
                    Data = CustomerDTO.MapToDTO(customer)
                };
            }
            catch (InvalidCustomerDocumentException ex)
            {
                return new CustomerResponse
                {
                    ErrorCode = ErrorCodes.INVALID_PERSON_ID,
                    Message = "Invalid Customer Document",
                    Success = false
                };
            }
            catch (MissingRequiredInformationException ex)
            {
                return new CustomerResponse
                {
                    ErrorCode = ErrorCodes.MISSING_REQUIRED_INFORMATION,
                    Message = "User is missing required information",
                    Success = false
                };
            }
            catch (InvalidEmailException ex)
            {
                return new CustomerResponse
                {
                    ErrorCode = ErrorCodes.INVALID_EMAIL,
                    Message = "User email is not valid",
                    Success = false
                };
            }
            catch (Exception e)
            {
                throw;
            }
        }
    }
}
