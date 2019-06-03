using System;
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

        public UserEditorService BeginEdit(UserAccessToken token)
            => new UserEditorService(dataSource, token);

        public async Task<string> GetUserEmailAddressAsync(int userId)
            => await dataSource
                .GetByKey("User", userId)
                .ToString("EmailAddress")
                .ExecuteAsync();

        public async Task<string> GetUserNameAsync(int userId)
            => await dataSource
                .GetByKey("User", userId)
                .ToString("UserName")
                .ExecuteAsync();

        public async Task<DateTime?> GetUserBirthdayAsync(int userId)
            => await dataSource
                .GetByKey("User", userId)
                .ToDateTimeOrNull("Birthday")
                .ExecuteAsync();

        public async Task<string> GetUserLocationAsync(int userId)
            => await dataSource
                .GetByKey("User", userId)
                .ToString("Location")
                .ExecuteAsync();

        public async Task<string> GetUserSignatureAsync(int userId)
            => await dataSource
                .GetByKey("User", userId)
                .ToString("Signature")
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