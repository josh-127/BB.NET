using System.Threading.Tasks;
using Tortuga.Chain;

namespace PicoBoards.Services
{
    public sealed class UserService
    {
        private readonly IClass2DataSource dataSource;

        public UserService(IClass2DataSource dataSource)
            => this.dataSource = dataSource;

        public async Task<bool> ValidateUserAsync(string userName, string password)
        {
            var results = await dataSource.Sql(@"
                SELECT  `UserName`, `Password`
                FROM    `User`
                WHERE   `UserName` = @UserName
                LIMIT   1",
                new { UserName = userName })
                .ToDataTable()
                .ExecuteAsync();

            if (results.Rows.Count == 0)
                return false;

            var user = results.Rows[0];

            return password == (string) user["Password"];
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
    }
}