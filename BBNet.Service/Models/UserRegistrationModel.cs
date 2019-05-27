﻿using System.ComponentModel.DataAnnotations;

namespace BBNet.Service.Models
{
    public sealed class UserRegistrationModel
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