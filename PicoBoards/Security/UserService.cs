using System.Linq;
using System.Threading.Tasks;
using PicoBoards.Security.Commands;
using PicoBoards.Security.Models;
using PicoBoards.Security.Queries;
using Tortuga.Chain;

namespace PicoBoards.Security
{
    public sealed class UserService
    {
        private readonly MySqlDataSource dataSource;

        public UserService(MySqlDataSource dataSource)
            => this.dataSource = dataSource;

        public async Task<UserProfileSettings> QueryAsync(UserProfileSettingsQuery query)
            => await dataSource
                .GetByKey("User", query.UserId)
                .ToObject<UserProfileSettings>()
                .ExecuteAsync();

        public async Task ExecuteAsync(EditUserProfileCommand command)
        {
            if (!command.IsValid())
                throw new CommandException("Invalid fields.");

            await dataSource
                .Update("User", command)
                .ExecuteAsync();
        }

        public async Task<UserAccountSettings> QueryAsync(UserAccountSettingsQuery query)
            => await dataSource
                .GetByKey("User", query.UserId)
                .ToObject<UserAccountSettings>()
                .ExecuteAsync();

        public async Task ExecuteAsync(EditUserAccountCommand command)
        {
            if (!command.IsValid())
                throw new CommandException("Invalid fields.");

            using (var transaction = await dataSource.BeginTransactionAsync())
            {
                var oldUserName = await transaction
                    .GetByKey("User", command.UserId)
                    .ToString("UserName")
                    .ExecuteAsync();

                if (command.UserName != oldUserName)
                {
                    var isUserNameTaken = await transaction
                        .From("User", new { command.UserName })
                        .AsCount()
                        .ExecuteAsync() > 0;

                    if (isUserNameTaken)
                    {
                        transaction.Commit();
                        throw new CommandException("User already exists.");
                    }
                }

                var password = await transaction
                    .From("User", new { command.UserId, Password = command.CurrentPassword })
                    .ToString("Password")
                    .ExecuteAsync();

                if (command.CurrentPassword != password)
                {
                    transaction.Commit();
                    throw new CommandException("Invalid password.");
                }

                await transaction
                    .Update("User", new
                    {
                        command.UserId,
                        command.EmailAddress,
                        command.UserName
                    })
                    .ExecuteAsync();

                if (!(command.NewPassword is null))
                {
                    await transaction
                        .Update("User", new
                        {
                            command.UserId,
                            Password = command.NewPassword
                        })
                        .ExecuteAsync();
                }

                transaction.Commit();
            }
        }

        public async Task<PublicUserProfileDetails> QueryAsync(PublicUserProfileQuery query)
            => (await dataSource
                .From("vw_UserProfileDetails", query)
                .WithLimits(1)
                .ToCollection<PublicUserProfileDetails>()
                .ExecuteAsync())
                .FirstOrDefault();

        public async Task<UserListingTable> QueryAsync(UserListingsQuery query)
            => new UserListingTable(
                await dataSource
                .From("User")
                .WithSorting(new SortExpression("UserName"))
                .ToCollection<UserListing>()
                .ExecuteAsync());
    }
}