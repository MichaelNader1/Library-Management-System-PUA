using System.ComponentModel.DataAnnotations;

namespace LibraryManagementSystem.Models
{
    public class Author
    {
        [Key]
        public int Author_ID { get; set; }

        [Required, MaxLength(255)]
        public string? Author_Name { get; set; }

        [MaxLength(255)]
        public string? Author_NickName { get; set; }
    }
}
