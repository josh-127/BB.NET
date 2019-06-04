using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using PicoBoards.Security.Authentication;
using PicoBoards.Security.Authentication.Commands;
using PicoBoards.Security.Authentication.Queries;
using Tortuga.Chain;
using Xunit;

namespace PicoBoards.Tests
{
    public class AuthServiceTest
    {
        [Fact]
        public async Task SuccessfulCase()
        {
            await SuccessfulCase_Impl("John_Smith@example1.com",         "John_Smith",         "password");
            await SuccessfulCase_Impl("Michelle_Cooper@example2.com",    "Michelle_Cooper",    "password");
            await SuccessfulCase_Impl("Theresa_Brown@example3.com",      "Theresa_Brown",      "password");
            await SuccessfulCase_Impl("Joyce_Robinson@example4.com",     "Joyce_Robinson",     "password");
            await SuccessfulCase_Impl("Lawrence_Jones@example5.com",     "Lawrence_Jones",     "password");
            await SuccessfulCase_Impl("Frank_Sanchez@example6.com",      "Frank_Sanchez",      "password");
            await SuccessfulCase_Impl("Phillip_Richardson@example7.com", "Phillip_Richardson", "password");
            await SuccessfulCase_Impl("David_Bailey@example8.com",       "David_Bailey",       "password");
            await SuccessfulCase_Impl("Jose_Parker@example9.com",        "Jose_Parker",        "password");
            await SuccessfulCase_Impl("Ralph_Perez@example10.com",       "Ralph_Perez",        "password");
        }

        private async Task SuccessfulCase_Impl(string emailAddress, string userName, string password)
        {
            var dataSource = new MySqlDataSource(Configuration.ConnectionString);
            var authService = new AuthService(dataSource);

            var token = await authService
                .ExecuteAsync(new RegisterUserCommand(emailAddress, userName, password));

            Assert.Equal(emailAddress, token.EmailAddress);
            Assert.Equal(userName, token.UserName);

            var row = await dataSource
                .GetByKey("User", token.UserId)
                .ToRow()
                .ExecuteAsync();

            Assert.Equal(emailAddress, row["EmailAddress"]);
            Assert.Equal(userName, row["UserName"]);

            token = await authService
                .ValidateUserAsync(new LoginCredentials(userName, password));

            Assert.Equal(token.UserId, row["UserId"]);
            Assert.Equal(emailAddress, token.EmailAddress);
            Assert.Equal(userName, token.UserName);

            await dataSource
                .DeleteByKey("User", token.UserId)
                .ExecuteAsync();
        }
    }
}