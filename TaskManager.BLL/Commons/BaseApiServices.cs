using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using TaskManager.Infastructure.Helpers;

namespace TaskManager.BLL.Commons
{
    public class BaseApiServices
    {
        private readonly IServiceProvider serviceProvider;

        public BaseApiServices(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }

        public ClaimsPrincipal GetCurrentUser()
        {
            var currentUserService = (ICurrentUserService)serviceProvider.GetService(typeof(ICurrentUserService));
            return currentUserService.GetCurrentUser();
        }

        public Task FileUpload(List<IFormFile> files)
        {
            throw new NotImplementedException();
        }

        public int GetCurrentTenantId()
        {
            return Convert.ToInt32(GetCurrentUser()?.FindFirst(i => i.Type == "Tenant")?.Value);
        }
    }
}
