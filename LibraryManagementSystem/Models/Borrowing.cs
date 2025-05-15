using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using LibraryManagementSystem.Models;
public class Borrowing
{
    [Key]
    public int Borrowing_Id { get; set; }

    [Required]
    [Range(1, 365)]
    public int NumberOfDays { get; set; }

    [Required]
    [DataType(DataType.Date)]
    public DateTime Date_Of_Borrowing { get; set; }
    public DateTime Return_Date { get; set; }
    public DateTime Actual_Return_Date { get; set; }
    public bool IsReturned { get; set; }

    [ForeignKey(nameof(PowerCampusUser))]
    public string User_Id { get; set; }
    [JsonIgnore]
    public virtual PowerCampusUser? PowerCampusUser { get; set; }
    [JsonIgnore]
    public virtual Penalty? penalty { get; set; }

    [ForeignKey(nameof(LibraryBook))]
    [Column("LibraryBook_ID")]
    public int LibraryBookID { get; set; }

    [JsonIgnore]
    public virtual LibraryBook? LibraryBook { get; set; }

}
