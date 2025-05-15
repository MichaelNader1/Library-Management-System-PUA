using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace LibraryManagementSystem.Models
{
    public class College
    {
        [Key]
        public int College_ID { get; set; }

        [Required, MaxLength(512)]
        public string? College_Name { get; set; }

        
        public virtual ICollection<Department> Departments { get; set; } = new List<Department>();

        public virtual ICollection<Book_College> Book_Colleges { get; set; } = new List<Book_College>();
    }
}
