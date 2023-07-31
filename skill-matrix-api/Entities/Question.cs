using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Xml.Serialization;
using System.Text.Json.Serialization;

namespace skill_matrix_api.Entities
{
    public class Question
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int QuestionId { get; set; }

        [Required]
        [MaxLength(1024)]
        public string Body { get; set; } = string.Empty;

        [Required]
        public int MinValue { get; set; } = 1;

        [Required]
        public int MaxValue { get; set; } = 5;
    }
}
