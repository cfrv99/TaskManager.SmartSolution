using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManager.Core.Responses;

namespace TaskManager.BLL.Services.Dto
{
    public class CreateTaskRequest : IDto
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public TaskStatus Status { get; set; }
        public DateTime DeadLine { get; set; }
    }
}
