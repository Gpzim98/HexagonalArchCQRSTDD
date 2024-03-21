using MediatR;

namespace PublicWebSite
{
    public class CreateCustomerCommand : IRequest<CustomerResponse>
    {
        public CustomerDTO CustomerDTO { get; set; }
    }
}