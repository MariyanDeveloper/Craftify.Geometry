using Autodesk.Revit.DB;
using System;

namespace Craftify.Geometry.Extensions.BoundingBoxes;

public static class BoundingBoxTransformations
{
    public static BoundingBoxXYZ MoveBy(this BoundingBoxXYZ boundingBox, XYZ translation)
    {
        if (translation is null) throw new ArgumentNullException(nameof(translation));
        var transform = boundingBox.Transform;
        var origin = transform.Origin.MoveAlongVector(translation);
        transform.Origin = origin;
        return boundingBox.SetTransform(transform);
    }
    
    public static BoundingBoxXYZ MoveToGlobalOrigin(this BoundingBoxXYZ boundingBox)
    {
        return boundingBox.MoveBy(
            boundingBox.GetCenter().Negate());
    }
    
    public static BoundingBoxXYZ SetTransform(this BoundingBoxXYZ boundingBox, Transform transform)
    {
        var box = boundingBox.Clone();
        box.Transform = transform;
        return box;
    }
        
    public static BoundingBoxXYZ ChangeOrigin(this BoundingBoxXYZ boundingBox, XYZ origin)
    {
        var vectorToMoveBy = boundingBox.Transform.Origin.ToVector(origin);
        return boundingBox.MoveBy(vectorToMoveBy);
    }
        
    public static BoundingBoxXYZ ChangePlacement(this BoundingBoxXYZ boundingBox, XYZ origin, Transform orientation)
    {
        var clonedTransform = orientation.Clone();
        clonedTransform.Origin = origin;
        return boundingBox.SetTransform(clonedTransform);
    }
        
    public static BoundingBoxXYZ Clone(this BoundingBoxXYZ boundingBox)
    {
        return new BoundingBoxXYZ()
        {
            Min = boundingBox.Min,
            Max = boundingBox.Max,
            Transform = boundingBox.Transform
        };
    }
}