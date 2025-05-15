using System.ComponentModel.DataAnnotations;

namespace LibraryManagementSystem.Models
{
    public class PowerCampusUser
    {
        public string User_Id { get; set; }
        public string User_First_Name { get; set; }
        public string User_Middle_Name { get; set; }
        public string User_Last_Name { get; set; }
        public string Prefix { get; set; }
    }

}