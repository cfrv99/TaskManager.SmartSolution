using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManager.Core.Responses;

namespace TaskManager.BLL.Services.Dto
{
    public class AssignUserTaskRequest : IDto
    {
        public int TaskId { get; set; }
        public List<int> UserIds { get; set; }
    }
}
