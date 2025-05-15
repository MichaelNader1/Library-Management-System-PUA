using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace LibraryManagementSystem.Models
{
    public class Publisher
    {
        [Key]
        public int Publisher_ID { get; set; }
        [Required]
        public string? Publisher_Name { get; set; }
        [JsonIgnore]
        public virtual ICollection<Book> Books { get; set; } = new List<Book>();


    }
}
