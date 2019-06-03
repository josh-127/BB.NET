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
                .ToCollection<UserAccessToken>()
                .ExecuteAsync();

            return query.Count > 0
                ? query[0]
                : throw new AuthenticationException("Invalid credentials.");
        }

        public async Task<UserAccessToken> ExecuteAsync(RegisterUserCommand command)
        {
            if (!command.IsValid())
                throw new AuthenticationException("Invalid fields.");

            var defaultGroupId = await dataSource
                .GetByKey("GlobalConfiguration", "0")
                .ToInt32("DefaultGroupId")
                .ReadOrCache("GlobalConfiguration")
                .ExecuteAsync();

            var userId = await dataSource
                .Insert("User", new
                {
                    GroupId = defaultGroupId,
                    command.EmailAddress,
                    command.UserName,
                    command.Password
                })
                .ExecuteAsync();

            return userId.HasValue
                ? new UserAccessToken(userId.Value, command.UserName)
                : throw new AuthenticationException("User already exists.");
        }
    }
}