using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WarhammerCore.Abstract.Interfaces;
using WarhammerCore.Abstract.Models;
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


        [HttpPost]
        public async Task<ActionResult<SigninResponse>> Signin(SigninRequest request)
        {
            var token = await _userService.SignInAsync(request.Email, request.Password);

            return new SigninResponse { Email = request.Email, Token = token.Token };
        }
    }
}
