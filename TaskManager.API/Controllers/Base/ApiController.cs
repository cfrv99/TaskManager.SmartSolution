using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskManager.Core.Responses;

namespace TaskManager.API.Controllers.Base
{
    [ApiController]
    [Route("api/[controller]")]
    public class ApiController : ControllerBase
    {
        protected IActionResult Response<T>(ApiResponse<T> response) where T : IDto
        {
            if (!string.IsNullOrWhiteSpace(response.ErrorMessage))
            {
                if (response.ErrorMessage.ToLower().Contains("exception"))
                {
                    return BadRequest("Error: " + response.ErrorMessage);
                }
                else
                {
                    return NotFound(response.ErrorMessage);
                }
            }
            else
            {
                return Ok(response.Data);
            }
        }
    }
}
