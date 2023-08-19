using System;
using Autodesk.Revit.DB;
using Craftify.Geometry.Extensions;
using Craftify.Geometry.Options;
using Craftify.Geometry.SolidConverters;

namespace Craftify.Geometry.BoundingBoxVisualizations;

public class SolidBoundingBoxVisualization : IBoundingBoxVisualization
{
    public void VisualizeIn(BoundingBoxXYZ boundingBox, Document document, Action<BoundingBoxVisualizationOption>? configOptions = null)
    {
        var options = new BoundingBoxVisualizationOption();
        configOptions?.Invoke(options);
        var solidToVisualize = new CuboidBoundingBoxSolidConverter()
            .Convert(boundingBox,
                converterOptions => converterOptions.ApplyTransform = options.ApplyTransform);
        solidToVisualize.VisualizeIn(document);
    }
}