using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace LibraryManagementSystem.Models
{
    public class Library
    {
        [Key]
        public int LibraryID { get; set; }
        [Required]
        public string Name { get; set; }
        [JsonIgnore]
        public virtual ICollection<AdminUser> Users { get; set; } = new List<AdminUser>();

    }
}
