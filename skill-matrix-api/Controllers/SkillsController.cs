using Microsoft.AspNetCore.Mvc;
using skill_matrix_api.Entities;
using skill_matrix_api.Services;

namespace skill_matrix_api.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/skills")]
    public class SkillsController : ControllerBase
    {
        private readonly ISkillRepository _skillRepo;
        private readonly ICategoryRepository _categoryRepo;

        public SkillsController(ISkillRepository skillRepo, ICategoryRepository categoryRepo)
        {
            _skillRepo = skillRepo;
            _categoryRepo = categoryRepo;
        }

        /// <summary>
        /// Retrieves a list of skills.
        /// </summary>
        /// <returns>An action result containing the list of skills.</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Skill>>> GetSkills()
        {
            // get skills
            IEnumerable<Skill> skills = await _skillRepo.GetSkillsAsync();

            return Ok(skills);
        }

        /// <summary>
        /// Retrieves a specific skill by its ID.
        /// </summary>
        /// <param name="SkillId">The ID of the skill to retrieve.</param>
        /// <returns>An action result containing the retrieved skill.</returns>
        [HttpGet("{SkillId}", Name = "GetSkill")]
        public async Task<ActionResult<Skill>> GetSkill(int SkillId)
        {
            // get skill
            var skill = await _skillRepo.GetSkillAsync(SkillId);

            // check if skill exists
            if (skill == null) { return NotFound(); }

            return Ok(skill);
        }

        /// <summary>
        /// Creates a new skill.
        /// </summary>
        /// <param name="skill">The skill object to create.</param>
        /// <returns>An action result containing the created skill.</returns>
        [HttpPost]
        public async Task<ActionResult<Skill>> PostSkill([FromBody] Skill skill)
        {
            if(skill == null)
                throw new ArgumentNullException(nameof(skill));

            // Check if Category exist
            var categoryExists = await _categoryRepo.GetCategoryAsync(skill.CategoryId);

            if (categoryExists == null)
                return BadRequest(new { message = "One or more of the provided IDs do not exist in the database." });

            await _skillRepo.PostSkillAsync(skill);

            await _skillRepo.SaveChangesAsync();
            
            return CreatedAtRoute("GetSkill",
                new { SkillId = skill.SkillId },
                skill
            );
        }

        [HttpPost("bulk")]
        public async Task<ActionResult<Skill>> BulkPostSkill([FromBody] List<Skill> skills)
        {
            for (int i = 0; i < skills.Count; i++)
            { 
                var skill = skills[i];

                if (skill == null)
                    throw new ArgumentNullException(nameof(skill));

                // Check if Category exist
                var categoryExists = await _categoryRepo.GetCategoryAsync(skill.CategoryId);

                if (categoryExists == null)
                    return BadRequest(new { message = "One or more of the provided IDs do not exist in the database." });
            }

            await _skillRepo.PostRangeOfSkillsAsync(skills);

            await _skillRepo.SaveChangesAsync();

            return NoContent();
        }

        /// <summary>
        /// Deletes a specific skill by its ID.
        /// </summary>
        /// <param name="SkillId">The ID of the skill to delete.</param>
        /// <returns>An action result indicating the status of the deletion.</returns>
        [HttpDelete("{SkillId}")]
        public async Task<ActionResult> DeleteSkill(int SkillId)
        {
            if (await _skillRepo.DeleteSkillAsync(SkillId) != 0)
                return BadRequest(new { message = "The data your tring to delete does not exist" });

            await _skillRepo.SaveChangesAsync();

            return NoContent();
        }
    }
}
