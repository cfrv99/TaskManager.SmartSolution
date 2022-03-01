using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManager.Core.Responses
{
    public class PagedListResult<T> where T : IDto
    {
        public List<T> ListResult { get; set; }
        public int TotalCount { get; set; }
    }
}
