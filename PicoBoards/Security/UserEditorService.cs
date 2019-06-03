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
        private readonly LoginToken loginToken;

        public UserEditorService(MySqlDataSource dataSource, LoginToken loginToken)
            => (this.dataSource, this.loginToken) = (dataSource, loginToken);

        public void Dispose() { }

        public async Task SetEmailAddressAsync(string emailAddress)
        {
            var context = new ValidationContext(emailAddress) { MemberName = nameof(emailAddress) };
            var result = new ValidationResultCollection();
            Validator.TryValidateValue(emailAddress, context, result, new List<ValidationAttribute>
            {
                new EmailAddressAttribute(),
                new RequiredAttribute()
            });

            if (!result.IsValid)
                throw new EditorException("Invalid value.");

            await dataSource
                .Update("User", new { loginToken.UserId, emailAddress })
                .ExecuteAsync();
        }
    }
}