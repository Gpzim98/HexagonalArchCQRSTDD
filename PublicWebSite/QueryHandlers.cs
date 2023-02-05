using MediatR;

namespace PublicWebSite
{
    public class GetCustomerHandler : IRequestHandler<GetCustomerQuery, CustomerResponse>
    {
        public Task<CustomerResponse> Handle(GetCustomerQuery query, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
