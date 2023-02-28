using Demo.NetStandard.Core.Entities;

using System.Collections.Generic;
using System.Threading.Tasks;

namespace Demo.NetStandard.Core.Services
{
	public interface IPointService
	{
		Task<IEnumerable<Point>> GetPointListAsync();
	}
}
