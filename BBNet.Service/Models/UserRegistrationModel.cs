using BBNet.Service.DataAnnotations;
using System.ComponentModel.DataAnnotations;

namespace BBNet.Service.Models
{
    public sealed class UserRegistrationModel : IModel
    {
        [EmailAddress]
        [Required]
        public string Email { get; set; }

        [NoWhitespace]
        [Required]
        public string UserName { get; set; }

        [Required]
        public string Password { get; set; }
    }
}