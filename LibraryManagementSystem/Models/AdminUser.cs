using LibraryManagementSystem.Models;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

public class AdminUser : IdentityUser
{
    [Key]
    [Column("User_Id")]
    public override string Id { get; set; }
    [ForeignKey("Library")]
    public int LibraryID { get; set; }
    [JsonIgnore]
    public virtual Library? Library { get; set; }
}