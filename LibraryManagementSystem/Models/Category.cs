using System.ComponentModel.DataAnnotations;

namespace LibraryManagementSystem.Models
{
    public class Category
    {
        [Key]
        public int Category_Id { get; set; }
        [Required, MaxLength(512)]
        public string? Category_Name { get; set; }
    }
}
