﻿using Demo.NetStandard.Core.Entities;
using Demo.NetStandard.Core.Services;

using Microsoft.Extensions.Logging;

using System;
using System.Collections.Generic;
using System.Linq;

namespace Demo.Net.Wpf.XmlPresenter.Services
{
    /// <summary>
    /// b > 0, b not equals 1
    /// </summary>
    public class LogarithmMathService : IMathService
    {
        private readonly ILogger _logger;
        public string Name { get; set; } = "Logarithm";
        public string Description { get; set; } = "y = log_{b}x";

        public LogarithmMathService(ILogger<LogarithmMathService> logger)
        {
            _logger = logger;
        }

        public IEnumerable<Point> Calculate(IEnumerable<Point> points, double coefficienta, double coefficientb, double coefficientc)
        {
            _logger.LogInformation($"{nameof(coefficienta)}: {coefficienta} - {nameof(coefficientb)}: {coefficientb} - {nameof(coefficientc)}: {coefficientc}");
            var result = points.Select(p => new Point() { X = p.X, Y = Math.Log(p.X <= 0 ? -1 * p.X : p.X, coefficientb) });
            _logger.LogTrace($"{nameof(Calculate)}");
            return result;
        }

		public string Validate(double coefficienta, double coefficientb, double coefficientc)
		{
			if (coefficientb <= 0)
				return "Coefficient B should be greater than zero";

			if (coefficientb == 1)
				return "Coefficient B should not equal 1";

            return string.Empty;
		}
	}
}
