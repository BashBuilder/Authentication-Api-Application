using ApplicationApi.Application.DTO;
using ApplicationApi.Application.Interfaces;
using Ecommerce.SharedLibrary.Logs;
using Ecommerce.SharedLibrary.Responses;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace ApplicationApi.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController(IUser userInterface) : ControllerBase
    {
        [HttpPost("register-user")]
        public async Task<ActionResult<Response>> RegisterUser(AppUserDTO payload)
        {
            try
            {
                if (!ModelState.IsValid) return BadRequest(ModelState);
                var result = await userInterface.Register(payload);

                return result.Flag ? Ok(result) : BadRequest(result);

            }catch(Exception ex)
            {
                LogException.LogExceptions(ex);
                return new Response(false, "Error while registering user");

            }
        }

        [HttpPost("login-user")]
        public async Task<ActionResult<Response>> LoginUser(LoginDTO payload)
        {
            try
            {
                if (!ModelState.IsValid) return BadRequest(ModelState);
                var result = await userInterface.Login(payload);
                return result.Flag ? Ok(result) : BadRequest(result);
            }catch(Exception ex)
            {
                LogException.LogExceptions(ex);
                return new Response(false, "Error while loging user in");
            }
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<GetUserDTO>> GetUserDetails(int id)
        {
            try
            {
                if ( id <= 0) return BadRequest("Invalid user details");
                var user = await userInterface.GetUser(id);
                return user is not null ? Ok(user) : NotFound("user not found");
            }catch(Exception ex)
            {
                LogException.LogExceptions(ex);
                return  BadRequest("Error while getting user detials");
            }
        }

    }
}
