using Demo.NetStandard.Core.Entities;

using System.Collections.Generic;
using System.Threading.Tasks;

namespace Demo.NetStandard.Core.Services
{
	public interface ICoordinateService
	{
		Task<IEnumerable<Point>> GetCoordinatesAsync();
	}
}
