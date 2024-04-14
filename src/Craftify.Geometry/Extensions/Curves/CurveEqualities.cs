using System.Linq;
using Autodesk.Revit.DB;
using Craftify.Geometry.Extensions.Points;
using Craftify.Shared;

namespace Craftify.Geometry.Extensions.Curves;

public static class CurveEqualities
{
    
    public static bool LiesInPlane(this Curve curve, Plane plane)
    {
        var startPoint = curve.GetStartPoint();
        var endPoint = curve.GetEndPoint();
        plane.Project(startPoint, out _, out var startPointDistance);
        plane.Project(endPoint, out _, out var endPointDistance);
        return startPointDistance.IsAlmostEqualTo(0) &&
               endPointDistance.IsAlmostEqualTo(0);
    }
    
    public static bool IsVertical(this Curve curve)
    {
        var vector = curve.ToNormalizedVector();
        return vector.IsAlmostEqualTo(XYZ.BasisZ) || vector.IsAlmostEqualTo(-XYZ.BasisZ);
    }
    
    public static bool IsCollinearTo(this Line fromLine, Line toLine)
    {
        var fromVector = fromLine.Direction;
        var toVector = toLine.Origin - fromLine.Origin;
        return fromVector.IsParallelTo(toLine.Direction) && fromVector.IsParallelTo(toVector);
    }
    
    public static bool IsParallelTo(this XYZ fromVector, XYZ toVector)
    {
        return fromVector.CrossProduct(toVector).IsZeroLength();
    }
    
    public static bool IntersectsWith(this Curve fromCurve, Curve toCurve)
    {
        return fromCurve.Intersect(toCurve) == SetComparisonResult.Overlap;
    }
    public static bool IsCompletelyInside(this Curve fromCurve, Curve toCurve)
    {
        return fromCurve
            .SelectVertices()
            .All(v => v.LiesOnCurve(toCurve));;
    }
    
    public static bool IsContinuousWith(this Curve fromCurve, Curve toCurve)
    {
        return fromCurve
            .GetEndPoint()
            .IsAlmostEqualTo(
                toCurve.GetStartPoint());
    }
    public static bool ShareSameEndPointsWith(this Curve fromCurve, Curve toCurve)
    {
        return fromCurve
            .GetEndPoint()
            .IsAlmostEqualTo(
                toCurve.GetEndPoint());
    }
}