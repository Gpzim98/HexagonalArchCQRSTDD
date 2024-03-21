using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PublicWebSite;
using System.Security.Claims;

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

        [Authorize]
        [HttpGet]
        public async Task<ActionResult<CustomerResponse>> GetCustomerData(
            int customerId)
        {
            var userDto = PopulateUserPermissions(User);

            var query = new GetCustomerQuery
            {
                Id = customerId,
                User = userDto
            };

            var res = await _mediator.Send(query);

            if (res.Success)
            { 
                return Ok(res);
            }
            else if (res.ErrorCode == ErrorCodes.USER_DOES_NOT_HAVE_PERMISSION_TO_QUERY_RECORD)
            {
                return Unauthorized(res.Message);
            }

            return BadRequest(500);
        }

        [HttpPost]
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

        public static UserDTO PopulateUserPermissions(ClaimsPrincipal userClaims)
        {
            var user = new UserDTO
            {
                Username = userClaims.FindFirst(ClaimTypes.Name)?.Value,
                Permissions = new List<string>(),
                Roles = new List<string>()
            };

            var roleClaims = userClaims.FindAll(ClaimTypes.Role).Select(claim => claim.Value);
            user.Roles.AddRange(roleClaims);

            var permissionClaims = userClaims.FindAll("permission").Select(claim => claim.Value);
            user.Permissions.AddRange(permissionClaims);
            return user;
        }
    }
}
