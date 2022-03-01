using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManager.Core.Entities.Interfaces;

namespace TaskManager.Core.Entities
{
    public class EntityBase : IEntity
    {
        public int Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public int Creator { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public int? Modifier { get; set; }
    }
}
