using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using LibraryManagementSystem.Models;
using System.Text.Json.Serialization;

public class Copying
{
    [Key]
    public int Copying_Id { get; set; }

    [Required]
    [DataType(DataType.Date)]
    public DateTime Start_Time { get; set; }
    public DateTime End_Time { get; set; }
    public bool IsReturned { get; set; }

    [ForeignKey(nameof(PowerCampusUser))]
    public string User_Id { get; set; }
    public virtual PowerCampusUser? PowerCampusUser { get; set; }

    [ForeignKey(nameof(LibraryBook))]
    public int LibraryBookID { get; set; }

    [JsonIgnore]
    public virtual LibraryBook? LibraryBook { get; set; }
}
