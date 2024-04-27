using System;
using Autodesk.Revit.DB;
using Craftify.Geometry.Extensions;
using Craftify.Geometry.Extensions.Points;

namespace Craftify.Geometry.BoundingBoxes;

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

    public static BoundingBoxXYZ SetMin(this BoundingBoxXYZ boundingBoxXYZ, XYZ min)
    {
        var clonedBox = boundingBoxXYZ.Clone();
        clonedBox.Min = min;
        return clonedBox;
    }
    public static BoundingBoxXYZ SetMax(this BoundingBoxXYZ boundingBoxXYZ, XYZ max)
    {
        var clonedBox = boundingBoxXYZ.Clone();
        clonedBox.Max = max;
        return clonedBox;
    }
    
    public static BoundingBoxXYZ ExtrudeUpwards(this BoundingBoxXYZ boundingBoxXYZ, double value)
    {
        var newMax = boundingBoxXYZ.Max.MoveAlongVector(XYZ.BasisZ * value);
        return boundingBoxXYZ.SetMax(newMax);
    }
    
    public static BoundingBoxXYZ ExtrudeFront(this BoundingBoxXYZ boundingBoxXYZ, double value)
    {
        var newMax = boundingBoxXYZ.Max.MoveAlongVector(XYZ.BasisY * value);
        return boundingBoxXYZ.SetMax(newMax);
    }
    public static BoundingBoxXYZ ExtrudeBack(this BoundingBoxXYZ boundingBoxXYZ, double value)
    {
        var newMin = boundingBoxXYZ.Min.MoveAlongVector(XYZ.BasisY.Negate() * value);
        return boundingBoxXYZ.SetMin(newMin);
    }
    public static BoundingBoxXYZ ExtrudeRight(this BoundingBoxXYZ boundingBoxXYZ, double value)
    {
        var newMax = boundingBoxXYZ.Max.MoveAlongVector(XYZ.BasisX * value);
        return boundingBoxXYZ.SetMax(newMax);
    }
    
    public static BoundingBoxXYZ ExtrudeLeft(this BoundingBoxXYZ boundingBoxXYZ, double value)
    {
        var newMin = boundingBoxXYZ.Min.MoveAlongVector(XYZ.BasisX.Negate() * value);
        return boundingBoxXYZ.SetMin(newMin);
    }

    public static BoundingBoxXYZ ExtrudeDownwards(this BoundingBoxXYZ boundingBoxXYZ, double value)
    {
        var newMin = boundingBoxXYZ.Min.MoveAlongVector(XYZ.BasisZ.Negate() * value);
        return boundingBoxXYZ.SetMin(newMin);
    }
        
    public static BoundingBoxXYZ SetOrigin(this BoundingBoxXYZ boundingBox, XYZ origin)
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