using System;
using System.ComponentModel.DataAnnotations;

namespace PicoBoards.Web.Models
{
    public sealed class UserProfileForm
    {
        [DataType(DataType.Date)]
        public DateTime? Birthday { get; set; }

        public string Location { get; set; }

        [DataType(DataType.MultilineText)]
        public string Signature { get; set; }
    }
}