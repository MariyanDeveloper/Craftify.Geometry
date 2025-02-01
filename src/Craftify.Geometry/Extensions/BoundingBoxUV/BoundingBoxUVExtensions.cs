using System.Collections.Generic;
using System.Linq;
using Autodesk.Revit.DB;

namespace Craftify.Geometry.Extensions.BoundingBoxUV;

public static class BoundingBoxUVExtensions
{
    public static IEnumerable<UV> SelectBounds(this Autodesk.Revit.DB.BoundingBoxUV boundingBoxUv)
    {
        var startIndexRange = 0;
        var endIndexRange = 2;
        return Enumerable.Range(startIndexRange, endIndexRange)
            .Select(boundingBoxUv.get_Bounds);
    }
}