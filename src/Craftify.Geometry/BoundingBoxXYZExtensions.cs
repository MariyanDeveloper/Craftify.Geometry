using System;
using System.Collections.Generic;
using System.Linq;
using Autodesk.Revit.DB;

namespace Craftify.Geometry;

public static class BoundingBoxXYZExtensions
{
    public static void MoveBy(this BoundingBoxXYZ boundingBox,XYZ translation)
    {
        if (translation == null) throw new ArgumentNullException(nameof(translation));
        var transform = boundingBox.Transform;
        var origin = transform.Origin.MoveAlongVector(translation);
        transform.Origin = origin;
        boundingBox.Transform = transform;
    }

    public static void MoveToGlobalOrigin(this BoundingBoxXYZ boundingBox)
    {
        boundingBox.MoveBy(boundingBox.GetCenter().Negate());
    }

    public static XYZ GetCenter(this BoundingBoxXYZ boundingBox)
    {
        if (boundingBox == null) throw new ArgumentNullException(nameof(boundingBox));
        return boundingBox.Max.MoveAlongVector(boundingBox.Min).Multiply(0.5);
    }
    public static XYZ[] GetVertices(this BoundingBoxXYZ box)
    {
        if (box == null) throw new ArgumentNullException(nameof(box));
        return Enumerable.Range(0, 2).Select(i => box.Transform.OfPoint(box.get_Bounds(i))).ToArray();
    }

    public static BoundingBoxXYZ Merge(this IEnumerable<BoundingBoxXYZ> boxes)
    {
        if (boxes == null) throw new ArgumentNullException(nameof(boxes));
        return boxes
            .Aggregate((previous, next) => previous.MergeWith(next));
    }
    public static BoundingBoxXYZ MergeWith(this BoundingBoxXYZ fromBoundingBox, BoundingBoxXYZ toBoundingBox)
    {
        if (fromBoundingBox == null) throw new ArgumentNullException(nameof(fromBoundingBox));
        if (toBoundingBox == null) throw new ArgumentNullException(nameof(toBoundingBox));
        var minX = Math.Min(fromBoundingBox.Min.X, toBoundingBox.Min.X);
        var minY = Math.Min(fromBoundingBox.Min.Y, toBoundingBox.Min.Y);
        var minZ = Math.Min(fromBoundingBox.Min.Z, toBoundingBox.Min.Z);

        var maxX = Math.Max(fromBoundingBox.Max.X, toBoundingBox.Max.X);
        var maxY = Math.Max(fromBoundingBox.Max.Y, toBoundingBox.Max.Y);
        var maxZ = Math.Max(fromBoundingBox.Max.Z, toBoundingBox.Max.Z);

        var newBoundingBox = new BoundingBoxXYZ
        {
            Min = new XYZ(minX, minY, minZ),
            Max = new XYZ(maxX, maxY, maxZ)
        };
        return newBoundingBox;
    }
}