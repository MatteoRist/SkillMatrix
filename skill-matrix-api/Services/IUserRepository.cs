using skill_matrix_api.Entities;

namespace skill_matrix_api.Services
{
    public interface IUserRepository
    {
        Task<User?> GetUserAsync(int UserId);
        Task<IEnumerable<User>> GetUsersAsync();
    }
}