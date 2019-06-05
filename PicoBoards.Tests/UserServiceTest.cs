using System.Threading.Tasks;
using PicoBoards.Security;
using PicoBoards.Security.Commands;
using PicoBoards.Security.Queries;
using Tortuga.Chain;
using Xunit;

namespace PicoBoards.Tests
{
    public class UserServiceTest
    {
        [Fact]
        public async Task ValidateUser_SuccessfulCase()
        {
            var dataSource = await Database.Create();
            var userId = await RegisterUser_ExpectSuccess(
                dataSource, "John_Smith@example.com", "John_Smith", "password");

            var userService = new UserService(dataSource);
            var token = await userService.ValidateUserAsync(
                new LoginCredentials("John_Smith", "password"));

            Assert.Equal(userId, token.UserId);
            Assert.Equal("John_Smith@example.com", token.EmailAddress);
            Assert.Equal("John_Smith", token.UserName);
        }

        [Fact]
        public async Task ValidateUser_UserDoesNotExist()
        {
            var dataSource = await Database.Create();
            await RegisterUser_ExpectSuccess(
                dataSource, "John_Smith@example.com", "John_Smith", "password");

            var userService = new UserService(dataSource);

            await Assert.ThrowsAsync<AuthenticationException>(
                async () => await userService.ValidateUserAsync(
                    new LoginCredentials("Joyce_Robinson", "password")));
        }

        [Fact]
        public async Task ValidateUser_IncorrectPassword()
        {
            var dataSource = await Database.Create();
            await RegisterUser_ExpectSuccess(
                dataSource, "John_Smith@example.com", "John_Smith", "password");

            var userService = new UserService(dataSource);

            await Assert.ThrowsAsync<AuthenticationException>(
                async () => await userService.ValidateUserAsync(
                    new LoginCredentials("John_Smith", "1234567890")));
        }

        [Fact]
        public async Task RegisterUser_SuccessfulCase()
        {
            var dataSource = await Database.Create();
            await RegisterUser_ExpectSuccess(dataSource, "John_Smith@example1.com",         "John_Smith");
            await RegisterUser_ExpectSuccess(dataSource, "Michelle_Cooper@example2.com",    "Michelle_Cooper");
            await RegisterUser_ExpectSuccess(dataSource, "Theresa_Brown@example3.com",      "Theresa_Brown");
            await RegisterUser_ExpectSuccess(dataSource, "Joyce_Robinson@example4.com",     "Joyce_Robinson");
            await RegisterUser_ExpectSuccess(dataSource, "Lawrence_Jones@example5.com",     "Lawrence_Jones");
            await RegisterUser_ExpectSuccess(dataSource, "Frank_Sanchez@example6.com",      "Frank_Sanchez");
            await RegisterUser_ExpectSuccess(dataSource, "Phillip_Richardson@example7.com", "Phillip_Richardson");
            await RegisterUser_ExpectSuccess(dataSource, "David_Bailey@example8.com",       "David_Bailey");
            await RegisterUser_ExpectSuccess(dataSource, "Jose_Parker@example9.com",        "Jose_Parker");
            await RegisterUser_ExpectSuccess(dataSource, "Ralph_Perez@example10.com",       "Ralph_Perez");
        }

        [Fact]
        public async Task RegisterUser_SameUserName_UserAlreadyExists()
        {
            var dataSource = await Database.Create();
            await RegisterUser_ExpectSuccess(dataSource, "John_Smith@example1.com", "John_Smith");
            await RegisterUser_ExpectUserAlreadyExists(dataSource, "John_Smith@example1000.com", "John_Smith");
        }

        [Fact]
        public async Task RegisterUser_InvalidEmailAddress()
        {
            var dataSource = await Database.Create();
            var userService = new UserService(dataSource);

            await Assert.ThrowsAsync<CommandException>(
                async () => await userService.ExecuteAsync(
                    new RegisterUserCommand("John_Smith@", "John_Smith", "password")));
        }

        [Fact]
        public async Task RegisterUser_InvalidUserName()
        {
            var dataSource = await Database.Create();
            var userService = new UserService(dataSource);

            await Assert.ThrowsAsync<CommandException>(
                async () => await userService.ExecuteAsync(
                    new RegisterUserCommand("John_Smith@example.com", "John Smith", "password")));
        }

        private async Task<int> RegisterUser_ExpectSuccess(
            MySqlDataSource dataSource,
            string emailAddress,
            string userName,
            string password = "password")
        {
            var userService = new UserService(dataSource);

            var token = await userService
                .ExecuteAsync(new RegisterUserCommand(emailAddress, userName, password));

            Assert.Equal(emailAddress, token.EmailAddress);
            Assert.Equal(userName, token.UserName);

            var row = await dataSource
                .GetByKey("User", token.UserId)
                .ToRow()
                .ExecuteAsync();

            Assert.Equal(emailAddress, row["EmailAddress"]);
            Assert.Equal(userName, row["UserName"]);

            token = await userService
                .ValidateUserAsync(new LoginCredentials(userName, password));

            Assert.Equal(token.UserId, row["UserId"]);
            Assert.Equal(emailAddress, token.EmailAddress);
            Assert.Equal(userName, token.UserName);

            return token.UserId;
        }

        private async Task RegisterUser_ExpectUserAlreadyExists(
            MySqlDataSource dataSource, string emailAddress, string userName)
        {
            var userService = new UserService(dataSource);

            await Assert.ThrowsAsync<CommandException>(
                async () => await userService.ExecuteAsync(
                    new RegisterUserCommand(emailAddress, userName, "password")));
        }
    }
}