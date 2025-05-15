using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Text.Json.Serialization;

namespace LibraryManagementSystem.Models
{
    public class Visits
    {
        [Key]
        public int Visit_Id { get; set; }
        [ForeignKey(nameof(PowerCampusUser))]
        public string User_Id { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public DateTime Visit_Date { get; set; }
        public string Visit_Type { get; set; }
        [JsonIgnore]
        public virtual PowerCampusUser? PowerCampusUser { get; set; }

        [ForeignKey(nameof(Library))]
        public int Library_Id { get; set; }
        [JsonIgnore]
        public virtual Library? Library { get; set; }


    }
}
