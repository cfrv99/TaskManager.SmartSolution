using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManager.Core.Entities.Interfaces;

namespace TaskManager.DAL.Entities
{
    public class AppUser : IdentityUser<int>, ITenant
    {
        public int OrganizationId { get; set; }
    }
}
