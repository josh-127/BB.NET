using BBNet.Service.Models;
using Tortuga.Chain;
using Xunit;

namespace BBNet.Service.Tests
{
    public class UserServiceTest
    {
        private static IClass2DataSource CreateDataSource()
            => new MySqlDataSource(Configuration.ConnectionString);

        [Fact]
        public void RegisterUser_SuccessfulCase()
        {
            var dataSource = CreateDataSource();
            var service = new UserService(dataSource);

            var key = service.RegisterUser(new UserRegistrationModel
            {
                Email = "johnsmith@example.com",
                UserName = "JohnSmith",
                Password = "password"
            }).Value;

            var row = dataSource.GetByKey("User", key).ToDynamicObject().Execute();

            Assert.Equal(key, row.UserId);
            Assert.Equal("johnsmith@example.com", row.Email);
            Assert.Equal("JohnSmith", row.UserName);
            Assert.Equal("password", row.Password);
        }

        [Fact]
        public void RegisterUser_MissingAllEntries()
        {
            var dataSource = CreateDataSource();
            var service = new UserService(dataSource);

            var key = service.RegisterUser(new UserRegistrationModel());
            Assert.False(key.HasValue);
        }

        [Fact]
        public void RegisterUser_InvalidEmail()
        {
            var dataSource = CreateDataSource();
            var service = new UserService(dataSource);

            var key = service.RegisterUser(new UserRegistrationModel
            {
                Email = "invalid_email",
                UserName = "JohnSmith",
                Password = "password"
            });

            Assert.False(key.HasValue);
        }

        [Fact]
        public void RegisterUser_InvalidUserName()
        {
            var dataSource = CreateDataSource();
            var service = new UserService(dataSource);

            var key = service.RegisterUser(new UserRegistrationModel
            {
                Email = "johnsmith@example.com",
                UserName = "Invalid UserName",
                Password = "password"
            });

            Assert.False(key.HasValue);
        }
    }
}