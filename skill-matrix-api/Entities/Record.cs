using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using System.Xml.Serialization;

namespace skill_matrix_api.Entities
{
    public class Record
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int RecordId { get; set; }

        [Required]
        public int UserId { get; set; }

        //[ForeignKey("UserId")]
        //public User? User { get; set; }

        [Required]
        public int SkillId { get; set; }

        //[ForeignKey("SkillId")]
        //public Skill? Skill { get; set; }

        [Required]
        public int QuestionId { get; set; }

        //[ForeignKey("QuestionId")]
        //public Question? Question { get; set; }

        [Required]
        public int Value { get; set; }
    } 
}
