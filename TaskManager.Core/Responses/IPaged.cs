using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManager.Core.Responses
{
    public interface IPaged : IDto
    {
        public int CurrentPage { get; set; }
        public int Limit { get; set; }
    }
}
