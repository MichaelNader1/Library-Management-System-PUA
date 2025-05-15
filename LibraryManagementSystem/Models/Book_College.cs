using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace LibraryManagementSystem.Models
{
    public class Book_College
    {
        [Key]
        public int Id { get; set; }

        public int Book_ID { get; set; }

        [ForeignKey(nameof(Book_ID))]
        [JsonIgnore]
        public virtual Book? Book { get; set; }

        public int College_ID { get; set; }

        [ForeignKey(nameof(College_ID))]
        [JsonIgnore]
        public virtual College? College { get; set; }
    }

}


