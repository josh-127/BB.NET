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

        public async Task<string> GetUserEmailAddressAsync(UserEmailAddressQuery query)
            => await dataSource
                .GetByKey("User", query.UserId)
                .ToString("EmailAddress")
                .ExecuteAsync();

        public async Task SetEmailAddressAsync(SetUserEmailAddressCommand command)
        {
            if (!command.IsValid())
                throw new CommandException("Invalid value.");

            await dataSource
                .Update("User", command)
                .ExecuteAsync();
        }

        public async Task<string> GetUserNameAsync(UserNameQuery query)
            => await dataSource
                .GetByKey("User", query.UserId)
                .ToString("UserName")
                .ExecuteAsync();

        public async Task SetUserNameAsync(SetUserNameCommand command)
        {
            if (!command.IsValid())
                throw new CommandException("Invalid value.");

            await dataSource
                .Update("User", command)
                .ExecuteAsync();
        }

        public async Task<UserProfileDetails> GetUserProfileAsync(UserProfileQuery query)
            => (await dataSource
                .From("vw_UserProfileDetails", query)
                .WithLimits(1)
                .ToCollection<UserProfileDetails>()
                .ExecuteAsync())
                .FirstOrDefault();

        public async Task<UserListingTable> GetUserListingsAsync(UserListingsQuery query)
            => new UserListingTable(
                await dataSource
                .From("User")
                .WithSorting(new SortExpression("UserName"))
                .ToCollection<UserListing>()
                .ExecuteAsync());
    }
}