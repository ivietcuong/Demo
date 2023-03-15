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

		public IEnumerable<NetStandard.Core.Entities.Point> Calculate(IEnumerable<NetStandard.Core.Entities.Point> points, double coefficienta, double coefficientb, double coefficientc)
		{
			_logger.LogInformation($"{nameof(coefficienta)}: {coefficienta} - {nameof(coefficientb)}: {coefficientb} - {nameof(coefficientc)}: {coefficientc}");
			var result = points.Select(p => new NetStandard.Core.Entities.Point() { X = p.X, Y = coefficienta * Math.Pow(p.X, 2) + coefficientb * p.X + coefficientc });
			_logger.LogTrace($"{nameof(Calculate)}");
			return result;
		}

		public string Validate(double coefficienta, double coefficientb, double coefficientc)
		{
			throw new NotImplementedException();
		}
	}
}
