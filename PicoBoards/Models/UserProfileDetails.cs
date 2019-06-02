using System;
using System.ComponentModel.DataAnnotations;

namespace PicoBoards.Models
{
    public class UserProfileDetails
    {
        [Display(Name = "Username")]
        public string UserName { get; set; }

        [Display(Name = "Group")]
        public string GroupName { get; set; }

        [Display(Name = "Joined")]
        public DateTime Created { get; set; }

        [Display(Name = "Last Active")]
        public DateTime LastActive { get; set; }

        public DateTime? Birthday { get; set; }

        public string Location { get; set; }

        public string Signature { get; set; }
    }
}