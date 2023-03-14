using Demo.NetStandard.Core.Services;

using Microsoft.Extensions.Logging;

namespace Demo.Net.Maui.UIApp.Services
{
	public class LogarithmMathService : IMathService
	{
		private readonly ILogger _logger;
		public string Name { get; set; } = "Logarithm";
		public string Description { get; set; } = "y = log_{b}x";

		public LogarithmMathService(ILogger<LogarithmMathService> logger)
		{
			_logger = logger;
		}

		public IEnumerable<NetStandard.Core.Entities.Point> Calculate(IEnumerable<NetStandard.Core.Entities.Point> points, double coefficienta, double coefficientb, double coefficientc)
		{
			_logger.LogInformation($"{nameof(coefficienta)}: {coefficienta} - {nameof(coefficientb)}: {coefficientb} - {nameof(coefficientc)}: {coefficientc}");
			var result = points.Select(p => new NetStandard.Core.Entities.Point() { X = p.X, Y = Math.Log(p.X <= 0 ? -1 * p.X : p.X, coefficientb) });
			_logger.LogTrace($"{nameof(Calculate)}");
			return result;
		}
	}
}
