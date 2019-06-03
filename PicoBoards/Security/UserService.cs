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

        public async Task<string> QueryAsync(UserEmailAddressQuery query)
            => await dataSource
                .GetByKey("User", query.UserId)
                .ToString("EmailAddress")
                .ExecuteAsync();

        public async Task ExecuteAsync(SetUserEmailAddressCommand command)
        {
            if (!command.IsValid())
                throw new CommandException("Invalid value.");

            await dataSource
                .Update("User", command)
                .ExecuteAsync();
        }

        public async Task<string> QueryAsync(UserNameQuery query)
            => await dataSource
                .GetByKey("User", query.UserId)
                .ToString("UserName")
                .ExecuteAsync();

        public async Task ExecuteAsync(SetUserNameCommand command)
        {
            if (!command.IsValid())
                throw new CommandException("Invalid value.");

            await dataSource
                .Update("User", command)
                .ExecuteAsync();
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