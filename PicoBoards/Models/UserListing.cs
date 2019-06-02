using System;
using System.ComponentModel.DataAnnotations;

namespace PicoBoards.Models
{
    public sealed class UserListing
    {
        [Display(Name = "Username")]
        public string UserName { get; set; }

        [Display(Name = "Group")]
        public string GroupName { get; set; }

        public string ProfileImageUrl { get; set; }

        [Display(Name = "Joined")]
        public DateTime Joined { get; set; }

        [Display(Name = "Last Active")]
        public DateTime LastActive { get; set; }

        [Display(Name = "Post Count")]
        public int PostCount { get; set; }

        [Display(Name = "Topic Count")]
        public int TopicCount { get; set; }
    }
}