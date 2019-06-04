using System.Linq;
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
                .ToCollection<CategoryListing>(CollectionOptions.InferConstructor)
                .ExecuteAsync());

        public async Task<CategoryDetailsCollection> QueryAsync(AllCategoryDetailsQuery query)
        {
            var categories = (Table) null;
            var forums = (Table) null;

            using (var transaction = await dataSource.BeginTransactionAsync())
            {
                categories = await transaction
                    .From("Category")
                    .ToTable()
                    .ExecuteAsync();

                forums = await transaction
                    .From("Forums")
                    .ToTable()
                    .ExecuteAsync();

                transaction.Commit();
            }

            var collection =
                from c in categories.Rows
                let id = (int) c["CategoryId"]
                select new CategoryDetails(
                    id,
                    (string) c["Name"],
                    new ForumListingCollection(
                        from f in forums.Rows
                        where (int) f["CategoryId"] == id
                        select new ForumListing(
                            (int) f["ForumId"],
                            (string) f["Name"],
                            (string) f["Description"],
                            (string) f["ImageUrl"]
                        )
                    )
                );

            return new CategoryDetailsCollection(collection);
        }

        public async Task ExecuteAsync(AddCategoryCommand command)
        {
            if (!command.IsValid())
                throw new CommandException("Invalid fields.");

            await dataSource
                .Insert("Category", command)
                .ExecuteAsync();
        }

        public async Task ExecuteAsync(RemoveCategoryCommand command)
        {
            if (!command.IsValid())
                throw new CommandException("Invalid fields.");

            using (var transaction = await dataSource.BeginTransactionAsync())
            {
                var hasForums = await transaction
                    .From("Forum", new { command.CategoryId })
                    .WithLimits(1)
                    .AsCount()
                    .ExecuteAsync() > 0;

                if (hasForums)
                {
                    transaction.Rollback();
                    throw new CommandException("This category is not empty.");
                }

                await transaction
                    .DeleteByKey("Category", command.CategoryId)
                    .ExecuteAsync();

                transaction.Commit();
            }
        }

        public async Task ExecuteAsync(EditCategoryCommand command)
        {
            if (!command.IsValid())
                throw new CommandException("Invalid fields.");

            await dataSource
                .Update("Category", command)
                .ExecuteAsync();
        }
    }
}