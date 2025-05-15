using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace LibraryManagementSystem.Models
{
    public class Department
    {
        [Key]
        public int Department_ID { get; set; }
        [Required]
        public string? Department_Name { get; set; }

        [ForeignKey("College")]
        public int College_ID { get; set; }
        [JsonIgnore]
        public virtual College? College { get; set; } 
            
        public virtual ICollection<Curriculum> Curriculums { get; set; } = new List<Curriculum>();
    }
}