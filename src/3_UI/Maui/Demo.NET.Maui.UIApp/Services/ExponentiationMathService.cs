using Demo.NetStandard.Core.Services;

using Microsoft.Extensions.Logging;

using Point = Demo.NetStandard.Core.Entities.Point;

namespace Demo.Net.Maui.UIApp.Services
{
	public class ExponentiationMathService : IMathService
	{
		private readonly ILogger _logger;
		public string Name { get; set; } = "Exponentiation";
		public string Description { get; set; } = "power.png";

		public ExponentiationMathService(ILogger<ExponentiationMathService> logger)
		{
			_logger = logger;
		}
		
		public IEnumerable<Point> Calculate(IEnumerable<Point> points, double coefficientA, double coefficientB, double coefficientC)
		{
			_logger.LogInformation($"{nameof(coefficientA)}: {coefficientA} - {nameof(coefficientB)}: {coefficientB} - {nameof(coefficientC)}: {coefficientC}");
			var result = points.Select(p => new Point() { X = p.X, Y = Math.Pow(p.X, coefficientA) });
			_logger.LogTrace($"{nameof(Calculate)}");
			return result;
		}

		public string Validate(double coefficientA, double coefficientB, double coefficientC)
		{
			return string.Empty;
		}
	}
}
