using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using PicoBoards.Models;
using Tortuga.Chain;

namespace PicoBoards.Services
{
    public sealed class UserService
    {
        private readonly IClass2DataSource dataSource;

        public UserService(IClass2DataSource dataSource)
            => this.dataSource = dataSource;

        public async Task<UserProfileDetails> GetUserProfileAsync(string userName)
            => await dataSource
                .From("vw_UserProfileDetails", new { userName })
                .WithLimits(1)
                .ToObject<UserProfileDetails>(RowOptions.AllowEmptyResults)
                .ExecuteAsync();

        public async Task<UserListingTable> GetUserListingsAsync()
        {
            var query = await dataSource
                .From("User")
                .WithSorting(new SortExpression("UserName"))
                .ToCollection<UserListing>()
                .ExecuteAsync();

            return new UserListingTable(query);
        }

        public async Task<ValidationResultCollection> ValidateUserAsync(Login login)
        {
            var result = login.GetValidationResult();

            if (!result.IsValid)
                return result;

            var query = await dataSource
                .From("User", new { login.UserName })
                .WithLimits(1)
                .ToCollection<Login>()
                .ExecuteAsync();

            if (query.Count == 0 || query[0].Password != login.Password)
            {
                result.Add(new ValidationResult("Invalid credentials."));
                return result;
            }

            return result;
        }

        public async Task<ValidationResultCollection> RegisterUserAsync(Registration registration)
        {
            var result = registration.GetValidationResult();

            if (!result.IsValid)
                return result;

            var config = await dataSource
                .GetByKey("GlobalConfiguration", "0")
                .ToRow()
                .ReadOrCache("GlobalConfiguration")
                .ExecuteAsync();

            await dataSource.Insert("User", new
            {
                GroupId = config["DefaultGroupId"],
                registration.EmailAddress,
                registration.UserName,
                registration.Password
            }).ExecuteAsync();

            return result;
        }
    }
}