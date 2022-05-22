using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TicketSystemApi.Persistance.Data;
using TicketSystemApi.Persistance.Interfaces;

namespace TicketSystemApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {

        private readonly IUserRepository _Authservices;

        public UserController(IUserRepository outhservices)
        {
            _Authservices = outhservices;
        }

        [HttpPost("Login")]
        public async Task<IActionResult> GetToken([FromBody] TokenRequestModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _Authservices.GetTokenAsync(model);
            return Ok(result);
        }

        [HttpPost("Register")]
        [Authorize(Policy = "RequireAdministratorRole")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> Registermodel(RegisterModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _Authservices.RegisterModel(model);

            return Ok(result);
        }
    }
}
