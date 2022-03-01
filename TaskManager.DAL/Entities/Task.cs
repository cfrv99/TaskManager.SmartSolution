using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManager.Core.Entities;
using TaskManager.Core.Entities.Interfaces;

namespace TaskManager.DAL.Entities
{
    public class Task : EntityBase, ITenant
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public int OrganizationId { get; set; }
        public DateTime DeadLine { get; set; }
        public TaskStatus Status { get; set; }
    }
}
