using Demo.NetStandard.Core.Entities;
using Demo.NetStandard.Core.Interfaces;

using Microsoft.EntityFrameworkCore;

using System.Diagnostics;

namespace Demo.Net.Infrast.Impl.SQLiteService
{
	public class SQLiteContext : DbContext, IUnitOfWork
	{

		public virtual DbSet<Point> Points { get; set; }

		public SQLiteContext()
		{
		}

		public SQLiteContext(DbContextOptions<SQLiteContext> options)
			: base(options) 
		{
		}

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			base.OnConfiguring(optionsBuilder);
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);
			modelBuilder.Entity<Point>().ToTable(nameof(Point));
			modelBuilder.Entity<Point>().HasKey(e => e.ID);
		}


		public Task<IEnumerable<T>> SetAsync<T>() where T : class
		{
			var query = base.Set<T>();
			return Task.FromResult(query.AsEnumerable());
		}

        public override void Dispose()
        {
            Debug.WriteLine($"{ContextId} context disposed.");
            base.Dispose();
        }

        public override ValueTask DisposeAsync()
        {
            Debug.WriteLine($"{ContextId} context disposed async.");
            return base.DisposeAsync();
        }
    }
}
