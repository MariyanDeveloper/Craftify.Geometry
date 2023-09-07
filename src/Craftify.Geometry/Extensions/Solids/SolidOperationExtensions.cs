using System.Collections.Generic;
using System.Linq;
using Autodesk.Revit.DB;

namespace Craftify.Geometry.Extensions.Solids;


public static class SolidOperationExtensions
{
    public static Solid Union(
        this IEnumerable<Solid> solids)
    {
        return solids
            .Where(x => x.HasVolume())
            .Aggregate((x, y) => BooleanOperationsUtils.ExecuteBooleanOperation(
                x, y, BooleanOperationsType.Union));
    }
    
    public static Solid CreateMoved(this Solid solid, XYZ translation)
    {
        return solid.CreateTransformed(Transform.CreateTranslation(translation));
    }

    public static Solid CreateTransformed(this Solid solid, params Transform[] transforms)
    {
        var combinedTransform = transforms
            .Reverse()
            .Aggregate((current, next) => current.Multiply(next));
        return solid.CreateTransformed(combinedTransform);
    }
    public static Solid CreateTransformed(
        this Solid solid, Transform transform)
    {
        return SolidUtils.CreateTransformed(solid, transform);
    }
    public static Solid CreateTransformedAtGlobalOrigin(this Solid solid)
    {
        return solid.CreateTransformed(
            Transform.CreateTranslation(solid.ComputeCentroid().ToVector(XYZ.Zero)));
    }

    public static Solid CreateUnionSolidWith(this Solid fromSolid, Solid toSolid)
    {
        return BooleanOperationsUtils
            .ExecuteBooleanOperation(fromSolid, toSolid, BooleanOperationsType.Union);
    }

    public static Solid CreateIntersectedSolidWith(this Solid fromSolid, Solid toSolid)
    {
        return BooleanOperationsUtils
            .ExecuteBooleanOperation(fromSolid, toSolid, BooleanOperationsType.Intersect);
    }
    
    public static Solid CreateDifferenceSolidWith(this Solid fromSolid, Solid toSolid)
    {
        return BooleanOperationsUtils
            .ExecuteBooleanOperation(fromSolid, toSolid, BooleanOperationsType.Difference);
    }
    
}