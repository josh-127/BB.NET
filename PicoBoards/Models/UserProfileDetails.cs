using System;
using System.ComponentModel.DataAnnotations;

namespace PicoBoards.Models
{
    public class UserProfileDetails : IModel, IValidatable
    {
        [Display(Name = "Username")]
        public string UserName { get; set; }

        [Display(Name = "Group")]
        public string GroupName { get; set; }

        [Display(Name = "Joined")]
        public DateTime Created { get; set; }

        [Display(Name = "Last Active")]
        public DateTime LastActive { get; set; }

        [DataType(DataType.Date)]
        public DateTime? Birthday { get; set; }

        public string Location { get; set; }

        [DataType(DataType.Text)]
        public string Signature { get; set; }
    }
}