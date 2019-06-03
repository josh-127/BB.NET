using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using PicoBoards.Models;
using Tortuga.Chain;

namespace PicoBoards.Services
{
    public sealed class AuthService
    {
        private readonly MySqlDataSource dataSource;

        public AuthService(MySqlDataSource dataSource)
            => this.dataSource = dataSource;

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