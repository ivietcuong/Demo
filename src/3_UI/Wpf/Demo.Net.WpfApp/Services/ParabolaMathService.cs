using Demo.NetStandard.Core.Entities;
using Demo.NetStandard.Core.Services;

using Microsoft.Extensions.Logging;

using System;
using System.Collections.Generic;
using System.Linq;

namespace Demo.Net.WpfApp.Services
{
	public class ParabolaMathService : IMathService
	{
		private readonly ILogger _logger;
		public string Name { get; set; } = "Parabola";
		public string Description { get; set; } = "y = a.x^{2} + b.x + c";

        public ParabolaMathService(ILogger<ParabolaMathService> logger)
		{
			_logger = logger;
		}
		public IEnumerable<Point> Calculate(IEnumerable<Point> points, double coefficienta, double coefficientb, double coefficientc)
		{
			var result = points.Select(p => new Point() { X = p.X, Y = coefficienta * Math.Pow(p.X, 2) + coefficientb * p.X + coefficientc });
			return result;
		}
	}
}
