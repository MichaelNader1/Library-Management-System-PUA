using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LibraryManagementSystem.Models
{
    public class BookLibraryBook
    {

        [ForeignKey("Book")]
        public int BookID { get; set; }
        public virtual Book? Book { get; set; }

        [ForeignKey("LibraryBook")]
        public int LibraryBookID { get; set; }
        public virtual LibraryBook? LibraryBook { get; set; }
    }
}
