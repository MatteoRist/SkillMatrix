namespace skill_matrix_api.Models.SkillRecords
{
    public class SkillRecord
    {
        public int RecordId { get; set; }
        public int UserId { get; set; }
        public int SkillId { get; set; }
        public int LevelId { get; set; }
        public int YearsOfExperience { get; set; }
        public string Note { get; set; } = string.Empty;
    }
}
