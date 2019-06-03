using System.Threading.Tasks;
using Tortuga.Chain;

namespace PicoBoards.Security
{
    public sealed class GroupService
    {
        private readonly MySqlDataSource dataSource;

        public GroupService(MySqlDataSource dataSource)
            => this.dataSource = dataSource;

        public async Task<GroupListingTable> GetGroupListingsAsync()
        {
            var query = await dataSource
                .From("Group")
                .WithSorting(new SortExpression("Name"))
                .ToCollection<GroupListing>()
                .ExecuteAsync();

            return new GroupListingTable(query);
        }
    }
}