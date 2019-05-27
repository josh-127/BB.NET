using BBNet.Service.Models;
using Tortuga.Chain;

namespace BBNet.Service
{
    public sealed class UserService
    {
        private readonly IClass2DataSource dataSource;

        public UserService(IClass2DataSource dataSource)
            => this.dataSource = dataSource;

        public int? RegisterUser(UserRegistrationModel model)
        {
            if (!model.IsValid())
                return null;

            return dataSource.Insert("User", new
            {
                GroupId = 2,
                model.Email,
                model.UserName,
                model.Password
            }).Execute();
        }
    }
}