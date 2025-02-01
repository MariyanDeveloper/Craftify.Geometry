using System.Collections.Generic;
using System.Linq;
using Autodesk.Revit.DB;
using Craftify.Shared;

namespace Craftify.Geometry.Extensions.Common.PointIntersectionModels;

public static class PointIntersectionResultMapExtensions
{
    public static IEnumerable<XYZ> ToPoints(this PointIntersectionResult pointIntersectionResult) =>
        pointIntersectionResult.Match(
            noIntersection: Enumerable.Empty<XYZ>,
            exactIntersection: point => point.AsMaterializedEnumerable(),
            multiIntersection: points => points);

    public static PointIntersectionResult ToPointIntersectionResult(this IEnumerable<XYZ> points) =>
        points.Match(
            emptyCase: PointIntersectionResults.CreateNoIntersection,
            singleCase: PointIntersectionResults.CreateExactIntersection,
            multipleCase: PointIntersectionResults.CreateMultiIntersection);
}