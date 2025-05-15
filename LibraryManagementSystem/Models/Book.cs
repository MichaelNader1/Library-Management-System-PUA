using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace LibraryManagementSystem.Models
{
    public class Book
    {
    
            [Key]
            public int BookID { get; set; }

            [Required, MaxLength(50)]
            public string? Classification_Number { get; set; }

            [Required, MaxLength(120)]
            public string? ISBN { get; set; }

            [Required, MaxLength(255)]
            public string? Title { get; set; }

            [MaxLength(255)]
            public string? Subtitle { get; set; }

            [Required]
            [DataType(DataType.Date)]
            public DateTime Publish_Year { get; set; }

            public bool IsMissing { get; set; }
            public bool IsDamage { get; set; }

            [MaxLength(1000)]
            public string? Notes { get; set; }

            [DataType(DataType.ImageUrl)]
            public string? CoverImage { get; set; }

            [DataType(DataType.Upload)]
            public string? Attachment { get; set; }

            [MaxLength(255)]
            public string? Place_of_publication { get; set; }

            [MaxLength(50)]
            public string? Locator { get; set; }


            public bool IsLocked { get; set; }

            [Range(0, 10000)]
            public int Price { get; set; }

            public string? Donor_Name { get; set; }

            [DataType(DataType.Date)]
            public DateTime Reciving_Date { get; set; }


        [JsonIgnore]
        public virtual ICollection<Subheading> BookSubheadings { get; set; } = new List<Subheading>();
        [JsonIgnore]
        public virtual ICollection<Book_College> Book_Colleges { get; set; } = new List<Book_College>();
        public virtual Author? Author { get; set; }
            [ForeignKey(nameof(Author))]
            public int Author_Id { get; set; }
        [JsonIgnore]
        public virtual ICollection<Publisher> Publishers { get; set; } = new List<Publisher>();
        [JsonIgnore]
        public virtual ICollection<Language> Languages { get; set; } = new List<Language>();

            [ForeignKey(nameof(Category))]
            public int Category_Id { get; set; }
        
            public virtual Category? Category { get; set; }

            [ForeignKey(nameof(Supply_Method))]
            public int Supply_Method_Id { get; set; }
            public virtual Supply_Method? Supply_Method { get; set; }
        [JsonIgnore]
        public virtual ICollection<BookLibraryBook> BookLibraryBooks { get; set; } = new List<BookLibraryBook>();



        [NotMapped]
        public List<int> LanguageIds { get; set; } = new List<int>();
        [NotMapped]
        public List<int> SubheadingIds { get; set; } = new List<int>();
        [NotMapped]
        public List<int> PublisherIds { get; set; } = new List<int>();
        [NotMapped]
        public List<int> CollegeIds { get; set; } = new List<int>();
    }
}

   
   
