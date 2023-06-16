using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using System.Xml.Serialization;

namespace skill_matrix_api.Entities
{
    [Index("UserId", "SkillId", "QuestionId", IsUnique = true, Name ="IX_UniqueRecord")]
    public class Record
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int RecordId { get; set; }

        [Required]
        public int UserId { get; set; }

        [Required]
        public int SkillId { get; set; }

        [Required]
        public int QuestionId { get; set; }

        [Required]
        public int Value { get; set; }
    } 
}
