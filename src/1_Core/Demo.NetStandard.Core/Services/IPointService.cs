using Demo.Net.Core.Entities;

using System.Collections.Generic;
using System.Threading.Tasks;

namespace Demo.Net.Core.Services
{
	public interface IPointService
	{
		Task<IEnumerable<Point>> GetPointListAsync();
	}
}
