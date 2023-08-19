using System;
using System.Collections.Generic;
using System.Linq;
using Autodesk.Revit.DB;
using Craftify.Geometry.BoundingBoxVisualizations;
using Craftify.Geometry.Collections;
using Craftify.Geometry.Enums;
using Craftify.Geometry.Interfaces;

namespace Craftify.Geometry.Extensions;

public static class BoundingBoxXYZExtensions
{
    public static void MoveBy(this BoundingBoxXYZ boundingBox, XYZ translation)
    {
        if (translation is null) throw new ArgumentNullException(nameof(translation));
        var transform = boundingBox.Transform;
        var origin = transform.Origin.MoveAlongVector(translation);
        transform.Origin = origin;
        boundingBox.Transform = transform;
    }

    public static void ChangeOrigin(this BoundingBoxXYZ boundingBox, XYZ origin)
    {
        var vectorToMoveBy = boundingBox.Transform.Origin.ToVector(origin);
        boundingBox.MoveBy(vectorToMoveBy);
    }

    public static void ChangePlacement(this BoundingBoxXYZ boundingBox, XYZ origin, Transform orientation)
    {
        var clonedTransform = orientation.Clone();
        clonedTransform.Origin = origin;
        boundingBox.Transform = clonedTransform;
    }
    public static void MoveToGlobalOrigin(this BoundingBoxXYZ boundingBox)
    {
        boundingBox.MoveBy(boundingBox.GetCenter().Negate());
    }
    
    public static void VisualizeIn(
        this BoundingBoxXYZ boundingBox,
        Document document,
        IBoundingBoxVisualization boundingBoxVisualization,
        ApplyTransform applyTransform = ApplyTransform.No)
    {
        boundingBoxVisualization.VisualizeIn(
            boundingBox,
            document,
            options => options.ApplyTransform = applyTransform);
    }

    public static Solid ConvertToSolid(
        this BoundingBoxXYZ boundingBox,
        IBoundingBoxSolidConverter solidConverter,
        Action<BoundingBoxSolidConverterOptions>? configOptions = null)
    {
        return solidConverter.Convert(boundingBox, configOptions);
    }
    public static XYZ GetCenter(this BoundingBoxXYZ boundingBox, ApplyTransform applyTransform = ApplyTransform.No)
    {
        if (boundingBox == null) throw new ArgumentNullException(nameof(boundingBox));
        var center = boundingBox.Max.MoveAlongVector(boundingBox.Min).Multiply(0.5);
        return (applyTransform == ApplyTransform.No)
            ? center
            : boundingBox.Transform.OfPoint(center);
    }
    public static CornerVertices GetCornerVertices(this BoundingBoxXYZ box, ApplyTransform applyTransform = ApplyTransform.Yes)
    {
        if (box == null) throw new ArgumentNullException(nameof(box));
        var vertices =  Enumerable.Range(0, 2)
            .Select(i =>
            {
                var vertex = box.get_Bounds(i);
                return (applyTransform == ApplyTransform.Yes)
                    ? box.Transform.OfPoint(vertex)
                    : vertex;
            }).ToArray();
        return new CornerVertices(vertices[0], vertices[1]);
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
        return CreateBoundingBoxByOutermostCorners(fromBoundingBox, toBoundingBox);
    }

    public static BoxDimension CalculateDimension(this BoundingBoxXYZ boundingBox)
    {
        var length = boundingBox.CalculateSideDimension(Side.Length);
        var width = boundingBox.CalculateSideDimension(Side.Width);
        var height = boundingBox.CalculateSideDimension(Side.Height);
        return new(length, width, height);
    }

    public static double CalculateSideDimension(this BoundingBoxXYZ boundingBox, Side side)
    {
        return boundingBox.Min
            .MeasureDistanceAlongVector(boundingBox.Max, side.GetCorrespondingVector());
    }
    
    /// <summary>
    /// SetLength - sets the length of the bounding box along the X vector (direction).
    /// The length represents the distance in the direction of the X-axis.
    /// </summary>
    /// <param name="boundingBox">The bounding box to modify.</param>
    /// <param name="value">The value of the length to set along the X vector.</param>
    /// <param name="alignment">The alignment of the bounding box (optional, default is Alignment.Center).</param>
    public static void SetLength(this BoundingBoxXYZ boundingBox, double value, Alignment alignment = Alignment.Center)
    {
        boundingBox.SetDimension(value, Side.Length, alignment);
    }
    
    /// <summary>
    /// SetWidth - sets the width of the bounding box along the X vector (direction).
    /// The width represents the distance in the direction of the X-axis.
    /// </summary>
    /// <param name="boundingBox">The bounding box to modify.</param>
    /// <param name="value">The value of the width to set along the X vector.</param>
    /// <param name="alignment">The alignment of the bounding box (optional, default is Alignment.Center).</param>
    public static void SetWidth(this BoundingBoxXYZ boundingBox, double value, Alignment alignment = Alignment.Center)
    {
        boundingBox.SetDimension(value, Side.Width, alignment);
    }
    
    /// <summary>
    /// SetHeight - sets the height of the bounding box along the Z vector (direction).
    /// The height represents the distance in the direction of the Z-axis.
    /// </summary>
    /// <param name="boundingBox">The bounding box to modify.</param>
    /// <param name="value">The value of the height to set along the Z vector.</param>
    /// <param name="alignment">The alignment of the bounding box (optional, default is Alignment.Bottom).</param>
    public static void SetHeight(this BoundingBoxXYZ boundingBox, double value, Alignment alignment = Alignment.Bottom)
    {
        boundingBox.SetDimension(value, Side.Height, alignment);
    }

    public static void SetDimension(this BoundingBoxXYZ boundingBox, BoxDimension boxDimension)
    {
        boundingBox.SetLength(boxDimension.Length);
        boundingBox.SetWidth(boxDimension.Width);
        boundingBox.SetHeight(boxDimension.Height);
    }

    public static void Align(this BoundingBoxXYZ boundingBox, Side side, Alignment alignment)
    {
        var currentSideValue = boundingBox.CalculateSideDimension(side);
        boundingBox.SetDimension(currentSideValue, side, alignment);
    }
    public static BoundingBoxXYZ AlignCloned(this BoundingBoxXYZ boundingBox, Side side, Alignment alignment)
    {
        var clonedBox = boundingBox.Clone();
        var currentSideValue = clonedBox.CalculateSideDimension(side);
        clonedBox.SetDimension(currentSideValue, side, alignment);
        return clonedBox;
    }

    public static BoundingBoxXYZ Clone(this BoundingBoxXYZ boundingBox)
    {
        var outputBoundingBox = new BoundingBoxXYZ()
        {
            Min = boundingBox.Min,
            Max = boundingBox.Max,
            Transform = boundingBox.Transform
        };
        return outputBoundingBox;
    }
    private static void SetDimension(
        this BoundingBoxXYZ boundingBox,
        double value,
        Side side,
        Alignment alignment)
    {
        if (value <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(value));
        }
        var alignmentFactor = (int)alignment;
        var sideIndex = (int)side;
        var minCoordinates = boundingBox.Min.GetCoordinates();
        var maxCoordinates = boundingBox.Max.GetCoordinates();
        var minValue = -value / 2 * alignmentFactor;
        var maxValue = value + minValue;
        minCoordinates[sideIndex] = minValue;
        maxCoordinates[sideIndex] = maxValue;
        boundingBox.Min = new XYZ(minCoordinates[0], minCoordinates[1], minCoordinates[2]);
        boundingBox.Max = new XYZ(maxCoordinates[0], maxCoordinates[1], maxCoordinates[2]);
    }
    private static BoundingBoxXYZ CreateBoundingBoxByOutermostCorners(BoundingBoxXYZ fromBoundingBox,
        BoundingBoxXYZ toBoundingBox)
    {
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