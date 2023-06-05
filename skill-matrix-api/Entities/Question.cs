using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Xml.Serialization;

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

        [XmlIgnore]
        public ICollection<Record> Records { get; set; } = new List<Record>();
    }
}
