using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManager.Core.Responses
{
    public interface ISorting : IDto
    {
        public string Sorting { get; set; }
    }
}
