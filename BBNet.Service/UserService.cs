using System;
using BBNet.Service.Models;
using Tortuga.Chain;

namespace BBNet.Service
{
    public class UserService
    {
        private readonly MySqlDataSource dataSource;

        public UserService(MySqlDataSource dataSource)
            => this.dataSource = dataSource;

        public void RegisterUser(UserRegistrationModel model)
        {
            throw new NotImplementedException();
        }
    }
}