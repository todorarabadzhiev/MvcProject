﻿using System.ComponentModel.DataAnnotations;
using CommonUtilities.Utilities;

namespace WildCampingWithMvc.Models.Account
{
    public class ForgotViewModel
    {
        [Required]
        [Display(Name = Messages.TextEmail)]
        public string Email { get; set; }
    }
}