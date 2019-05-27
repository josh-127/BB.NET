using BBNet.Service.Models;
using Tortuga.Chain;

namespace BBNet.Service
{
    public class UserService
    {
        private readonly MySqlDataSource dataSource;

        public UserService(MySqlDataSource dataSource)
            => this.dataSource = dataSource;

        public int? RegisterUser(UserRegistrationModel model)
            => dataSource.Insert("User", new
            {
                GroupId = 2,
                model.Email,
                model.UserName,
                model.Password
            }).Execute();
    }
}