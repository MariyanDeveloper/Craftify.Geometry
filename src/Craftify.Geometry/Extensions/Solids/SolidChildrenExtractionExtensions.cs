using System;
using System.Collections.Generic;
using System.Linq;
using Autodesk.Revit.DB;
using Craftify.Geometry.Collections;

namespace Craftify.Geometry.Extensions.Solids;

public static class SolidChildrenExtractionExtensions
{
    
    public static Collections.Solids ToSolids(this IEnumerable<Solid> solids) => new Collections.Solids(solids);
    
    public static IEnumerable<T> GetChildComponents<T>(this Solid solid)
    {
        if (typeof(T) == typeof(Face))
        {
            return (IEnumerable<T>)solid.GetFaces();
        }
        if (typeof(T) == typeof(Curve))
        {
            return (IEnumerable<T>)solid.GetCurves();
        }

        if (typeof(T) == typeof(XYZ))
        {
            return (IEnumerable<T>)solid.GetVertices();
        }

        if (typeof(T) == typeof(CurveLoop))
        {
            return (IEnumerable<T>)solid.GetFaces()
                .SelectMany(f => f.GetEdgesAsCurveLoops());
        }

        throw new NotImplementedException($"Given type : {typeof(T)} is not supported");
    }
    
        
    public static Faces GetFaces(
        this Solid solid)
    {
        return new Faces(solid.Faces.OfType<Face>());

    }
    public static Collections.Curves GetCurves(
        this Solid solid)
    {
        return new Collections.Curves(solid.GetFaces()
            .SelectMany(x => x.GetEdgesAsCurveLoops())
            .SelectMany(x => x));
    }
    
    public static IEnumerable<T> SelectFlattenFaces<T>(this IEnumerable<Solid> solids) where T : Face
    {
        return solids
            .SelectMany(s => s.GetFaces()
                .OfType<T>());
    }
        
    public static Vertices GetVertices(
        this Solid solid)
    {
        return new Vertices(solid.GetCurves()
            .SelectMany(x => x.Tessellate()));
    }
    
    public static Vertices GetEdgeVertices(this Solid solid)
    {
        return solid
            .Edges.Cast<Edge>()
            .SelectMany(x => x.Tessellate())
            .ToVertices();
    }

}