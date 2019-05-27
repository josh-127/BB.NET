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

            var config = dataSource.GetByKey("GlobalConfiguration", "0")
                .ToObject<GlobalConfiguration>()
                .ReadOrCache("GlobalConfiguration")
                .Execute();

            return dataSource.Insert("User", new
            {
                GroupId = config.DefaultGroupId,
                model.Email,
                model.UserName,
                model.Password
            }).Execute();
        }
    }
}