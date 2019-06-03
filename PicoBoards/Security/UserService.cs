using System.Linq;
using System.Threading.Tasks;
using PicoBoards.Security.Authentication;
using Tortuga.Chain;

namespace PicoBoards.Security
{
    public sealed class UserService
    {
        private readonly MySqlDataSource dataSource;

        public UserService(MySqlDataSource dataSource)
            => this.dataSource = dataSource;

        public UserEditorService BeginEdit(LoginToken token)
            => new UserEditorService(dataSource, token);

        public async Task<string> GetUserEmailAddressAsync(int userId)
            => await dataSource
                .GetByKey("User", userId)
                .ToString("EmailAddress")
                .ExecuteAsync();

        public async Task<UserProfileDetails> GetUserProfileAsync(int userId)
            => (await dataSource
                .From("vw_UserProfileDetails", new { userId })
                .WithLimits(1)
                .ToCollection<UserProfileDetails>()
                .ExecuteAsync())
                .FirstOrDefault();

        public async Task<UserListingTable> GetUserListingsAsync()
        {
            var query = await dataSource
                .From("User")
                .WithSorting(new SortExpression("UserName"))
                .ToCollection<UserListing>()
                .ExecuteAsync();

            return new UserListingTable(query);
        }
    }
}