using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LibraryManagementSystem.Models
{
    public class Penalty
    {
        [Key]
        public int Penalty_Id { get; set; }
        public int Amount { get; set; }

        [DataType(DataType.Date)]
        public DateTime Penalty_Date { get; set; }
        [ForeignKey(nameof(Borrowing))]
        public int Borrowing_Id { get; set; }
        public virtual Borrowing? Borrowing { get; set; }
    }
}
