using System;
using System.Collections.Generic;
using Autodesk.Revit.DB;
using Craftify.Geometry.Enums;
using Craftify.Geometry.Extensions;
using Craftify.Geometry.Extensions.BoundingBoxes;
using Craftify.Geometry.Interfaces;

namespace Craftify.Geometry.SolidConverters;

public class CuboidBoundingBoxSolidConverter : IBoundingBoxSolidConverter
{
    public Solid Convert(BoundingBoxXYZ boundingBox, Action<BoundingBoxSolidConverterOptions>? configOptions = null)
    {
        var options = new BoundingBoxSolidConverterOptions();
        configOptions?.Invoke(options);
        var curveLoop = boundingBox.GetCurveLoop(FaceSide.Bottom, options.ApplyTransform);
        var extrusionDistance = boundingBox.CalculateDimension().Height;
        var boundingBoxUpDirection = boundingBox.Transform.BasisZ;
        var extrusionDirection = (extrusionDistance < 0)
            ? -boundingBoxUpDirection
            : boundingBoxUpDirection;
        var solid = GeometryCreationUtilities.CreateExtrusionGeometry(
            new List<CurveLoop>() { curveLoop },
            extrusionDirection,
            Math.Abs(extrusionDistance));
        return solid;
    }
}