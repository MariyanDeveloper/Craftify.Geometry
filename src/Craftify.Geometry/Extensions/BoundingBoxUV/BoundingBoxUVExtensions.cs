using System.Collections.Generic;
using System.Linq;
using Autodesk.Revit.DB;

namespace Craftify.Geometry.Extensions.Faces;

public static class BoundingBoxUVExtensions
{
    public static IEnumerable<UV> SelectBounds(this BoundingBoxUV boundingBoxUv)
    {
        var startIndexRange = 0;
        var endIndexRange = 2;
        return Enumerable.Range(startIndexRange, endIndexRange)
            .Select(boundingBoxUv.get_Bounds);
    }
}