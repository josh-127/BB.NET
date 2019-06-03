using System.Threading.Tasks;
using PicoBoards.Security.Models;
using PicoBoards.Security.Queries;
using Tortuga.Chain;

namespace PicoBoards.Security
{
    public sealed class GroupService
    {
        private readonly MySqlDataSource dataSource;

        public GroupService(MySqlDataSource dataSource)
            => this.dataSource = dataSource;

        public async Task<GroupListingTable> QueryAsync(GroupListingsQuery query)
            => new GroupListingTable(
                await dataSource
                .From("Group")
                .WithSorting(new SortExpression("Name"))
                .ToCollection<GroupListing>()
                .ExecuteAsync());
    }
}