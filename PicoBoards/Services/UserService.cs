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

        public async Task<LoginResult> ValidateUserAsync(Login login)
        {
            var result = login.GetValidationResult();

            if (!result.IsValid)
                return new LoginResult(result);

            var query = await dataSource
                .From("User", new { login.UserName, login.Password })
                .WithLimits(1)
                .ToCollection<LoginToken>()
                .ExecuteAsync();

            if (query.Count == 0)
            {
                result.Add(new ValidationResult("Invalid credentials."));
                return new LoginResult(result);
            }

            return new LoginResult(query[0]);
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

            using (var transaction = await dataSource.BeginTransactionAsync())
            {
                var count = await transaction
                    .From("User", new { registration.UserName })
                    .WithLimits(1)
                    .AsCount()
                    .ExecuteAsync();

                if (count == 0)
                {
                    await transaction
                        .Insert("User", new
                        {
                            GroupId = config["DefaultGroupId"],
                            registration.EmailAddress,
                            registration.UserName,
                            registration.Password
                        })
                        .ExecuteAsync();
                }
                else
                {
                    result.Add(new ValidationResult("User already exists."));
                }

                transaction.Commit();
            }

            return result;
        }
    }
}