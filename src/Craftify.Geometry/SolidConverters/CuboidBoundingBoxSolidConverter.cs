using System;
using System.Collections.Generic;
using Autodesk.Revit.DB;
using Craftify.Geometry.BoundingBoxes;
using Craftify.Geometry.Enums;
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
        var boundingBoxUpDirection = options.ApplyTransform switch {
            ApplyTransform.Yes => boundingBox.Transform.BasisZ,
            ApplyTransform.No => XYZ.BasisZ,
            _ => throw new ArgumentOutOfRangeException()
        };
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