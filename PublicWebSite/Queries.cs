using Domain;
using MediatR;

namespace PublicWebSite
{
    public class GetCustomerQuery : IRequest<CustomerResponse>
    {
        public int Id { get; set; }
        public User User { get; set; }
    }
}
