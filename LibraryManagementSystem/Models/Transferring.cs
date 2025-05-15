using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LibraryManagementSystem.Models
{
    public class Transferring
    {
        [Key]
        public int Transferring_ID { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public DateTime Transferring_Date { get; set; }

        [ForeignKey("SourceLibraryBook")]
        public int SourceLibraryBookID { get; set; }

        public virtual LibraryBook? SourceLibraryBook { get; set; }

        [ForeignKey("DestinationLibraryBook")]
        public int DestinationLibraryBookID { get; set; }

        public virtual LibraryBook? DestinationLibraryBook { get; set; }
    }

}
