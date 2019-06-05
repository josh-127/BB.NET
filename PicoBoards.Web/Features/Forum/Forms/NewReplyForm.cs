using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace PicoBoards.Web.Features.Forum.Forms
{
    public sealed class NewReplyForm
    {
        [HiddenInput]
        public int TopicId { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Body { get; set; }

        [Display(Name = "Enable Formatting")]
        public bool FormattingEnabled { get; set; } = true;

        [Display(Name = "Enable Smilies")]
        public bool SmiliesEnabled { get; set; } = true;

        [Display(Name = "Parse URLs")]
        public bool ParseUrls { get; set; } = true;

        [Display(Name = "Attach Signature")]
        public bool AttachSignature { get; set; } = true;

        public NewReplyForm() { }

        public NewReplyForm(int topicId, string name)
            => (TopicId, Name) = (topicId, name);
    }
}