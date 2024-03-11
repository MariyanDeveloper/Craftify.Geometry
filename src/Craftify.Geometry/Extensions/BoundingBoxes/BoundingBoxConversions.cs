using System;
using Autodesk.Revit.DB;
using Craftify.Geometry.Interfaces;

namespace Craftify.Geometry.Extensions.BoundingBoxes;

public static class BoundingBoxConversions
{
    public static Solid ConvertToSolid(
        this BoundingBoxXYZ boundingBox,
        IBoundingBoxSolidConverter solidConverter,
        Action<BoundingBoxSolidConverterOptions>? configOptions = null)
    {
        return solidConverter.Convert(boundingBox, configOptions);
    }
}