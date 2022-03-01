using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManager.DAL.Entities
{
    public class TaskAssignedUsers
    {
        public int UserId { get; set; }
        public int TaskId { get; set; }
        public AppUser User { get; set; }
        public Task Task { get; set; }
    }
}
