using Domain;
using MediatR;

namespace PublicWebSite
{
    public class GetCustomerQuery : IRequest<CustomerResponse>
    {
        public int Id { get; set; }
        public UserDTO User { get; set; }
    }
}
