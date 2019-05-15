﻿using System.ComponentModel.DataAnnotations;

namespace BBNet.Web.ViewModels
{
    public class TopicNewViewModel
    {
        public int ForumId { get; set; }

        [Required]
        public string Title { get; set; }

        public string Description { get; set; }

        [Required]
        [Display(Name = "Body")]
        public string OpeningPostBody { get; set; }
    }
}