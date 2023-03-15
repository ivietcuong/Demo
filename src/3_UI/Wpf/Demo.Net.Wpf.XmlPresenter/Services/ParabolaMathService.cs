using Demo.NetStandard.Core.Entities;
using Demo.NetStandard.Core.Services;

using Microsoft.Extensions.Logging;

using System;
using System.Collections.Generic;
using System.Linq;

namespace Demo.Net.Wpf.XmlPresenter.Services
{
    /// <summary>
    /// a not equals 0
    /// </summary>
    public class ParabolaMathService : IMathService
    {
        private readonly ILogger _logger;
        public string Name { get; set; } = "Parabola";
        public string Description { get; set; } = "y = a.x^{2} + b.x + c";

        public ParabolaMathService(ILogger<ParabolaMathService> logger)
        {
            _logger = logger;
        }
        public IEnumerable<Point> Calculate(IEnumerable<Point> points, double coefficientA, double coefficientB, double coefficientC)
        {
            _logger.LogInformation($"{nameof(coefficientA)}: {coefficientA} - {nameof(coefficientB)}: {coefficientB} - {nameof(coefficientC)}: {coefficientC}");
            var result = points.Select(p => new Point() { X = p.X, Y = coefficientA * Math.Pow(p.X, 2) + coefficientB * p.X + coefficientC });
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
