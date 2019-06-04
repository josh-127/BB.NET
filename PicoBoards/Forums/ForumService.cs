using System;
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

        public async Task<CategoryRefCollection> QueryAsync(CategoryRefsQuery query)
            => new CategoryRefCollection(
                await dataSource
                .From("Category")
                .ToCollection<CategoryRef>(CollectionOptions.InferConstructor)
                .ExecuteAsync());

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
                    .From("Forum")
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

        public async Task<ForumDetails> QueryAsync(ForumDetailsQuery query)
        {
            var forum = (Row) null;
            var topics = (TopicListingCollection) null;

            using (var transaction = await dataSource.BeginTransactionAsync())
            {
                forum = await transaction
                    .GetByKey("Forum", query.ForumId)
                    .ToRow()
                    .ExecuteAsync();

                topics = new TopicListingCollection(
                    await transaction
                    .From("Topic", query.ForumId)
                    .ToCollection<TopicListing>(CollectionOptions.InferConstructor)
                    .ExecuteAsync());

                transaction.Commit();
            }

            return new ForumDetails(
                (int) forum["ForumId"],
                (string) forum["Name"],
                (string) forum["Description"],
                (DateTime) forum["Created"],
                new TopicListingCollection(topics));
        }

        public async Task ExecuteAsync(AddForumCommand command)
        {
            if (!command.IsValid())
                throw new CommandException("Invalid fields.");

            var key =
                await dataSource
                .Insert("Forum", command)
                .ExecuteAsync();

            if (key is null)
                throw new CommandException("Invalid fields.");
        }

        public async Task ExecuteAsync(RemoveForumCommand command)
        {
            if (!command.IsValid())
                throw new CommandException("Invalid fields.");

            using (var transaction = await dataSource.BeginTransactionAsync())
            {
                var hasTopics =
                    await transaction
                    .From("Topic", new { command.ForumId })
                    .WithLimits(1)
                    .AsCount()
                    .ExecuteAsync() > 0;

                if (hasTopics)
                {
                    transaction.Rollback();
                    throw new CommandException("This forum is not empty.");
                }

                await transaction
                    .DeleteByKey("Forum", command.ForumId)
                    .ExecuteAsync();

                transaction.Commit();
            }
        }
    }
}