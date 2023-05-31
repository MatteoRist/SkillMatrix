using Microsoft.AspNetCore.Mvc;
using skill_matrix_api.Models.Skills;

namespace skill_matrix_api.Controllers
{
    [ApiController]
    [Route("api/skills")]
    public class SkillsController : ControllerBase
    {
        [HttpGet]
        public ActionResult<IEnumerable<SkillDto>> GetSkills()
        {
            return Ok(DataMapper.MapToDto(SkillsDataStore.Current.Skills));
        }

        [HttpGet("{id}")]
        public ActionResult<SkillDto> GetSkill(int id)
        {
            var skillToReturn = SkillsDataStore.Current.Skills.FirstOrDefault(c => c.SkillId == id);

            if (skillToReturn == null) { return NotFound(); }

            return Ok(DataMapper.MapToDto(skillToReturn));
        }
    }
}
