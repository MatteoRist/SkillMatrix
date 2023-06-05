using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Xml.Serialization;

namespace skill_matrix_api.Entities
{
    public class Skill
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int SkillId { get; set; }

        [Required]
        [MaxLength(50)]
        public string Category { get; set; } = string.Empty;

        [Required]
        [MaxLength(50)]
        public string Title { get; set; } = string.Empty;

        [XmlIgnore]
        public ICollection<Record> Records { get; set; } = new List<Record>();
    }
}
