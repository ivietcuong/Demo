using Demo.NetStandard.Core.Entities;

using System.Collections.Generic;

namespace Demo.NetStandard.Core.Services
{
	public interface IMathService
	{
		string Name { get; set; }
		string Description { get; set; }
		string Validate(double coefficienta, double coefficientb, double coefficientc);
		IEnumerable<Point> Calculate(IEnumerable<Point> points, double coefficienta, double coefficientb, double coefficientc);
	}
}
