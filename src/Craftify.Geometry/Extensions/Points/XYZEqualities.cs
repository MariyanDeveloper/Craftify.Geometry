using Autodesk.Revit.DB;
using Craftify.Shared;

namespace Craftify.Geometry.Extensions.Points;

public static class XYZEqualities
{
    public static bool LiesOnCurve(
        this XYZ point, Curve curve)
    {
        return curve.Distance(point).IsAlmostEqualTo(0);
    }
}