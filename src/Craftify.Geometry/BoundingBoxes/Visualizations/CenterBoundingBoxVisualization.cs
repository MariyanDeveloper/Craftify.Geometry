using System;
using Autodesk.Revit.DB;
using Craftify.Geometry.BoundingBoxes.Visualizations.Models;
using Craftify.Geometry.Extensions.Points;

namespace Craftify.Geometry.BoundingBoxes.Visualizations;

public class CenterBoundingBoxVisualization : IBoundingBoxVisualization
{
    public void VisualizeIn(BoundingBoxXYZ boundingBox, Document document, Action<BoundingBoxVisualizationOption>? configOptions = null)
    {
        var options = new BoundingBoxVisualizationOption();
        configOptions?.Invoke(options);
        boundingBox
            .GetCenter(options.ApplyTransform)
            .VisualizeIn(document);
    }
}