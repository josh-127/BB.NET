using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
    }
}