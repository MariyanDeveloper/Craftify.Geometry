using System.Collections.Generic;

namespace Craftify.Geometry.System;

public static class StringExtensions
{
    public static string JoinBy(this IEnumerable<string> values, string separator) => string.Join(separator, values);
}