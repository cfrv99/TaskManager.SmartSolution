using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManager.Core.Responses;

namespace TaskManager.Infastructure.Auth.Models
{
    public class LoginResult : IDto
    {
        public string AccessToken { get; set; }
        public DateTime Expired { get; set; }
    }
}
