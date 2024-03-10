using MediatR;
using Microsoft.AspNetCore.Mvc;
using PublicWebSite;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CustomerController : ControllerBase
    {
        private readonly IMediator _mediator;
        public CustomerController(IMediator mediator)
        {
            _mediator= mediator;
        }

        public async Task<ActionResult<CustomerResponse>> CreateCustomer(
            CustomerDTO customer
            )
        {
            var command = new CreateCustomerCommand()
            { 
                CustomerDTO = customer
            };

            var res = await _mediator.Send(command);

            if (res.Success) return Created("", res.Data);

            else if (res.ErrorCode == ErrorCodes.INVALID_PERSON_ID)
            {
                res.Message = "Please verity that your document is correct...";
                return BadRequest(res);
            }
            else if (res.ErrorCode == ErrorCodes.MISSING_REQUIRED_INFORMATION)
            {
                return BadRequest(res);
            }

            return BadRequest(500);
        }
    }
}
