using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using WarhammerCore.Abstract.Exceptions;
using WarhammerCore.Abstract.Interfaces;
using WarhammerCore.WebApi.Models.Enums;
using WarhammerCore.WebApi.Models.Request;
using WarhammerCore.WebApi.Models.Response;

namespace WarhammerCore.WebApi.Controllers
{
    public class UserController : ApiControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService ?? throw new ArgumentNullException(nameof(userService));
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<ActionResult<SigninResponse>> Signin(SigninRequest request)
        {
            var token = await _userService.SignInAsync(request.Email, request.Password);

            if (token == null)
            {
                return NotFound(new ErrorResponse(ErrorCode.UserNotFound));
            }
            return new SigninResponse { Email = request.Email, Token = token.Token };
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPost]
        public async Task<ActionResult> SignedIn()
        {
            return Ok();
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<ActionResult<SigninResponse>> Signup(SigninRequest request)
        {
            try
            {
                await _userService.SignUpAsync(request.Email, request.Password);
            }
            catch (AppBusinessException e)
            {
                if (e.ErrorCode == "EmailAreadyExists") return Conflict();
            }
            catch
            {
                return BadRequest();
            }

            return await Signin(request);
        }
    }
}