using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace LibraryManagementSystem.Models
{
    public class Language
    {
        [Key]
        public int Language_Id { get; set; }
        [Required]
        public string? Language_Name{ get; set; }
        [JsonIgnore]
        public virtual ICollection<Book> books { get; set; } = new List<Book>();
    }
}
