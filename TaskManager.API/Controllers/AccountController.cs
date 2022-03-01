using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskManager.API.Controllers.Base;
using TaskManager.BLL.Services;
using TaskManager.BLL.Services.Dto;

namespace TaskManager.API.Controllers
{
    public class AccountController : ApiController
    {
        private readonly IUserService _userService;

        public AccountController(IUserService userService)
        {
            _userService = userService;
        }
        [HttpPost("signin")]
        public async Task<IActionResult> Login(LoginRequest request)
        {
            var result = await _userService.Login(request);
            return Response(result);
        }
        [HttpPost("sign-up-as-admin")]
        public async Task<IActionResult> SignUpAsAdmin(SignUpAsAdminRequest request)
        {
            var result = await _userService.SignInAsAdmin(request);
            return Response(result);
        }
    }
}
