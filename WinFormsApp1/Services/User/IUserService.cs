using WinFormsApp1.Models;

namespace WinFormsApp1.Services
{
    public interface IUserService
    {
        Task<IReadOnlyList<ApplicationUser>> GetAllAsync();
        Task<ApplicationUser?> GetByIdAsync(int id);
        Task<(bool Success, string Message)> CreateAsync(ApplicationUser user, string password);
        Task<(bool Success, string Message)> UpdateAsync(ApplicationUser user, string? newPassword);
        Task<(bool Success, string Message)> DeleteAsync(int id);
    }
}
