using System.Collections.Generic;
using System.Linq;
using Autodesk.Revit.DB;

namespace Craftify.Geometry.Extensions;

public static class IntersectionResultArrayExtensions
{
    public static IEnumerable<XYZ> SelectIntersectionPoints(this IntersectionResultArray intersectionResultArray)
    {
        return intersectionResultArray
            .OfType<IntersectionResult>()
            .Select(x => x.XYZPoint);
    }
}