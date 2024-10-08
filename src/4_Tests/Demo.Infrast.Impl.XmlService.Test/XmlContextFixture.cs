using Demo.NetStandard.Core.DataSource;
using Demo.NetStandard.Core.Entities;
using Demo.NetStandard.Core.Interfaces;
using Demo.NetStandard.Core.Services;
using Demo.Net.Infrast.Impl.XmlService;

using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;

namespace Demo.Infrast.Impl.XmlService.Test
{
	[TestFixture]
	public class XmlContextFixture
	{
		private IUnitOfWork _unitOfWork;
		private IPointService _pointService;
		private ILogger<XmlContext> _logger;
		private IXmlPathService _pathService;
		private AsyncXmlRepository _asyncRepository;

		public XmlContextFixture()
		{
			_logger = new NullLogger<XmlContext>();
			_pathService = new TestXmlPathService();
			_unitOfWork = new XmlContext(_pathService, _logger);
			_asyncRepository = new AsyncXmlRepository(_unitOfWork);
			_pointService = new XmlPointService(_asyncRepository);
		}

		[Test]
		public async Task IsWrittingXmlFile_Successful()
		{
			await ((XmlContext)_unitOfWork).WriteXmlDataAsync();
			var result = File.Exists(_pathService.GetPath());
			Assert.Equals(result, true);
		}

		[Test]
		public async Task DataCollectionInJsonFile_IsTheSame_AsSourceCollection()
		{
			var result = await ((XmlContext)_unitOfWork).ReadXmlDataAsync();
			Assert.Equals(result.Any(), true);
			Assert.That(result.Count(), Is.EqualTo(Data.Points.Count));
		}

		[Test]
		public async Task PointSet_Is_Not_Empty()
		{
			var result = await _unitOfWork.SetAsync<Point>();
			Assert.Equals(result.Any(), true);
			Assert.That(result.Count(), Is.EqualTo(Data.Points.Count));
		}

		[Test]
		public async Task Points_From_Repository_Are_Equal_To_Points_From_Context()
		{
			var pointsInContext = await _unitOfWork.SetAsync<Point>();
			var pointsInRepo = await _asyncRepository.GetAsync<Point>();

			Assert.That(pointsInRepo.Count(), Is.EqualTo(pointsInContext.Count()));
		}

		[Test]
		public void Throw_Exception_When_Call_AddAsync()
		{
			Assert.ThrowsAsync<NotImplementedException>(async () => { await _asyncRepository.AddAsync<Point>(new Point()); });
		}

		[Test]
		public async Task GetPointsFromService()
		{
			var result = await _pointService.GetPointListAsync();
			Assert.Equals(result.Any(), true);
		}
	}
}