using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PicoBoards.Forums.Commands;
using PicoBoards.Forums.Models;
using PicoBoards.Forums.Queries;
using PicoBoards.Security.Models;
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
            var topics = (Table) null;

            using (var transaction = await dataSource.BeginTransactionAsync())
            {
                forum = await transaction
                    .GetByKey("Forum", query.ForumId)
                    .ToRow()
                    .ExecuteAsync();

                topics = await transaction
                    .From("Topic", new { query.ForumId })
                    .ToTable()
                    .ExecuteAsync();

                transaction.Commit();
            }

            return new ForumDetails(
                (int) forum["ForumId"],
                (string) forum["Name"],
                (string) forum["Description"],
                (DateTime) forum["Created"],
                new TopicListingCollection(
                    from t in topics.Rows
                    select new TopicListing(
                        (int) t["TopicId"],
                        (string) t["Name"],
                        (string) t["Description"],
                        (sbyte) t["IsLocked"] != 0,
                        (sbyte) t["IsSticky"] != 0
                    )
                )
            );
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

        public async Task<TopicDetails> QueryAsync(TopicDetailsQuery query)
        {
            var topic = (Row) null;
            var posts = (IReadOnlyList<Row>) null;

            using (var transaction = await dataSource.BeginTransactionAsync())
            {
                topic =
                    await dataSource
                    .GetByKey("Topic", query.TopicId)
                    .ToRow()
                    .ExecuteAsync();

                posts =
                    (await dataSource
                    .From("vw_PostListing", new { query.TopicId })
                    .WithSorting(new SortExpression("Created"))
                    .ToTable()
                    .ExecuteAsync())
                    .Rows;

                transaction.Commit();
            }

            return new TopicDetails(
                (int) topic["TopicId"],
                (string) topic["Name"],
                (string) topic["Description"],
                (DateTime) topic["Created"],
                (sbyte) topic["IsLocked"] != 0,
                (sbyte) topic["IsSticky"] != 0,
                new PostListingCollection(
                    from p in posts
                    select new PostListing(
                        (int) p["PostId"],
                        (string) p["Name"],
                        (string) p["Body"],
                        (DateTime) p["Created"],
                        (DateTime) p["Modified"],
                        (sbyte) p["FormattingEnabled"] != 0,
                        (sbyte) p["SmiliesEnabled"] != 0,
                        (sbyte) p["ParseUrls"] != 0,
                        (sbyte) p["AttachSignature"] != 0,
                        new UserProfileSummary(
                            (int) p["UserId"],
                            (string) p["UserName"],
                            (string) p["Signature"]
                        )
                    )
                )
            ); ;
        }

        public async Task<int> ExecuteAsync(AddTopicCommand command)
        {
            if (!command.IsValid())
                throw new CommandException("Invalid fields.");

            using (var transaction = await dataSource.BeginTransactionAsync())
            {
                var topicId =
                    await transaction
                    .Insert("Topic", command)
                    .ExecuteAsync();

                if (topicId is null)
                {
                    transaction.Rollback();
                    throw new CommandException("Can't add topic.");
                }

                var postId =
                    await transaction
                    .Insert("Post", new
                    {
                        TopicId = topicId.Value,
                        UserId = command.AuthorUserId,
                        command.Name,
                        Body = command.OpeningPostBody,
                        command.FormattingEnabled,
                        command.SmiliesEnabled,
                        command.ParseUrls,
                        command.AttachSignature
                    })
                    .ExecuteAsync();

                if (postId is null)
                {
                    transaction.Rollback();
                    throw new CommandException("Can't add post.");
                }

                transaction.Commit();

                return topicId.Value;
            }
        }

        public async Task ExecuteAsync(RemoveTopicCommand command)
        {
            if (!command.IsValid())
                throw new CommandException("Invalid fields.");

            using (var transaction = await dataSource.BeginTransactionAsync())
            {
                var hasPosts =
                    await transaction
                    .From("Post", new { command.TopicId })
                    .WithLimits(1)
                    .AsCount()
                    .ExecuteAsync() > 0;

                if (hasPosts)
                {
                    transaction.Rollback();
                    throw new CommandException("This topic is not empty.");
                }

                var hasPolls =
                    await transaction
                    .From("Poll", new { command.TopicId })
                    .WithLimits(1)
                    .AsCount()
                    .ExecuteAsync() > 0;

                if (hasPolls)
                {
                    transaction.Rollback();
                    throw new CommandException("This topic is not empty.");
                }

                await transaction
                    .DeleteByKey("Topic", command.TopicId)
                    .ExecuteAsync();

                transaction.Commit();
            }
        }
    }
}