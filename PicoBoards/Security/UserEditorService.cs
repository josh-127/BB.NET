using System;
using System.Threading.Tasks;
using PicoBoards.Security.Authentication;
using Tortuga.Chain;

namespace PicoBoards.Security
{
    public sealed class UserEditorService : IDisposable
    {
        private readonly MySqlDataSource dataSource;
        private readonly UserAccessToken accessToken;

        public UserEditorService(MySqlDataSource dataSource, UserAccessToken accessToken)
            => (this.dataSource, this.accessToken) = (dataSource, accessToken);

        public void Dispose() { }

        public async Task SetEmailAddressAsync(string value)
        {
            if (!value.IsValidEmailAddress())
                throw new EditorException("Invalid value.");

            await dataSource
                .Update("User", new { accessToken.UserId, EmailAddress = value })
                .ExecuteAsync();
        }

        public async Task SetUserNameAsync(string value)
        {
            if (!value.IsValidUserName())
                throw new EditorException("Invalid value.");

            await dataSource
                .Update("User", new { accessToken.UserId, UserName = value })
                .ExecuteAsync();
        }

        public async Task SetPasswordAsync(string value)
        {
            if (value.Length == 0)
                throw new EditorException("Invalid value.");

            await dataSource
                .Update("User", new { accessToken.UserId, Password = value })
                .ExecuteAsync();
        }

        public async Task SetBirthdayAsync(DateTime? value)
        {
            await dataSource
                .Update("User", new { accessToken.UserId, Birthday = value })
                .ExecuteAsync();
        }

        public async Task SetLocationAsync(string value)
        {
            await dataSource
                .Update("User", new { accessToken.UserId, Location = value })
                .ExecuteAsync();
        }

        public async Task SetSignatureAsync(string value)
        {
            await dataSource
                .Update("User", new { accessToken.UserId, Signature = value })
                .ExecuteAsync();
        }
    }
}