using Demo.NetStandard.Core.Entities;

using System.Collections.Generic;

namespace Demo.NetStandard.Core.Services
{
	public interface IMathService
	{
		string Name { get; }
		string Description { get; }
		IEnumerable<Point> Calculate(IEnumerable<Point> points, double coefficienta, double coefficientb, double coefficientc);
	}
}
