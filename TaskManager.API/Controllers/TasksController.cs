using Microsoft.AspNetCore.Authorization;
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
    [Authorize]
    public class TasksController : ApiController
    {
        private readonly ITaskService _taskService;

        public TasksController(ITaskService taskService)
        {
            _taskService = taskService;
        }

        [HttpPost("create-task")]
        public async Task<IActionResult> CreateTask(CreateTaskRequest request)
        {
            var result = await _taskService.CreateTask(request);
            return Response(result);
        }
        [HttpGet("all-tasks")]
        public async Task<IActionResult> GetAll()
        {
            return Ok();
        }
        [HttpPost("assign-task-to-users")]
        public async Task<IActionResult> AssingTaskToUser(AssignUserTaskRequest request)
        {
            var result = await _taskService.AssignUser(request);
            return Response(result);
        }
    }
}
