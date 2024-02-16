using Microsoft.AspNetCore.Components;

namespace PoolMS.UI.WebAssembly.Auth
{
    public class AuthorizeAdmin
    {
        private readonly AuthService _authService;
        private readonly NavigationManager _navigationManager;

        public AuthorizeAdmin(AuthService authService, NavigationManager navigationManager)
        {
            _authService = authService;
            _navigationManager = navigationManager;
        }

        public async Task<bool> CheckUserRole()
        {
            var userRole = await _authService.GetUserRole();
            if (userRole == null)
            {
                _navigationManager.NavigateTo("/login");
                return false;
            }
            else if (userRole != "Admin")
            {
                _navigationManager.NavigateTo("/forbidden");
                return false;
            }

            return true;
        }
    }
}
