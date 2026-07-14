using Microsoft.EntityFrameworkCore;
using WinFormsApp1.Data;
using WinFormsApp1.Helpers;
using WinFormsApp1.Models;

namespace WinFormsApp1.Services
{
    public class AuthService
    {
        private readonly IUnitOfWork _unitOfWork;

        public AuthService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ApplicationUser?> LoginAsync(string username, string password)
        {
            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
                return null;

            var user = await _unitOfWork.Repository<ApplicationUser>()
                .FindAsync(u => u.Username == username);

            var matched = user.FirstOrDefault();
            if (matched == null)
                return null;

            if (!PasswordHasher.VerifyPassword(password, matched.PasswordHash))
                return null;

            /// start the transaction
            await _unitOfWork.BeginTransactionAsync();

            // audit log
            try
            {
                await _unitOfWork.Repository<AuditLog>().AddAsync(new AuditLog
                {
                    UserId = matched.Id,
                    Action = "Login",
                    EntityName = "ApplicationUser",
                    EntityId = matched.Id,
                    Details = $"User '{matched.Username}' logged in",
                    Timestamp = DateTime.UtcNow
                });

                await _unitOfWork.SaveChangesAsync();
                await _unitOfWork.CommitAsync();
            }
            catch
            {
                await _unitOfWork.RollbackAsync();
            }

            return matched;
        }

        public async Task<ApplicationUser?> GetUserByIdAsync(int userId)
        {
            return await _unitOfWork.Repository<ApplicationUser>().GetByIdAsync(userId);
        }

        public async Task<(bool Success, string Message)> ChangePasswordAsync(int userId, string oldPassword, string newPassword)
        {
            if (string.IsNullOrWhiteSpace(oldPassword) || string.IsNullOrWhiteSpace(newPassword))
                return (false, "Both current and new passwords are required.");

            if (newPassword.Length < 8)
                return (false, "New password must be at least 8 characters.");

            if (oldPassword == newPassword)
                return (false, "New password must be different from current password.");

            var user = await _unitOfWork.Repository<ApplicationUser>().GetByIdAsync(userId);
            if (user == null)
                return (false, "User not found.");

            if (!PasswordHasher.VerifyPassword(oldPassword, user.PasswordHash))
                return (false, "Current password is incorrect.");

            user.PasswordHash = PasswordHasher.HashPassword(newPassword);
            user.UpdatedAt = DateTime.UtcNow;
            await _unitOfWork.SaveChangesAsync();

            return (true, "Password changed successfully.");
        }
    }
}
