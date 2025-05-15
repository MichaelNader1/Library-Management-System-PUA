using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace LibraryManagementSystem.Models
{
    public class Subheading
    {
        [Key]
        public int Subheading_Id { get; set; }
        [Required]
        public string? Subheading_Name { get; set; }

        [JsonIgnore]
        public virtual ICollection<Book> books { get; set; } = new List<Book>();
    }
}
