using System.Collections.Generic;
using System.Linq;
using Autodesk.Revit.DB;

namespace Craftify.Geometry.Extensions;

public static class PlanarFacesExtensions
{
    public static IEnumerable<PlanarFace> WhereNormalMatches(this IEnumerable<PlanarFace> planarFaces, XYZ normal)
    {
        return planarFaces
            .Where(f => f.FaceNormal.IsAlmostEqualTo(normal));
    }
}