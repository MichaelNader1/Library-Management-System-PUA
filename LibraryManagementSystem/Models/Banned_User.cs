using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LibraryManagementSystem.Models
{
    public class Banned_User
    {
        [Key]
        public int Ban_Id { get; set; }

        [ForeignKey(nameof(PowerCampusUser))]
        public string User_Id { get; set; }


        [DataType(DataType.Date)]
        [Required]
        public DateTime Ban_Start_Date { get; set; }

        [Required]
        public DateTime Ban_End_Date { get; set; }

        public virtual PowerCampusUser? PowerCampusUser { get; set; }
    }
}
