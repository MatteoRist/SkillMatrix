using skill_matrix_api.Models.SkillLevels;
using skill_matrix_api.Models.Skills;
using skill_matrix_api.Models.Users;

namespace skill_matrix_api.Models.SkillRecords
{
    public class SkillRecordDto
    {
        public int RecordId { get; set; }
        public int UserId { get; set; }
        public int SkillId { get; set; }
        public int LevelId { get; set; }
        public int YearsOfExperience { get; set; }
        public string Note { get; set; }

        // Navigation properties
        public UserDto User { get; set; }
        public SkillDto Skill { get; set; }
        public SkillLevelDto SkillLevel { get; set; }
    }

}
