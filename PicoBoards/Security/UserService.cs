﻿using System;
using System.Linq;
using System.Threading.Tasks;
using PicoBoards.Security.Authentication;
using PicoBoards.Security.Models;
using PicoBoards.Security.Queries;
using Tortuga.Chain;

namespace PicoBoards.Security
{
    public sealed class UserService
    {
        private readonly MySqlDataSource dataSource;

        public UserService(MySqlDataSource dataSource)
            => this.dataSource = dataSource;

        public UserEditorService BeginEdit(UserAccessToken token)
            => new UserEditorService(dataSource, token);

        public async Task<string> GetUserEmailAddressAsync(int userId)
            => await dataSource
                .GetByKey("User", userId)
                .ToString("EmailAddress")
                .ExecuteAsync();

        public async Task<string> GetUserNameAsync(int userId)
            => await dataSource
                .GetByKey("User", userId)
                .ToString("UserName")
                .ExecuteAsync();

        public async Task<UserProfileDetails> GetUserProfileAsync(UserProfileQuery query)
            => (await dataSource
                .From("vw_UserProfileDetails", query)
                .WithLimits(1)
                .ToCollection<UserProfileDetails>()
                .ExecuteAsync())
                .FirstOrDefault();

        public async Task<UserListingTable> GetUserListingsAsync(UserListingsQuery query)
            => new UserListingTable(
                await dataSource
                .From("User")
                .WithSorting(new SortExpression("UserName"))
                .ToCollection<UserListing>()
                .ExecuteAsync());
    }
}