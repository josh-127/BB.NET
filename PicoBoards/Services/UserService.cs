using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using PicoBoards.Models;
using Tortuga.Chain;

namespace PicoBoards.Services
{
    public sealed class UserService
    {
        private readonly MySqlDataSource dataSource;

        public UserService(MySqlDataSource dataSource)
            => this.dataSource = dataSource;

        public async Task<string> GetUserEmailAddressAsync(int userId)
            => await dataSource
            .GetByKey("User", userId)
            .ToString("EmailAddress")
            .ExecuteAsync();

        public async Task<ValidationResultCollection> SetUserEmailAddressAsync(
            int userId, string emailAddress)
        {
            var context = new ValidationContext(emailAddress) { MemberName = nameof(emailAddress) };
            var result = new ValidationResultCollection();
            Validator.TryValidateValue(emailAddress, context, result, new List<ValidationAttribute>
            {
                new EmailAddressAttribute(),
                new RequiredAttribute()
            });

            if (!result.IsValid)
                return result;

            await dataSource
                .Update("User", new { userId, emailAddress })
                .ExecuteAsync();

            return result;
        }

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