using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace PicoBoards.Web.Features.Forum.Forms
{
    public sealed class NewTopicForm
    {
        [HiddenInput]
        public int ForumId { get; set; }

        [Required]
        public string Name { get; set; }

        public string Description { get; set; }

        [DataType(DataType.MultilineText)]
        [Display(Name = "Body")]
        [Required]
        public string OpeningPostBody { get; set; }

        [Display(Name = "Enable Formatting")]
        public bool FormattingEnabled { get; set; } = true;

        [Display(Name = "Enable Smilies")]
        public bool SmiliesEnabled { get; set; } = true;

        [Display(Name = "Parse URLs")]
        public bool ParseUrls { get; set; } = true;

        [Display(Name = "Attach Signature")]
        public bool AttachSignature { get; set; } = true;

        public NewTopicForm() { }

        public NewTopicForm(int forumId)
            => ForumId = forumId;
    }
}