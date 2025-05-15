using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace LibraryManagementSystem.Models
{
    public class Curriculum
    {
        [Key]
        public int Curriculum_ID { get; set; }
        [Required]
        public string? Curriculum_Name { get; set; }

        [ForeignKey("Department")]
        public int Department_ID { get; set; }
        [JsonIgnore]
        public virtual Department? Department { get; set; }
    }
}
