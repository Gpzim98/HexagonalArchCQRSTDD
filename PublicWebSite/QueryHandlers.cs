using Domain;
using MediatR;

namespace PublicWebSite
{
    public class GetCustomerHandler : IRequestHandler<GetCustomerQuery, CustomerResponse>
    {
        private readonly IProvideCustomerData _customerDataProvider;
        public GetCustomerHandler(IProvideCustomerData customerDataProvider)
        {
            _customerDataProvider = customerDataProvider;
        }
        public async Task<CustomerResponse> Handle(GetCustomerQuery query, CancellationToken cancellationToken)
        {
            try
            {
                ValidateUserPermissions(query.User);
                var customer = await _customerDataProvider.GetCustomerData(query.Id);
                return new CustomerResponse
                {
                    Success = true,
                    Message = "Data fetched successfully",
                    Data = CustomerDTO.MapToDTO(customer)
                };
            }
            catch (UserDoesNotHavePermissionsToSeeTheRecord)
            {
                return new CustomerResponse()
                {
                    Success = false,
                    Message = "The user does not have permissions to query the record",
                    ErrorCode = ErrorCodes.USER_DOES_NOT_HAVE_PERMISSION_TO_QUERY_RECORD
                };
            }
            catch (Exception ex)
            {
                return new CustomerResponse()
                {
                    Success = false,
                    Message = ex.Message,
                    ErrorCode = ErrorCodes.UNKNOWN
                };
            }
        }

        public void ValidateUserPermissions(UserDTO user)
        {
            if (!user.Roles.Contains(
                Roles.Manager.ToString()))
            {
                throw new UserDoesNotHavePermissionsToSeeTheRecord();
            }
        }
    }
}
