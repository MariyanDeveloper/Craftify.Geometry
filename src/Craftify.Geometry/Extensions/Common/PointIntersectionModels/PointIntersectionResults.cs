using System.Collections.Generic;
using Autodesk.Revit.DB;

namespace Craftify.Geometry.Extensions.Common.Intersections;

public static class PointIntersectionResults
{
    public static PointIntersectionResult CreateNoIntersection() => new NoIntersectionResult();
    public static PointIntersectionResult CreateExactIntersection(XYZ intersection) => new ExactIntersectionResult(intersection);
    public static PointIntersectionResult CreateMultiIntersection(params XYZ[] intersections) =>
        new MultiPointIntersectionResult(intersections);
    public static PointIntersectionResult CreateMultiIntersection(IReadOnlyCollection<XYZ> intersections) =>
        new MultiPointIntersectionResult(intersections);
    
}