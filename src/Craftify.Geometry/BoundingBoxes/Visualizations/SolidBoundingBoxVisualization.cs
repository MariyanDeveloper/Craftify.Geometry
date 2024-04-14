using System;
using Autodesk.Revit.DB;
using Craftify.Geometry.BoundingBoxes.Visualizations.Models;
using Craftify.Geometry.Extensions;
using Craftify.Geometry.SolidConverters;

namespace Craftify.Geometry.BoundingBoxes.Visualizations;

public class SolidBoundingBoxVisualization : IBoundingBoxVisualization
{
    public void VisualizeIn(BoundingBoxXYZ boundingBox, Document document, Action<BoundingBoxVisualizationOption>? configOptions = null)
    {
        var options = new BoundingBoxVisualizationOption();
        configOptions?.Invoke(options);
        var solidToVisualize = boundingBox.ConvertToSolid(
            new CuboidBoundingBoxSolidConverter(), converterOptions =>
        {
            converterOptions.ApplyTransform = options.ApplyTransform;
        });
        solidToVisualize.VisualizeIn(document);
    }
}