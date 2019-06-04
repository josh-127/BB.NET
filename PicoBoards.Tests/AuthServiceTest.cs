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
        public void SuccessfulCase()
        {
            SuccessfulCase_Impl("John_Smith@example1.com",         "John_Smith",         "password");
            SuccessfulCase_Impl("Michelle_Cooper@example2.com",    "Michelle_Cooper",    "password");
            SuccessfulCase_Impl("Theresa_Brown@example3.com",      "Theresa_Brown",      "password");
            SuccessfulCase_Impl("Joyce_Robinson@example4.com",     "Joyce_Robinson",     "password");
            SuccessfulCase_Impl("Lawrence_Jones@example5.com",     "Lawrence_Jones",     "password");
            SuccessfulCase_Impl("Frank_Sanchez@example6.com",      "Frank_Sanchez",      "password");
            SuccessfulCase_Impl("Phillip_Richardson@example7.com", "Phillip_Richardson", "password");
            SuccessfulCase_Impl("David_Bailey@example8.com",       "David_Bailey",       "password");
            SuccessfulCase_Impl("Jose_Parker@example9.com",        "Jose_Parker",        "password");
            SuccessfulCase_Impl("Ralph_Perez@example10.com",       "Ralph_Perez",        "password");
        }

        private void SuccessfulCase_Impl(string emailAddress, string userName, string password)
        {
            var dataSource = new MySqlDataSource(Configuration.ConnectionString);
            var authService = new AuthService(dataSource);

            using (var transaction = dataSource.BeginTransaction())
            {
                var token = authService
                    .ExecuteAsync(new RegisterUserCommand(emailAddress, userName, password))
                    .Result;

                Assert.Equal(emailAddress, token.EmailAddress);
                Assert.Equal(userName, token.UserName);

                var row = transaction
                    .GetByKey("User", token.UserId)
                    .ToRow()
                    .Execute();

                Assert.Equal(emailAddress, row["EmailAddress"]);
                Assert.Equal(userName, row["UserName"]);

                token = authService
                    .ValidateUserAsync(new LoginCredentials(userName, password))
                    .Result;

                Assert.Equal(token.UserId, row["UserId"]);
                Assert.Equal(emailAddress, token.EmailAddress);
                Assert.Equal(userName, token.UserName);

                transaction.Rollback();
            }
        }
    }
}