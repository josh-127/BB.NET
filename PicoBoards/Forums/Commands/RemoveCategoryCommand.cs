
namespace PicoBoards.Forums.Commands
{
    public sealed class RemoveCategoryCommand : IValidatable
    {
        public int CategoryId { get; }

        public RemoveCategoryCommand(int categoryId)
            => CategoryId = categoryId;
    }
}