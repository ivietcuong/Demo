using Demo.NetStandard.Core.Entities;
using Demo.NetStandard.Core.Services;

using Microsoft.Extensions.Logging;

using System;
using System.Collections.Generic;
using System.Linq;

namespace Demo.Net.Wpf.JsonPresenter.Services
{
    public class TangentMathService : IMathService
    {
        private readonly ILogger _logger;

        public string Name { get; } = "Tangent";
        public string Description { get; } = "y = tan(x)";
        public TangentMathService(ILogger<TangentMathService> logger)
        {
            _logger = logger;
        }
        public IEnumerable<Point> Calculate(IEnumerable<Point> points, double coefficienta, double coefficientb, double coefficientc)
        {
            _logger.LogInformation($"{nameof(coefficienta)}: {coefficienta} - {nameof(coefficientb)}: {coefficientb} - {nameof(coefficientc)}: {coefficientc}");
            var result = points.Select(p => new Point() { X = p.X, Y = Math.Tan(p.X) });
            _logger.LogTrace($"{nameof(Calculate)}");
            return result;
        }
    }
}
