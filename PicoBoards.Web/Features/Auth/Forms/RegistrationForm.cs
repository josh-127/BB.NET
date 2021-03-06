﻿using System.ComponentModel.DataAnnotations;
using PicoBoards.DataAnnotations;
using PicoBoards.Security.Commands;

namespace PicoBoards.Web.Features.Auth.Forms
{
    public sealed class RegistrationForm
    {
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Email Address", Prompt = "example@example.com")]
        [Required]
        public string EmailAddress { get; set; }

        [Identifier]
        [Display(Name = "Username", Prompt = "johnsmith")]
        [Required]
        public string UserName { get; set; }

        [DataType(DataType.Password)]
        [Required]
        public string Password { get; set; }

        [Compare(nameof(Password))]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm Password")]
        public string ConfirmPassword { get; set; }

        public RegisterUserCommand ToRegistration()
            => new RegisterUserCommand(EmailAddress, UserName, Password);
    }
}