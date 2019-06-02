﻿using System.ComponentModel.DataAnnotations;
using PicoBoards.Models;

namespace PicoBoards.Web.Models
{
    public sealed class RegistrationForm : Registration
    {
        [Compare(nameof(Password))]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }
    }
}