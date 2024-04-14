using System;
using Autodesk.Revit.DB;
using Craftify.Geometry.Enums;
using Craftify.Geometry.Interfaces;
using Craftify.Geometry.SolidConverters;

namespace Craftify.Geometry.BoundingBoxes;

public static class BoundingBoxConversions
{
    public static Solid ConvertToSolid(
        this BoundingBoxXYZ boundingBox,
        IBoundingBoxSolidConverter? solidConverter = default,
        Action<BoundingBoxSolidConverterOptions>? configOptions = null)
    {
        solidConverter ??= new CuboidBoundingBoxSolidConverter();
        return solidConverter.Convert(boundingBox, configOptions);
    }
    
    public static Outline ConvertToOutline(this BoundingBoxXYZ boundingBox)
    {
        var boxMin = boundingBox.GetMin(ApplyTransform.Yes);
        var boxMax = boundingBox.GetMax(ApplyTransform.Yes);
        var minX = Math.Min(boxMin.X, boxMax.X);
        var minY = Math.Min(boxMin.Y, boxMax.Y);
        var minZ = Math.Min(boxMin.Z, boxMax.Z);

        var maxX = Math.Max(boxMin.X, boxMax.X);
        var maxY = Math.Max(boxMin.Y, boxMax.Y);
        var maxZ = Math.Max(boxMin.Z, boxMax.Z);

        var sortedMin = new XYZ(minX, minY, minZ);
        var sortedMax = new XYZ(maxX, maxY, maxZ);

        return new Outline(sortedMin, sortedMax);
    }
    
    public static BoundingBoxIntersectsFilter ConvertToIntersectionFilter(this BoundingBoxXYZ boundingBox)
    {
        return new BoundingBoxIntersectsFilter(
            boundingBox.ConvertToOutline());
    }
}