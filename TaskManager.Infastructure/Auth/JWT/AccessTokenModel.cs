using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManager.Infastructure.Auth.JWT
{
    public class AccessTokenModel
    {
        public string AccessToken { get; set; }
        public DateTime Expired { get; set; }
    }
}
