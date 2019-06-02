using System.ComponentModel.DataAnnotations;

namespace PicoBoards.Web.Models
{
    public sealed class ChangeEmailAddressForm
    {
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Email Address")]
        [Required]
        public string EmailAddress { get; set; }

        public ChangeEmailAddressForm() { }

        public ChangeEmailAddressForm(string emailAddress)
            => EmailAddress = emailAddress;
    }
}