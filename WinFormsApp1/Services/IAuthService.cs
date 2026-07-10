using WinFormsApp1.Models;

namespace WinFormsApp1.Services
{
    public interface IAuthService
    {
        Task<ApplicationUser?> LoginAsync(string username, string password);
        Task<ApplicationUser?> GetUserByIdAsync(int userId);
        Task<(bool Success, string Message)> ChangePasswordAsync(int userId, string oldPassword, string newPassword);
    }
}
