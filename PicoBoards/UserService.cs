using System.Threading.Tasks;
using Tortuga.Chain;

namespace PicoBoards
{
    public sealed class UserService
    {
        private readonly IClass2DataSource dataSource;

        public UserService(IClass2DataSource dataSource)
            => this.dataSource = dataSource;

        public async Task<bool> ValidateUserAsync(string userName, string password)
        {
            var user = await dataSource.Sql(@"
                SELECT  `UserName`, `Password`
                FROM    `User`
                WHERE   `UserName` = @UserName
                LIMIT   2",
                new { UserName = userName })
                .ToCollection<Login>()
                .ExecuteAsync();

            if (user.Count == 0)
                return false;

            return password == user[0].Password;
        }

        public async Task RegisterUser(string email, string userName, string password)
        {
            var config = await dataSource
                .GetByKey("GlobalConfiguration", "0")
                .ToDataRow()
                .ReadOrCache("GlobalConfiguration")
                .ExecuteAsync();

            await dataSource.Insert("User", new
            {
                GroupId = config["DefaultGroupId"],
                Email = email,
                UserName = userName,
                Password = password
            }).ExecuteAsync();
        }

        private sealed class Login
        {
            public string UserName { get; set; }
            public string Password { get; set; }
        }
    }
}