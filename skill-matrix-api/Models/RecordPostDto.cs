using System.ComponentModel.DataAnnotations;

namespace skill_matrix_api.Models
{
    public class RecordPostDto
    {
        [Range(1, int.MaxValue, ErrorMessage = "You must provide a user id.")]
        public int UserId { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "You must provide a skill id.")]
        public int SkillId { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "You must provide a question id.")]
        public int QuestionId { get; set; }

        [Required(ErrorMessage = "You must provide a value.")]
        public int Value { get; set; }
    }

}
