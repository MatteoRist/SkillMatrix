using Microsoft.AspNetCore.Mvc;
using skill_matrix_api.Entities;
using skill_matrix_api.Services;

namespace skill_matrix_api.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/categories")]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryRepository _dataStore;

        public CategoryController(ICategoryRepository dataStore)
        {
            _dataStore = dataStore;
        }

        /// <summary>
        /// Retrieves a list of categories.
        /// </summary>
        /// <returns>An action result containing the list of categories.</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Category>>> GetCategories()
        {
            return Ok(await _dataStore.GetCategoriesAsync());
        }

        /// <summary>
        /// Retrieves a specific category by its ID.
        /// </summary>
        /// <param name="CategoryId">The ID of the category to retrieve.</param>
        /// <returns>An action result containing the retrieved category.</returns>
        [HttpGet("{CategoryId}", Name = "GetCategory")]
        public async Task<ActionResult<Category>> GetCategory(int CategoryId)
        {
            var categoryToReturn = await _dataStore.GetCategoryAsync(CategoryId);

            if (categoryToReturn == null) { return NotFound(); }

            return Ok(categoryToReturn);
        }

        /// <summary>
        /// Creates a new category.
        /// </summary>
        /// <param name="category">The category object to create.</param>
        /// <returns>An action result containing the created category.</returns
        [HttpPost]
        public async Task<ActionResult<Category>> PostCategory([FromBody] Category category)
        {
            if (category == null)
                throw new ArgumentNullException(nameof(category));

            await _dataStore.PostCategoryAsync(category);

            await _dataStore.SaveChangesAsync();

            return CreatedAtRoute("GetCategory",
                new { CategoryId = category.CategoryId },
                category
            );
        }

        /// <summary>
        /// Deletes a specific Category by its ID.
        /// </summary>
        /// <param name="CategoryId">The ID of the category to delete.</param>
        /// <returns>An action result indicating the status of the deletion.</returns>
        [HttpDelete("{CategoryId}")]
        public async Task<ActionResult> DeleteSkill(int CategoryId)
        {
            if (await _dataStore.DeleteCategoryAsync(CategoryId) != 0)
                return BadRequest(new { message = "The data your tring to delete does not exist" });
           
            await _dataStore.SaveChangesAsync();

            return NoContent();
        }
    }
}
