using PharmacyManagementSystem.Application.DTOs.UserDTOs;
using System.Text.Json;

namespace PharmacyManagementSystem.WebAppMVC.Helpers
{
    public class SessionHelper
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private ISession Session => _httpContextAccessor.HttpContext!.Session;

        private const string TokenKey = "AuthToken";
        private const string UserKey = "UserInfo";

        public SessionHelper(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public void SetAuthSession(AuthResponseDto authResponse)
        {
            Session.SetString(TokenKey, authResponse.Token);

            var userInfo = new
            {
                authResponse.UserId,
                authResponse.UserName,
                authResponse.FullName,
                authResponse.Email,
                authResponse.Role,
                authResponse.IsAuthenticated
            };

            var userInfoJson = JsonSerializer.Serialize(userInfo);
            Session.SetString(UserKey, userInfoJson);
        }

        public string? GetToken()
        {
            return Session.GetString(TokenKey);
        }

        public AuthResponseDto? GetUserInfo()
        {
            var userInfoJson = Session.GetString(UserKey);
            if (string.IsNullOrEmpty(userInfoJson))
                return null;

            try
            {
                var userInfo = JsonSerializer.Deserialize<AuthResponseDto>(userInfoJson);
                if (userInfo != null)
                {
                    userInfo.Token = Session.GetString(TokenKey) ?? string.Empty;
                }
                return userInfo;
            }
            catch
            {
                return null;
            }
        }

        public bool IsAuthenticated()
        {
            var token = GetToken();
            return !string.IsNullOrEmpty(token);
        }

        public string? GetCurrentUserRole()
        {
            var userInfo = GetUserInfo();
            return userInfo?.Role;
        }

        public bool IsInRole(string role)
        {
            var userRole = GetCurrentUserRole();
            return string.Equals(userRole, role, StringComparison.OrdinalIgnoreCase);
        }
        public void ClearSession()
        {
            Session.Remove(TokenKey);
            Session.Remove(UserKey);
            Session.Clear();
        }
    }
}