using WinFormsApp1.Data;
using WinFormsApp1.Helpers;
using WinFormsApp1.Models;

namespace WinFormsApp1.Services
{
    public class UserService
    {
        private readonly IUnitOfWork _unitOfWork;

        public UserService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IReadOnlyList<ApplicationUser>> GetAllAsync()
        {
            return await _unitOfWork.Repository<ApplicationUser>().GetAllAsync();
        }

        public async Task<ApplicationUser?> GetByIdAsync(int id)
        {
                return await _unitOfWork.Repository<ApplicationUser>().GetByIdAsync(id);
        }

        public async Task<(bool Success, string Message)> CreateAsync(ApplicationUser user, string password)
        {
            if (string.IsNullOrEmpty(user.Username))
            {
                return (false, "Username need fill !");
            }
            if (string.IsNullOrWhiteSpace(password))
            {
                return (false, "Password is empty !");
            }
            if (password.Length < 8)
            {
                return (false, "Password need more 8 characters !");
            }
            var existing = await _unitOfWork.Repository<ApplicationUser>()
                .FindAsync(s => s.Username == user.Username);
            if (existing.Any())
            {
                return (false, "User is already exist!");
            }
            user.PasswordHash = PasswordHasher.HashPassword(password);
            user.CreatedAt = DateTime.Now;
            user.UpdatedAt = DateTime.Now;
            await _unitOfWork.Repository<ApplicationUser>().AddAsync(user);
            await _unitOfWork.SaveChangesAsync();
            return (true, "Add User Successfully !");
        }

        public async Task<(bool Success, string Message)> UpdateAsync(ApplicationUser user, string? newPassword)
        {
            var existing = await _unitOfWork.Repository<ApplicationUser>().GetByIdAsync(user.Id);
            if(existing == null)
            {
                return (false, $"Cant Find User id : {user.Id} !");
            }
            existing.Role = user.Role;
            existing.Username = user.Username;
            if (!string.IsNullOrEmpty(newPassword) && newPassword.Length >= 8)
            {
                existing.PasswordHash = PasswordHasher.HashPassword(newPassword);
            }
            existing.UpdatedAt = DateTime.Now;
            await _unitOfWork.Repository<ApplicationUser>().UpdateAsync(existing);
            await _unitOfWork.SaveChangesAsync();
            return (true, "Update User Successfully!");
        }

        public async Task<(bool Success, string Message)> DeleteAsync(int id)
        {
            if (id == SessionManager.CurrentUser?.Id)
                return (false, "Cannot delete your own account.");

            var user = await _unitOfWork.Repository<ApplicationUser>().GetByIdAsync(id);
            if (user == null)
                return (false, "User not found.");

            if (user.CheckedOutBorrows.Any())
                return (false, "Cannot delete user with active borrow records.");

            await _unitOfWork.Repository<ApplicationUser>().DeleteAsync(id);
            await _unitOfWork.SaveChangesAsync();

            return (true, "User deleted successfully.");
        }
    }
}
