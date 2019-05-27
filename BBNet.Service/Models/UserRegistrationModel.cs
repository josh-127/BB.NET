using System.ComponentModel.DataAnnotations;

namespace BBNet.Service.Models
{
    public class UserRegistrationModel
    {
        [EmailAddress]
        [Required]
        public string Email { get; set; }

        [Required]
        public string UserName { get; set; }

        [Required]
        public string Password { get; set; }
    }
}