namespace skill_matrix_api.Services
{
    /// <summary>
    /// Represents a repository interface for basic CRUD operations.
    /// </summary>
    public interface ICrudRepository
    {
        /// <summary>
        /// Asynchronously saves the changes made in the repository.
        /// </summary>
        /// <returns>A task representing the asynchronous operation.</returns>
        public Task<int> SaveChangesAsync();
    }
}
