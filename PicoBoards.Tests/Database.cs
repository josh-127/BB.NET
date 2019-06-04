using System;
using System.IO;
using System.Threading.Tasks;
using Tortuga.Chain;

namespace PicoBoards.Tests
{
    public sealed class Database
    {
        public const string ConnectionString = "server=127.0.0.1;port=3306;database=picoboards;uid=root";
        public const string ConnectionStringWithoutDatabase = "server=127.0.0.1;port=3306;uid=root";

        public static async Task<MySqlDataSource> Create()
        {
            var directory = Path.Combine(Environment.CurrentDirectory, "../../../../Database");
            var schemaPath = Path.Combine(directory, "schema.sql");
            var viewsPath = Path.Combine(directory, "views.sql");

            var schemaText = await File.ReadAllTextAsync(schemaPath);
            var viewsText = await File.ReadAllTextAsync(viewsPath);

            var dataSource = new MySqlDataSource(ConnectionStringWithoutDatabase);
            await dataSource.Sql("DROP DATABASE IF EXISTS `PicoBoards`").ExecuteAsync();
            await dataSource.Sql(schemaText).ExecuteAsync();
            await dataSource.Sql(viewsText).ExecuteAsync();

            return new MySqlDataSource(ConnectionString);
        }
    }
}