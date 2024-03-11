using System.Linq;
using Autodesk.Revit.DB;
using Craftify.Geometry.Enums;

namespace Craftify.Geometry.Extensions.BoundingBoxes;

public static class BoundingBoxMeasurements
{
    public static BoundingBoxXYZ SetLength(
        this BoundingBoxXYZ box,
        double value,
        Alignment alignment = Alignment.Center)
    {
        return box.SetDimension(value, Side.Length, alignment);
    }

    public static BoundingBoxXYZ SetWidth(this BoundingBoxXYZ boundingBox, double value,
        Alignment alignment = Alignment.Center)
    {
        return boundingBox.SetDimension(value, Side.Width, alignment);
    }


    public static BoundingBoxXYZ SetHeight(this BoundingBoxXYZ boundingBox, double value,
        Alignment alignment = Alignment.Bottom)
    {
        return boundingBox.SetDimension(value, Side.Height, alignment);
    }

    public static BoundingBoxXYZ Align(this BoundingBoxXYZ boundingBox, Side side, Alignment alignment)
    {
        var currentSideValue = boundingBox.CalculateSideDimension(side);
        return boundingBox.SetDimension(currentSideValue, side, alignment);
    }

    private static BoundingBoxXYZ SetDimension(
        this BoundingBoxXYZ boundingBox,
        double value,
        Side side,
        Alignment alignment)
    {
        var alignmentFactor = (int)alignment;
        var sideIndex = (int)side;
        var minCoordinates = boundingBox.Min.GetCoordinates();
        var maxCoordinates = boundingBox.Max.GetCoordinates();
        var minValue = -value / 2 * alignmentFactor;
        var maxValue = value + minValue;
        minCoordinates[sideIndex] = minValue;
        maxCoordinates[sideIndex] = maxValue;
        var clonedBox = boundingBox.Clone();
        clonedBox.Min = new XYZ(minCoordinates[0], minCoordinates[1], minCoordinates[2]);
        clonedBox.Max = new XYZ(maxCoordinates[0], maxCoordinates[1], maxCoordinates[2]);
        return clonedBox;
    }

    public static double[] GetCoordinates(this XYZ xyz)
    {
        return Enumerable.Range(0, 3).Select(x => xyz[x]).ToArray();
    }

    public static (double Length, double Width, double Height) CalculateDimension(this BoundingBoxXYZ boundingBox)
    {
        var length = boundingBox.CalculateSideDimension(Side.Length);
        var width = boundingBox.CalculateSideDimension(Side.Width);
        var height = boundingBox.CalculateSideDimension(Side.Height);
        return (length, width, height);
    }

    public static BoundingBoxXYZ SetDimension(
        this BoundingBoxXYZ boundingBox,
        double length, double width, double height)
    {
        return boundingBox
            .SetLength(length)
            .SetWidth(width)
            .SetHeight(height);
    }

    public static double CalculateSideDimension(this BoundingBoxXYZ boundingBox, Side side)
    {
        return boundingBox.Min
            .MeasureSignedDistance(boundingBox.Max, side.GetCorrespondingVector());
    }
}