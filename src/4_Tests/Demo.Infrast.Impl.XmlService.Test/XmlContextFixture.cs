using Demo.NetStandard.Core.Data;
using Demo.NetStandard.Core.Interfaces;
using Demo.NetStandard.Infrast.XmlService.Impl;

namespace Demo.Infrast.Impl.XmlService.Test
{
	[TestFixture]
	public class XmlContextFixture
	{
		private readonly IUnitOfWork _unitOfWork;

		public XmlContextFixture()
		{
			_unitOfWork = new XmlContext();
		}

		[Test]
		public async Task IsWrittingXmlFile_Successful()
		{
			await ((XmlContext)_unitOfWork).WriteXmlDataAsync();
			var result = File.Exists(@"..\..\..\..\..\..\DataSource\points.xml");
			Assert.True(result);
		}

		[Test]
		public async Task DataCollectionInJsonFile_IsTheSame_AsSourceCollection()
		{
			var result = await ((XmlContext)_unitOfWork).ReadXmlDataAsync();
			Assert.True(result.Any());
			Assert.That(result.Count(), Is.EqualTo(Data.Points.Count));
		}
	}
}