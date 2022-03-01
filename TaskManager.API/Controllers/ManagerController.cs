using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskManager.API.Controllers.Base;
using TaskManager.BLL.Services;
using TaskManager.Infastructure.Auth.Models;

namespace TaskManager.API.Controllers
{
    [Authorize(Roles = "Admin")]
    public class ManagerController : ApiController
    {
        private readonly IUserService _userService;

        public ManagerController(IUserService userService)
        {
            _userService = userService;
        }
        [HttpPost("create-user-staff")]
        public async Task<IActionResult> CreateUser(SignUpRequest request)
        {
            var result = await _userService.CreateUserAsStaff(request);
            return Response(result);
        }
    }
}
