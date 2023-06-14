using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using skill_matrix_api.DbContexts;
using skill_matrix_api.Entities;

namespace skill_matrix_api.Services
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly MatrixContext _context;

        /// <summary>
        /// Initializes a new instance of the CategoryRepository class.
        /// </summary>
        /// <param name="context">The MatrixContext instance.</param>
        public CategoryRepository(MatrixContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        /// <inheritdoc cref="ICategoryRepository.GetCategoryAsync"/>
        public async Task<Category?> GetCategoryAsync(int CategoryId)
        {
            return await _context.Category
                .Where(c => c.CategoryId == CategoryId)
                .Include(c => c.Skills)
                .FirstOrDefaultAsync();
        }

        /// <inheritdoc cref="ICategoryRepository.GetCategoriesAsync"/>
        public async Task<IEnumerable<Category>> GetCategoriesAsync()
        {
            return await _context.Category.ToListAsync();
        }

        /// <inheritdoc cref="ICategoryRepository.PostCategoryAsync"/>
        public async Task PostCategoryAsync(Category category)
        {
            await _context.AddAsync<Category>(category);
        }

        /// <inheritdoc cref="ICategoryRepository.DeleteCategoryAsync"/>
        public async Task<int> DeleteCategoryAsync(int CategoryId)
        {
            Category? categoryToDelete = await _context.Category.FirstOrDefaultAsync(c => c.CategoryId == CategoryId);

            if (categoryToDelete == null)
            {
                return 1;
            }
            else
            {
                _context.Category.Remove(categoryToDelete);
                return 0;
            }
        }

        /// <inheritdoc cref="ICategoryRepository.SaveChangesAsync"/>
        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}
