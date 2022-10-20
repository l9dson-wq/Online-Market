using Microsoft.AspNetCore.Http;
using StoackApp.Core.Application.Helpers;
using StoackApp.Core.Application.ViewModels.Users;

namespace WebApp.NewStockApp.Middlewares
{
    public class ValidateUserSession
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ValidateUserSession(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public bool HasUser()
        {
            UserViewModel userViewModel = _httpContextAccessor.HttpContext.Session.Get<UserViewModel>("user");
            if(userViewModel == null)
            {
                return false; 
            }

            return true; 
        }
    }
}
