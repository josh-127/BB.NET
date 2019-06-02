using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using PicoBoards.Models;
using Tortuga.Chain;

namespace PicoBoards.Services
{
    public sealed class UserService
    {
        private readonly IClass2DataSource dataSource;

        public UserService(IClass2DataSource dataSource)
            => this.dataSource = dataSource;

        public async Task<ValidationResultCollection> ValidateUserAsync(Login login)
        {
            var result = login.GetValidationResult();

            if (!result.IsValid)
                return result;

            var query = await dataSource
                .From("User", new { login.UserName })
                .WithLimits(1)
                .ToTable()
                .ExecuteAsync();

            if (query.Rows.Count == 0)
            {
                result.Add(new ValidationResult("Invalid credentials."));
                return result;
            }

            if (login.Password != (string) query.Rows[0]["Password"])
                result.Add(new ValidationResult("Invalid credentials."));

            return result;
        }

        public async Task<ValidationResultCollection> RegisterUser(Registration registration)
        {
            var result = registration.GetValidationResult();

            if (!result.IsValid)
                return result;

            var config = await dataSource
                .GetByKey("GlobalConfiguration", "0")
                .ToRow()
                .ReadOrCache("GlobalConfiguration")
                .ExecuteAsync();

            await dataSource.Insert("User", new
            {
                GroupId = config["DefaultGroupId"],
                registration.EmailAddress,
                registration.UserName,
                registration.Password
            }).ExecuteAsync();

            return result;
        }
    }
}