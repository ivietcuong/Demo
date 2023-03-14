using Demo.Net.Core.Entities;
using Demo.Net.Core.Interfaces;
using Demo.Net.Core.Services;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Demo.Net.Infrast.Impl.SQLiteService
{
	public class SQLiteContext : DbContext, IUnitOfWork
	{
		private readonly ILogger? _logger;
		private readonly IPathService? _pathService;

		public virtual DbSet<Point> Points { get; set; }

        public SQLiteContext()
        {
            
        }

        public SQLiteContext(DbContextOptions<SQLiteContext> options)
			: base(options) 
		{
		}

		public SQLiteContext(ISQLitePathService pathService, ILogger<SQLiteContext>? logger = null)
		{
			_logger = logger;
			_pathService = pathService;
		}

		public Task<IEnumerable<T>> SetAsync<T>() where T : class
		{
			var query = base.Set<T>();
			return Task.FromResult(query.AsEnumerable<T>());
		}
	}
}
