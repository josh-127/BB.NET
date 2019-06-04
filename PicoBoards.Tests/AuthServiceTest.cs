using System;
using System.IO;
using System.Threading.Tasks;
using PicoBoards.Security.Authentication;
using PicoBoards.Security.Authentication.Commands;
using PicoBoards.Security.Authentication.Queries;
using Tortuga.Chain;
using Xunit;

namespace PicoBoards.Tests
{
    public class AuthServiceTest
    {
        private async Task<MySqlDataSource> CreateDatabase()
        {
            var directory = Path.Combine(Environment.CurrentDirectory, "../../../../Database");
            var schemaPath = Path.Combine(directory, "schema.sql");
            var viewsPath = Path.Combine(directory, "views.sql");

            var schemaText = await File.ReadAllTextAsync(schemaPath);
            var viewsText = await File.ReadAllTextAsync(viewsPath);

            var dataSource = new MySqlDataSource(Configuration.ConnectionStringWithoutDatabase);
            await dataSource.Sql("DROP DATABASE IF EXISTS `PicoBoards`").ExecuteAsync();
            await dataSource.Sql(schemaText).ExecuteAsync();
            await dataSource.Sql(viewsText).ExecuteAsync();

            return new MySqlDataSource(Configuration.ConnectionString);
        }

        [Fact]
        public async Task SuccessfulCase()
        {
            var dataSource = await CreateDatabase();
            await SuccessfulCase_Impl(dataSource, "John_Smith@example1.com",         "John_Smith",         "password");
            await SuccessfulCase_Impl(dataSource, "Michelle_Cooper@example2.com",    "Michelle_Cooper",    "password");
            await SuccessfulCase_Impl(dataSource, "Theresa_Brown@example3.com",      "Theresa_Brown",      "password");
            await SuccessfulCase_Impl(dataSource, "Joyce_Robinson@example4.com",     "Joyce_Robinson",     "password");
            await SuccessfulCase_Impl(dataSource, "Lawrence_Jones@example5.com",     "Lawrence_Jones",     "password");
            await SuccessfulCase_Impl(dataSource, "Frank_Sanchez@example6.com",      "Frank_Sanchez",      "password");
            await SuccessfulCase_Impl(dataSource, "Phillip_Richardson@example7.com", "Phillip_Richardson", "password");
            await SuccessfulCase_Impl(dataSource, "David_Bailey@example8.com",       "David_Bailey",       "password");
            await SuccessfulCase_Impl(dataSource, "Jose_Parker@example9.com",        "Jose_Parker",        "password");
            await SuccessfulCase_Impl(dataSource, "Ralph_Perez@example10.com",       "Ralph_Perez",        "password");
        }

        private async Task SuccessfulCase_Impl(
            MySqlDataSource dataSource, string emailAddress, string userName, string password)
        {
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