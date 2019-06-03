using System;
using System.ComponentModel.DataAnnotations;

namespace PicoBoards.Web.ViewModels
{
    public sealed class EditUserProfileForm
    {
        [DataType(DataType.Date)]
        public DateTime? Birthday { get; set; }

        public string Location { get; set; }

        [DataType(DataType.MultilineText)]
        public string Signature { get; set; }
    }
}