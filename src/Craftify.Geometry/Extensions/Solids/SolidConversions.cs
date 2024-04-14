using Autodesk.Revit.DB;

namespace Craftify.Geometry.Extensions.Solids;

public static class SolidConversions
{
    public static ElementIntersectsSolidFilter ConvertToIntersectionFilter(this Solid solid, bool isInverted = false) =>
        new(solid, isInverted);
}