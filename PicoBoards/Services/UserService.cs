using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
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

        public async Task<UserProfileDetails> GetUserProfile(string userName)
        {
            var query = await dataSource
                .Sql(@"
                    SELECT  `UserName`,
                            (SELECT `Name`
                                FROM `Group`
                                WHERE `Group`.`GroupId` = `User`.`GroupId`
                                LIMIT 1)
                                AS `GroupName`,
                            `Created`,
                            `LastActive`,
                            `Birthday`,
                            `Location`,
                            `Signature`
                    FROM `User`
                    WHERE `UserName` = @userName
                    LIMIT 1", new { userName })
                .ToCollection<UserProfileDetails>()
                .ExecuteAsync();

            return query.FirstOrDefault();
        }

        public async Task<UserListingTable> GetUserListings()
        {
            var query = await dataSource
                .Sql(@"
                    SELECT  `UserName`,
                            (SELECT `Name`
                                FROM `Group`
                                WHERE `Group`.`GroupId` = `User`.`GroupId`
                                LIMIT 1)
                                AS `GroupName`,
                            `Created`,
                            `LastActive`
                    FROM `User`
                    ORDER BY `UserName` ASC", new { })
                .ToCollection<UserListing>()
                .ExecuteAsync();

            var table = new UserListingTable();

            foreach (var row in query)
                table.Add(row);

            return table;
        }

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