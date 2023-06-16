using skill_matrix_api.Entities;

namespace skill_matrix_api.Services
{
    /// <summary>
    /// Represents a repository interface for category-related operations.
    /// </summary>
    public interface ICategoryRepository : ICrudRepository
    {
        /// <summary>
        /// Asynchronously deletes a category by its ID.
        /// </summary>
        /// <param name="CategoryId">The ID of the category to delete.</param>
        /// <returns>A task representing the asynchronous operation and containing the number of affected records.</returns>
        Task<int> DeleteCategoryAsync(int CategoryId);

        /// <summary>
        /// Asynchronously retrieves a category by its ID.
        /// </summary>
        /// <param name="CategoryId">The ID of the category to retrieve.</param>
        /// <returns>A task representing the asynchronous operation and containing the retrieved category.</returns>
        Task<Category?> GetCategoryAsync(int CategoryId);

        /// <summary>
        /// Asynchronously retrieves a list of category.
        /// </summary>
        /// <returns>A task representing the asynchronous operation and containing the list of categories.</returns>
        Task<IEnumerable<Category>> GetCategoriesAsync();
        
        /// <summary>
        /// Asynchronously retrieves a category by its ID with all the associated skills.
        /// </summary>
        /// <param name="CategoryId">The ID of the category to retrieve.</param>
        /// <returns>A task representing the asynchronous operation and containing the retrieved category.</returns>
        Task<Category?> GetCategoryWithSkillsAsync(int CategoryId);

        /// <summary>
        /// Asynchronously retrieves a list of category.
        /// </summary>
        /// <returns>A task representing the asynchronous operation and containing the list of categories with all the associated skills.</returns>
        Task<IEnumerable<Category>> GetCategoriesWithSkillsAsync();

        /// <summary>
        /// Asynchronously creates a new category.
        /// </summary>
        /// <param name="category">The category object to create.</param>
        /// <returns>A task representing the asynchronous operation and containing the created category.</returns>
        Task PostCategoryAsync(Category category);
    }
}