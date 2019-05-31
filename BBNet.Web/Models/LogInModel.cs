using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace BBNet.Web.Models
{
    public class LogInModel
    {
        [Required]
        public string UserName { get; set; }

        [DataType(DataType.Password)]
        [Required]
        public string Password { get; set; }

        [HiddenInput]
        public string ReturnUrl { get; set; }
    }
}