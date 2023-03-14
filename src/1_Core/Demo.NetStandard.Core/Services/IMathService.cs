using Demo.Net.Core.Entities;

using System.Collections.Generic;

namespace Demo.Net.Core.Services
{
    public interface IMathService
    {
        string Name { get; set; }
        string Description { get; set; }
        IEnumerable<Point> Calculate(IEnumerable<Point> points, double coefficienta, double coefficientb, double coefficientc);
    }
}
