using System.Collections.Generic;
using System.Linq;
using Autodesk.Revit.DB;

namespace Craftify.Geometry.Extensions.Solids;

public static class SolidChildrenExtractionExtensions
{
    
    public static IEnumerable<Face> SelectFaces(
        this Solid solid)
    {
        return solid.Faces.OfType<Face>();
    }
    public static IEnumerable<Curve> SelectCurves(
        this Solid solid)
    {
        return solid.SelectCurveLoops()
            .SelectMany(x => x);
    }

    public static IEnumerable<CurveLoop> SelectCurveLoops(this Solid solid)
    {
        return solid
            .SelectFaces()
            .SelectMany(x => x.GetEdgesAsCurveLoops());
    }
    
    public static IEnumerable<T> SelectCurvesOfType<T>(
        this Solid solid) where T: Curve
    {
        return solid.SelectFaces()
            .SelectMany(x => x.GetEdgesAsCurveLoops())
            .SelectMany(x => x)
            .OfType<T>();
    }
    
    public static IEnumerable<T> SelectFlattenFaces<T>(this IEnumerable<Solid> solids) where T : Face
    {
        return solids
            .SelectMany(s => s.SelectFaces()
                .OfType<T>());
    }
        
    public static IEnumerable<XYZ> SelectFaceVertices(
        this Solid solid)
    {
        return solid.SelectCurves()
            .SelectMany(x => x.Tessellate());
    }
    
    public static IEnumerable<XYZ> SelectEdgeVertices(this Solid solid)
    {
        return solid
            .Edges.Cast<Edge>()
            .SelectMany(x => x.Tessellate());
    }

}