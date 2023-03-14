using Microsoft.EntityFrameworkCore;

namespace Demo.Net.Infrast.Impl.SQLiteService
{
	public class SqliteDbContextOptionsFactory/* : IDbContextOptionsFactory*/
	{
		public DbContextOptions CreateContextOptions(string connection = "")
		{
			throw new NotImplementedException();
			//var connectionString = string.IsNullOrEmpty(connection) ? $@"Filename=..\Database\Orca.db" : $@"Filename={connection}";
			//var optionsBuilder = new DbContextOptionsBuilder<_context>().UseSqlite(connectionString);
			//return optionsBuilder.Options;
		}
	}
}
