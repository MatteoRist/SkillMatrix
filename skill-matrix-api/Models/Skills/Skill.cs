namespace skill_matrix_api.Models.Skills
{
    public class Skill
    {
        public int SkillId { get; set; }
        public string Category { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
    }
}
