using System.Threading.Tasks;
using Tortuga.Chain;

namespace PicoBoards.Security.Authentication
{
    public sealed class AuthService
    {
        private readonly MySqlDataSource dataSource;

        public AuthService(MySqlDataSource dataSource)
            => this.dataSource = dataSource;

        public async Task<LoginToken> ValidateUserAsync(LoginCredentials login)
        {
            var query = await dataSource
                .From("User", new { login.UserName, login.Password })
                .WithLimits(1)
                .ToCollection<LoginToken>()
                .ExecuteAsync();

            return query.Count > 0
                ? query[0]
                : throw new AuthenticationException("Invalid credentials.");
        }

        public async Task<LoginToken> RegisterUserAsync(Registration registration)
        {
            if (!registration.IsValid())
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
                    registration.EmailAddress,
                    registration.UserName,
                    registration.Password
                })
                .ExecuteAsync();

            return userId.HasValue
                ? new LoginToken(userId.Value, registration.UserName)
                : throw new AuthenticationException("User already exists.");
        }
    }
}