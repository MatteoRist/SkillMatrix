using Microsoft.AspNetCore.Mvc;
using skill_matrix_api.Entities;
using skill_matrix_api.Services;

namespace skill_matrix_api.Controllers
{
    [ApiController]
    [Route("api/skills")]
    public class SkillsController : ControllerBase
    {
        private readonly ISkillRepository _dataStore;

        public SkillsController(ISkillRepository dataStore)
        {
            _dataStore = dataStore;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Skill>>> GetSkills()
        {
            return Ok(await _dataStore.GetSkillsAsync());
        }

        [HttpGet("{SkillId}", Name = "GetSkill")]
        public async Task<ActionResult<Skill>> GetSkill(int SkillId)
        {
            var skillToReturn = await _dataStore.GetSkillAsync(SkillId);

            if (skillToReturn == null) { return NotFound(); }

            return Ok(skillToReturn);
        }

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
