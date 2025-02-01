using System;
using Autodesk.Revit.DB;

namespace Craftify.Geometry.Interfaces;

public interface IBoundingBoxSolidConverter
{
    Solid Convert(BoundingBoxXYZ boundingBox, Action<BoundingBoxSolidConverterOptions>? configOptions = null);
}