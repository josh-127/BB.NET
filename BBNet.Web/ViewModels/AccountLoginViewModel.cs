﻿using System.ComponentModel.DataAnnotations;

namespace BBNet.Web.ViewModels
{
    public class AccountLoginViewModel
    {
        [Required]
        public string UserName { get; set; }

        [Required]
        public string Password { get; set; }
    }
}