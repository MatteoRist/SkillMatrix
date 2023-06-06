using skill_matrix_api.Entities;

namespace skill_matrix_api.Models
{
    public class RecordGetDto
    {
        public int RecordId { get; set; }
        public User? User { get; set; }
        public Skill? Skill { get; set; }
        public Question? Question { get; set; }
        public int value { get; set; }
    }
}
