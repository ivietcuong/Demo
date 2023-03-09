using Demo.NetStandard.Core.Entities;
using Demo.NetStandard.Core.Services;

using Microsoft.Extensions.Logging;

using System;
using System.Collections.Generic;
using System.Linq;

namespace Demo.Net.WpfApp.Services
{
	public class ExponentiationMathService : IMathService
	{
		private readonly ILogger _logger;
		public string Name { get; } = "Exponentiation";
		public string Description { get; } = "y = b^{n}";

		public ExponentiationMathService(ILogger<ExponentiationMathService> logger)
		{
			_logger = logger;
		}
		public IEnumerable<Point> Calculate(IEnumerable<Point> points, double coefficienta, double coefficientb, double coefficientc)
		{
			_logger.LogInformation($"{nameof(coefficienta)}: {coefficienta} - {nameof(coefficientb)}: {coefficientb} - {nameof(coefficientc)}: {coefficientc}");
			var result = points.Select(p => new Point() { X = p.X, Y = Math.Pow(p.X, coefficienta) });
			_logger.LogTrace($"{nameof(Calculate)}");
			return result;
		}
	}
}
