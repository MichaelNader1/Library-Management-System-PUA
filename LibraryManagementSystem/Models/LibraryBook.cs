using LibraryManagementSystem.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

public class LibraryBook
{
    [Key]
    public int LibraryBookID { get; set; }

    [ForeignKey("Library")]
    public int LibraryID { get; set; }

    [ForeignKey("Book")]
    public int BookID { get; set; }

    public int Available_Copies { get; set; }

    [Required]
    public int Total_Copies { get; set; }

    [JsonIgnore]
    public virtual Library? Library { get; set; }
    [JsonIgnore]
    public virtual ICollection<BookLibraryBook> BookLibraryBooks { get; set; } = new List<BookLibraryBook>();
    [JsonIgnore]
    public virtual ICollection<Reading> Reading { get; set; } = new List<Reading>();
    [JsonIgnore]
    public virtual ICollection<Borrowing> Borrowing { get; set; } = new List<Borrowing>();
    [JsonIgnore]
    public virtual ICollection<Copying> Copyings { get; set; } = new List<Copying>();
    [JsonIgnore]
    public virtual Discarding? Discarding { get; set; }

}
