using System.Collections.Generic;
using System.Linq;
using Autodesk.Revit.DB;
using Craftify.Geometry.Extensions.Faces;

namespace Craftify.Geometry.Extensions.Solids;

public static class SolidSelectors
{
    
    public static IEnumerable<Solid> WhereVolumesNotEmpty(this IEnumerable<Solid> solids) =>
        solids
            .Where(s => s.HasVolume());
    
    public static IEnumerable<PlanarFace> SelectPlanarFacesMatchingDirection(
        this IEnumerable<Solid> solids, XYZ direction)
    {
        return solids.SelectMany(x => x.Faces.OfType<PlanarFace>()
            .WhereNormalMatches(direction));
    }

    public static IEnumerable<Face> SelectFacesMatchingCenterNormal(
        this IEnumerable<Solid> solids, XYZ direction)
    {
        return solids
            .SelectMany(x => x.SelectFacesMatchingCenterNormal(direction));
    }

    public static IEnumerable<Face> SelectFacesMatchingCenterNormal(
        this Solid solid,
        XYZ normal)
    {
        return solid.Faces
            .OfType<Face>()
            .Where(x => x.CenterNormalMatchesDirection(normal));
    }
}