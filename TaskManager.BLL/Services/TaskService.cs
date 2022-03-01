using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManager.BLL.Commons;
using TaskManager.BLL.Services.Dto;
using TaskManager.Core.Repository;
using TaskManager.Core.Responses;
using TaskManager.DAL.Entities;

namespace TaskManager.BLL.Services
{
    public interface ITaskService
    {
        Task<ApiResponse<TaskCreateResult>> CreateTask(CreateTaskRequest request);
        Task<ApiResponse<TaskAssingResult>> AssignUser(AssignUserTaskRequest request);
    }
    public class TaskService : BaseApiServices, ITaskService
    {
        private readonly IRepository<DAL.Entities.Task> _taskRepository;
        private readonly IRepository<TaskAssignedUsers> _assignedUserRepository;

        public TaskService(
            IServiceProvider serviceProvider,
            IRepository<TaskManager.DAL.Entities.Task> taskRepository,
            IRepository<TaskAssignedUsers> assignedUserRepository) : base(serviceProvider)
        {
            _taskRepository = taskRepository;
            _assignedUserRepository = assignedUserRepository;
        }

        public async Task<ApiResponse<TaskAssingResult>> AssignUser(AssignUserTaskRequest request)
        {
            ApiResponse<TaskAssingResult> response = null;
            try
            {
                List<TaskAssignedUsers> assignedUsers = new List<TaskAssignedUsers>();
                foreach (var userId in request.UserIds)
                {
                    TaskAssignedUsers assignedUser = new TaskAssignedUsers();
                    assignedUser.TaskId = request.TaskId;
                    assignedUser.UserId = userId;
                    assignedUsers.Add(assignedUser);
                }
                await _assignedUserRepository.AddRange(assignedUsers);
                await _assignedUserRepository.Commit();
                response = new ApiResponse<TaskAssingResult>(new TaskAssingResult());
            }
            catch (Exception ex)
            {
                response = new ApiResponse<TaskAssingResult>(ex.Message);
            }
            return response;
        }

        public async Task<ApiResponse<TaskCreateResult>> CreateTask(CreateTaskRequest request)
        {
            ApiResponse<TaskCreateResult> response = null;
            try
            {
                await _taskRepository.Add(new DAL.Entities.Task
                {
                    DeadLine = request.DeadLine,
                    Title = request.Title,
                    Status = request.Status,
                    Description = request.Description,
                    OrganizationId = GetCurrentTenantId()
                });
                await _taskRepository.Commit();
                response = new ApiResponse<TaskCreateResult>(new TaskCreateResult());
            }
            catch (Exception ex)
            {
                response = new ApiResponse<TaskCreateResult>(ex.Message);
            }
            return response;
        }
    }

}
