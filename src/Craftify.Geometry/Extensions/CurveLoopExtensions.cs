using System.Collections.Generic;
using System.Linq;
using Autodesk.Revit.DB;
using Craftify.Geometry.Extensions.Points;

namespace Craftify.Geometry.Extensions;

public static class CurveLoopExtensions
{
    public static CurveLoop ProjectOntoPlane(this CurveLoop curveLoop, Plane plane)
    {
        var distance = curveLoop.GetPlane().Origin.MeasureSignedDistance(
            plane.Origin,
            plane.Normal);
        var vectorToMoveBy = plane.Normal.Multiply(distance);
        return CurveLoop.CreateViaTransform(curveLoop, Transform.CreateTranslation(vectorToMoveBy));
    }
    public static IEnumerable<CurveLoop> ProjectOntoPlane(this IEnumerable<CurveLoop> curveLoops, Plane plane)
    {
        return curveLoops
            .Select(c => ProjectOntoPlane(c, plane));
    }
}