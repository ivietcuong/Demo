using Demo.Net.Core.DataSource;
using Demo.Net.Core.Entities;
using Demo.Net.Core.Interfaces;
using Demo.Net.Core.Services;
using Demo.Net.Infrast.Impl.SQLiteService;

using Microsoft.EntityFrameworkCore;

using System.Diagnostics;

namespace Demo.Infrast.Impl.SQLiteService.Test
{
	[TestFixture]
	public class SQLiteContextFixture
	{
		private IUnitOfWork _unitOfWork;
		private IPointService _pointService;
		private ISQLitePathService _pathService;
		private AsyncSQLiteRepository _asyncRepository;

		[SetUp]
		public void Setup()
		{
			_pathService = new TestSQLitePathService();

			//https://learn.microsoft.com/de-de/ef/core/testing/testing-without-the-database#inmemory-provider
			var optionsBuilder = new DbContextOptionsBuilder<SQLiteContext>();
			optionsBuilder.UseSqlite(_pathService.GetPath()).LogTo(Console.WriteLine);

			_unitOfWork = new SQLiteContext(optionsBuilder.Options);
			_asyncRepository = new AsyncSQLiteRepository(_unitOfWork);
			_pointService = new SQLitePointService(_asyncRepository);
		}

		[Test]
		public async Task Create_Database()
		{
			await ((SQLiteContext)_unitOfWork).Database.EnsureCreatedAsync();
			((SQLiteContext)_unitOfWork).Points.AddRange(Data.Points);
			var result = await _unitOfWork.SaveChangesAsync();

			Assert.That(Data.Points.Count, Is.EqualTo(result));
		}

		[Test]
		public async Task GetData_With_Set_From_UnitOfWork()
		{
			var result = await _unitOfWork.SetAsync<Point>();
			Assert.IsTrue(result.Any());
		}

		[Test]
		public async Task GetData_From_PointService()
		{
			var result = await _pointService.GetPointListAsync();
			Assert.IsTrue(result.Any());
		}
	}
}