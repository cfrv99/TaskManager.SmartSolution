using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace TaskManager.Infastructure.Helpers
{
    public interface ICurrentUserService
    {
        ClaimsPrincipal GetCurrentUser();
        int GetCurrentUserId();
    }
    public class CurrentUserService : ICurrentUserService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CurrentUserService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }
        public ClaimsPrincipal GetCurrentUser()
        {
            var currentUser = _httpContextAccessor.HttpContext.User;
            return currentUser;
        }

        public int GetCurrentUserId()
        {
            var currentUserId = _httpContextAccessor.HttpContext.User?.FindFirst(i => i.Type == ClaimTypes.NameIdentifier)?.Value;
            return Convert.ToInt32(currentUserId);
        }
    }
}
