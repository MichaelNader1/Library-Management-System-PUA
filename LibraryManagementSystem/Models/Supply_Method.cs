using System.ComponentModel.DataAnnotations;

namespace LibraryManagementSystem.Models
{
    public class Supply_Method
    {
        [Key]
        public int Supply_Method_Id { get; set; }
        [Required]
        public string Supply_Method_Name { get; set; }
    }
}
