using Demo.NetStandard.Core.Services;

using Microsoft.Extensions.Logging;

namespace Demo.Net.Maui.UIApp.Services
{
	public class TangentMathService : IMathService
	{
		private readonly ILogger _logger;
		public string Name { get; set; } = "Tangent";
		public string Description { get; set; } = "tangent.png";

		public TangentMathService(ILogger<TangentMathService> logger)
		{
			_logger = logger;
		}
		public IEnumerable<NetStandard.Core.Entities.Point> Calculate(IEnumerable<NetStandard.Core.Entities.Point> points, double coefficienta, double coefficientb, double coefficientc)
		{
			_logger.LogInformation($"{nameof(coefficienta)}: {coefficienta} - {nameof(coefficientb)}: {coefficientb} - {nameof(coefficientc)}: {coefficientc}");
			var result = points.Select(p => new NetStandard.Core.Entities.Point() { X = p.X, Y = Math.Tan(p.X) });
			_logger.LogTrace($"{nameof(Calculate)}");
			return result;
		}
	}
}
