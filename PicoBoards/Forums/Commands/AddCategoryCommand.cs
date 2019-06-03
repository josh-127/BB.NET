using System.ComponentModel.DataAnnotations;

namespace PicoBoards.Forums.Commands
{
    public sealed class AddCategoryCommand : IValidatable
    {
        [Required]
        public string Name { get; }

        public AddCategoryCommand(string name)
            => Name = name;
    }
}