using System.ComponentModel.DataAnnotations;

namespace PicoBoards.Forums.Commands
{
    public sealed class EditCategoryCommand : IValidatable
    {
        public int CategoryId { get; }

        [Required]
        public string Name { get; }

        public EditCategoryCommand(int categoryId, string name)
            => (CategoryId, Name) = (categoryId, name);
    }
}