using System.Threading.Tasks;
using PicoBoards.Security.Authentication.Commands;
using PicoBoards.Security.Authentication.Models;
using PicoBoards.Security.Authentication.Queries;
using Tortuga.Chain;

namespace PicoBoards.Security.Authentication
{
    public sealed class AuthService
    {
        private readonly MySqlDataSource dataSource;

        public AuthService(MySqlDataSource dataSource)
            => this.dataSource = dataSource;

        public async Task<UserAccessToken> ValidateUserAsync(LoginCredentials login)
        {
            var query = await dataSource
                .From("User", new { login.UserName, login.Password })
                .WithLimits(1)
                .ToCollection<UserAccessToken>(CollectionOptions.InferConstructor)
                .ExecuteAsync();

            return query.Count > 0
                ? query[0]
                : throw new AuthenticationException("Invalid credentials.");
        }

        public async Task<UserAccessToken> ExecuteAsync(RegisterUserCommand command)
        {
            if (!command.IsValid())
                throw new AuthenticationException("Invalid fields.");

            using (var transaction = await dataSource.BeginTransactionAsync())
            {
                var defaultGroupId =
                    await transaction
                    .GetByKey("GlobalConfiguration", "0")
                    .ToInt32("DefaultGroupId")
                    .ReadOrCache("GlobalConfiguration")
                    .ExecuteAsync();

                var userAlreadyExists =
                    await transaction
                    .From("User", new { command.UserName })
                    .WithLimits(1)
                    .AsCount()
                    .ExecuteAsync() > 0;

                if (userAlreadyExists)
                {
                    transaction.Rollback();
                    throw new CommandException("User already exists.");
                }

                var userId =
                    await transaction
                    .Insert("User", new
                    {
                        GroupId = defaultGroupId,
                        command.EmailAddress,
                        command.UserName,
                        command.Password
                    })
                    .ToInt32()
                    .ExecuteAsync();

                transaction.Commit();
                return new UserAccessToken(userId, command.EmailAddress, command.UserName);
            }
        }
    }
}