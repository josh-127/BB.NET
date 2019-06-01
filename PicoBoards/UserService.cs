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
                .ToCollection<Wtf>()
                .ExecuteAsync();

            if (user.Count == 0)
                return false;

            return password == user[0].Password;
        }

        private sealed class Wtf
        {
            public string UserName { get; set; }
            public string Password { get; set; }
        }
    }
}