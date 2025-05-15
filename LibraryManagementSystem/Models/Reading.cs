using LibraryManagementSystem.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

public class Reading
{
    [Key]
    public int Reading_Id { get; set; }

    [Required]
    [DataType(DataType.Date)]
    public DateTime Start_Time { get; set; }

    public DateTime End_Time { get; set; }

    public bool IsFinished { get; set; }

    [ForeignKey(nameof(PowerCampusUser))]
    public string User_Id { get; set; }
    [JsonIgnore]
    public virtual PowerCampusUser? PowerCampusUser { get; set; }

    [ForeignKey(nameof(LibraryBook))]
    public int LibraryBookID { get; set; }
    [JsonIgnore]
    public virtual LibraryBook? LibraryBook { get; set; }
}
