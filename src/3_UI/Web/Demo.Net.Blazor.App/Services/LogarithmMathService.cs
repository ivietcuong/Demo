﻿using Point = Demo.NetStandard.Core.Entities.Point;

namespace Demo.Net.Blazor.App.Services
{
    public class LogarithmMathService : ILogarithmMathService
    {
        private readonly ILogger _logger;
        public string Name { get; set; } = "Logarithm";
        public string Description { get; set; } = "logarithm.png";

        public LogarithmMathService(ILogger<LogarithmMathService> logger)
        {
            _logger = logger;
        }

        public IEnumerable<Point> Calculate(IEnumerable<Point> points, double coefficientA, double coefficientB, double coefficientC)
        {
            _logger.LogInformation($"{nameof(coefficientA)}: {coefficientA} - {nameof(coefficientB)}: {coefficientB} - {nameof(coefficientC)}: {coefficientC}");
            var result = points.Select(p => new Point() { X = p.X, Y = Math.Log(p.X <= 0 ? -1 * p.X : p.X, coefficientB) });

            _logger.LogTrace($"{GetType().FullName}: {nameof(Calculate)}");

            return result;
        }

        public string Validate(double coefficientA, double coefficientB, double coefficientC)
        {
            if (coefficientB <= 0)
                return "Coefficient B should be greater than zero";

            if (coefficientB == 1)
                return "Coefficient B should not equal 1";

            return string.Empty;
        }
    }
}
