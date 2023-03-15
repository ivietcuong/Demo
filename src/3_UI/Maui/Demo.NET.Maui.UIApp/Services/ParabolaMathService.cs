using Demo.NetStandard.Core.Services;

using Microsoft.Extensions.Logging;

namespace Demo.Net.Maui.UIApp.Services
{
	public class ParabolaMathService : IMathService
	{
		private readonly ILogger _logger;
		public string Name { get; set; } = "Parabola";
		public string Description { get; set; } = "algebra.png";

		public ParabolaMathService(ILogger<ParabolaMathService> logger)
		{
			_logger = logger;
		}

		public IEnumerable<NetStandard.Core.Entities.Point> Calculate(IEnumerable<NetStandard.Core.Entities.Point> points, double coefficientA, double coefficientB, double coefficientC)
		{
			_logger.LogInformation($"{nameof(coefficientA)}: {coefficientA} - {nameof(coefficientB)}: {coefficientB} - {nameof(coefficientC)}: {coefficientC}");
			var result = points.Select(p => new NetStandard.Core.Entities.Point() { X = p.X, Y = coefficientA * Math.Pow(p.X, 2) + coefficientB * p.X + coefficientC });
			_logger.LogTrace($"{nameof(Calculate)}");
			return result;
		}

		public string Validate(double coefficientA, double coefficientB, double coefficientC)
		{
			if (coefficientA == 0)
				return "Coefficient A should not be zero";

			return string.Empty;
		}
	}
}
