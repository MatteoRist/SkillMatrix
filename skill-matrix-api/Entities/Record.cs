using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Serialization;

namespace skill_matrix_api.Entities
{
    public class Record
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int RecordId { get; set; }

        public int UserId { get; set; }

        [ForeignKey("UserId")]
        public User? User { get; set; }

        public int SkillId { get; set; }

        [ForeignKey("SkillId")]
        public Skill? Skill { get; set; }

        public int QuestionId { get; set; }

        [ForeignKey("QuestionId")]
        public Question? Question { get; set; } 

        public int value { get; set; }
    } 
}
