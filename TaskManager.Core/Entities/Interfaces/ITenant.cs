using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManager.Core.Entities.Interfaces
{
    public interface ITenant
    {
        public int OrganizationId { get; set; }
    }
}
