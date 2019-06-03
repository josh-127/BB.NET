using System.Threading.Tasks;
using PicoBoards.Forums.Commands;
using PicoBoards.Forums.Models;
using PicoBoards.Forums.Queries;
using Tortuga.Chain;

namespace PicoBoards.Forums
{
    public sealed class ForumService
    {
        private readonly MySqlDataSource dataSource;

        public ForumService(MySqlDataSource dataSource)
            => this.dataSource = dataSource;

        public async Task<CategoryListingCollection> QueryAsync(CategoryListingsQuery query)
            => new CategoryListingCollection(
                await dataSource
                .From("Category")
                .ToCollection<CategoryListing>()
                .ExecuteAsync());

        public async Task ExecuteAsync(AddCategoryCommand command)
        {
            if (!command.IsValid())
                throw new CommandException("Invalid fields.");

            await dataSource
                .Insert("Category", command)
                .ExecuteAsync();
        }
    }
}