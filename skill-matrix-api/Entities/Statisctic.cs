using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using System.Xml.Serialization;

namespace skill_matrix_api.Entities
{
    [Index("UserId", "CategoryId", IsUnique = true, Name ="IX_UniqueStatistic")]
    public class Statistic
    {
        [Required]
        public int UserId { get; set; }

        [Required]
        public int CategoryId { get; set; }

        [Required] 
        public string CategoryName { get; set; } = string.Empty;

        [Required]
        public int StatValue { get; set; }
    } 
}
