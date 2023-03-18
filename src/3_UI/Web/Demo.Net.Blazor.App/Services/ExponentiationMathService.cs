using Point = Demo.NetStandard.Core.Entities.Point;

namespace Demo.Net.Blazor.App.Services
{
	public class ExponentiationMathService : IExponentiationMathService
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

            _logger.LogTrace($"{GetType().FullName}: {nameof(Calculate)}");
            return result;
		}

		public string Validate(double coefficientA, double coefficientB, double coefficientC)
		{
			return string.Empty;
		}
	}
}
