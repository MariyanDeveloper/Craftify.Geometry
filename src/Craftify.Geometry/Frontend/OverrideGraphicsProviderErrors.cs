using Craftify.Functional;

namespace Craftify.Geometry.Frontend;

public static class OverrideGraphicsProviderErrors
{
    public static Error PatternNotFoundError(string patternName)
    {
        return new PatternNotFoundError($"Pattern named {patternName} was not found in the database");
    }
}