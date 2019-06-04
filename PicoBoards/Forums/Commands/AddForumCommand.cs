using System.ComponentModel.DataAnnotations;

namespace PicoBoards.Forums.Commands
{
    public sealed class AddForumCommand : IValidatable
    {
        [Required]
        public int CategoryId { get; }

        [Required]
        public string Name { get; }

        public string Description { get; }

        public string ImageUrl { get; }

        public AddForumCommand(int categoryId, string name, string description, string imageUrl)
        {
            CategoryId = categoryId;
            Name = name;
            Description = description;
            ImageUrl = imageUrl;
        }
    }
}