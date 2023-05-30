using skill_matrix_api.Models.SkillRecords;

namespace skill_matrix_api.Models.Users
{
    public class UserDto
    {
        public int UserId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public ICollection<SkillRecordDto> SkillRecords { get; set; }
    }
}
