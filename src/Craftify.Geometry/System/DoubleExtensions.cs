using System;

namespace Craftify.Geometry.System;

public static class DoubleExtensions
{
    public static double Round(this double value, int decimals) => Math.Round(value, decimals);
}