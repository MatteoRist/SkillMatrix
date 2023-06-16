using skill_matrix_api.Entities;

namespace skill_matrix_api.Services
{
    /// <summary>
    /// Represents a repository interface for user-related operations.
    /// </summary>
    public interface IUserRepository
    {
        /// <summary>
        /// Asynchronously retrieves a user by their ID.
        /// </summary>
        /// <param name="UserId">The ID of the user to retrieve.</param>
        /// <returns>A task representing the asynchronous operation and containing the retrieved user.</returns>
        Task<User?> GetUserAsync(int UserId);

        /// <summary>
        /// Asynchronously retrieves a list of users.
        /// </summary>
        /// <returns>A task representing the asynchronous operation and containing the list of users.</returns>
        Task<IEnumerable<User>> GetUsersAsync();

        /// <summary>
        /// Asynchronously retrieves user's statistics.
        /// </summary>
        /// <returns>A task representing the asynchronous operation and containing the list of statistics.</returns>
        Task<IEnumerable<Statistic>> GetUserStatisticsAsync(int UserId);
    }
}