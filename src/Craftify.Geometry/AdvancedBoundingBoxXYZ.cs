using System.Linq;
using Autodesk.Revit.DB;
using System;
using Craftify.Geometry.Enums;

namespace Craftify.Geometry;

public class AdvancedBoundingBoxXYZ : BoundingBoxXYZ
{
    public double Length
    {
        get => GetDimensions(Side.Length);
        set => SetDimensions(value, Side.Length, Alignment.Center);
    }

    public double Width
    {
        get => GetDimensions(Side.Width);
        set => SetDimensions(value, Side.Width, Alignment.Center);
    }
    public double Height
    {
        get => GetDimensions(Side.Height);
        set => SetDimensions(value, Side.Height, Alignment.Bottom);
    }
    public AdvancedBoundingBoxXYZ(double length = 1, double width = 1, double height = 1)
    {
        if (length <= 0) throw new ArgumentOutOfRangeException(nameof(length));
        if (width <= 0) throw new ArgumentOutOfRangeException(nameof(width));
        if (height <= 0) throw new ArgumentOutOfRangeException(nameof(height));
        Length = length;
        Width = width;
        Height = height;
    }
    
    public void Align(Side side, Alignment alignment)
    {
        SetDimensions(
            GetDimensions(side), side, alignment);
    }
    private double GetDimensions(Side side)
    {
        var measurementIndex = (int)side;
        return Max[measurementIndex] - Min[measurementIndex];
    }
    private void SetDimensions(double value, Side side, Alignment alignment)
    {
        var alignmentFactor = (int)alignment;
        var measurementIndex = (int)side;
        var minCoordinates = Enumerable.Range(0, 3).Select(x => Min[x]).ToList();
        var maxCoordinates = Enumerable.Range(0, 3).Select(x => Max[x]).ToList();
        var minValue = -value / 2 * alignmentFactor;
        var maxValue = value + minValue;
        minCoordinates[measurementIndex] = minValue;
        maxCoordinates[measurementIndex] = maxValue;
        Min = new XYZ(minCoordinates[0], minCoordinates[1], minCoordinates[2]);
        Max = new XYZ(maxCoordinates[0], maxCoordinates[1], maxCoordinates[2]);
    }
}