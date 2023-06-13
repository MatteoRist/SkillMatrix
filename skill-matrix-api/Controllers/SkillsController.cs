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
        private readonly ISkillRepository _dataStore;

        public SkillsController(ISkillRepository dataStore)
        {
            _dataStore = dataStore;
        }

        /// <summary>
        /// Retrieves a list of skills.
        /// </summary>
        /// <returns>An action result containing the list of skills.</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Skill>>> GetSkills()
        {
            return Ok(await _dataStore.GetSkillsAsync());
        }

        /// <summary>
        /// Retrieves a specific skill by its ID.
        /// </summary>
        /// <param name="SkillId">The ID of the skill to retrieve.</param>
        /// <returns>An action result containing the retrieved skill.</returns>
        [HttpGet("{SkillId}", Name = "GetSkill")]
        public async Task<ActionResult<Skill>> GetSkill(int SkillId)
        {
            var skillToReturn = await _dataStore.GetSkillAsync(SkillId);

            if (skillToReturn == null) { return NotFound(); }

            return Ok(skillToReturn);
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

            await _dataStore.PostSkillAsync(skill);

            await _dataStore.SaveChangesAsync();
            
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
            }

            await _dataStore.PostRangeOfSkillsAsync(skills);

            await _dataStore.SaveChangesAsync();

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
            if (await _dataStore.DeleteSkillAsync(SkillId) != 0)
                return BadRequest(new { message = "The data your tring to delete does not exist" });

            await _dataStore.SaveChangesAsync();

            return NoContent();
        }
    }
}
