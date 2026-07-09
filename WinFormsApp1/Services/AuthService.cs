using Microsoft.EntityFrameworkCore;
using WinFormsApp1.Data;
using WinFormsApp1.Helpers;
using WinFormsApp1.Models;

namespace WinFormsApp1.Services
{
    public class AuthService : IAuthService
    {
        private readonly AppDbContext _context;

        public AuthService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<ApplicationUser?> LoginAsync(string username, string password)
        {
            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
                return null;

            var user = await _context.ApplicationUsers
                .FirstOrDefaultAsync(u => u.Username == username);

            if (user == null)
                return null;

            if (!PasswordHasher.VerifyPassword(password, user.PasswordHash))
                return null;

            return user;
        }

        public async Task<ApplicationUser?> GetUserByIdAsync(int userId)
        {
            return await _context.ApplicationUsers.FindAsync(userId);
        }

        public async Task<bool> ValidateCredentialsAsync(string username, string password)
        {
            var user = await LoginAsync(username, password);
            return user != null;
        }

        public async Task<(bool Success, string Message)> ChangePasswordAsync(int userId, string oldPassword, string newPassword)
        {
            if (string.IsNullOrWhiteSpace(oldPassword) || string.IsNullOrWhiteSpace(newPassword))
                return (false, "Both current and new passwords are required.");

            if (newPassword.Length < 4)
                return (false, "New password must be at least 4 characters.");

            if (oldPassword == newPassword)
                return (false, "New password must be different from current password.");

            var user = await _context.ApplicationUsers.FindAsync(userId);
            if (user == null)
                return (false, "User not found.");

            if (!PasswordHasher.VerifyPassword(oldPassword, user.PasswordHash))
                return (false, "Current password is incorrect.");

            user.PasswordHash = PasswordHasher.HashPassword(newPassword);
            user.UpdatedAt = DateTime.UtcNow;
            await _context.SaveChangesAsync();

            return (true, "Password changed successfully.");
        }
    }
}
