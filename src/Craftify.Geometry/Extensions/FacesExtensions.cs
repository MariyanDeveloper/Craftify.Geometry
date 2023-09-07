using System.Collections.Generic;
using System.Linq;
using Autodesk.Revit.DB;

namespace Craftify.Geometry.Extensions;

public static class FacesExtensions
{
    public static IEnumerable<CurveLoop> SelectFlattenLoops(this IEnumerable<Face> faces)
    {
        return faces
            .SelectMany(x => x.GetEdgesAsCurveLoops());
    }
}