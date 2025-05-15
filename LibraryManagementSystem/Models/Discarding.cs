using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LibraryManagementSystem.Models
{
    public class Discarding
    {
        [Key]
        public int Discarding_ID { get; set; }
        [Required]
        public string? Discarding_Reason { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime Discarding_Date { get; set; }
        [Required]
        [ForeignKey("LibraryBook")]
        public int LibraryBookID { get; set; }
        public virtual LibraryBook? LibraryBook { get; set; }


    }
}
