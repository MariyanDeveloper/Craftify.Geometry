using System;
using System.Collections.Generic;
using System.Linq;
using Autodesk.Revit.DB;

namespace Craftify.Geometry.BoundingBoxes;

public static class BoundingBoxMergeOperations
{
    public static BoundingBoxXYZ Merge(this IEnumerable<BoundingBoxXYZ> boxes)
    {
        if (boxes == null)
        {
            throw new ArgumentNullException(nameof(boxes));
        }

        return boxes
            .Aggregate((accumulation, current) => accumulation.MergeWith(current));
    }

    public static BoundingBoxXYZ MergeWith(
        this BoundingBoxXYZ fromBoundingBox,
        BoundingBoxXYZ toBoundingBox)
    {
        if (fromBoundingBox is null)
        {
            throw new ArgumentNullException(nameof(fromBoundingBox));
        }

        if (toBoundingBox is null)
        {
            throw new ArgumentNullException(nameof(toBoundingBox));
        }

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