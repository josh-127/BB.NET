using System;
using System.ComponentModel.DataAnnotations;

namespace PicoBoards.Web.Features.Ucp.Forms
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