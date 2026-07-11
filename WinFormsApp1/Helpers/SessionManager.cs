using WinFormsApp1.Models;
using WinFormsApp1.Models.Enums;

namespace WinFormsApp1.Helpers
{
    public static class SessionManager
    {
        public static ApplicationUser? CurrentUser { get; private set; }
        public static bool IsLoggedIn => CurrentUser != null;

        public static void Login(ApplicationUser user)
        {
            CurrentUser = user;
        }

        public static void Logout()
        {
            CurrentUser = null;
        }

        public static int GetCurrentUserId()
        {
            return CurrentUser?.Id ?? 0;
        }

        public static UserRole GetCurrentUserRole()
        {
            return CurrentUser?.Role ?? UserRole.Staff;
        }

        public static bool HasRole(UserRole role)
        {
            return CurrentUser?.Role == role;
        }

        public static bool IsAdmin => CurrentUser?.Role == UserRole.Admin;
        public static bool IsLibrarian => CurrentUser?.Role == UserRole.Librarian;
        public static bool IsStaff => CurrentUser?.Role == UserRole.Staff;
    }
}
