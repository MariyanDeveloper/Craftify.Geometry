using System;
using Autodesk.Revit.DB;
using Craftify.Geometry.BoundingBoxes.Visualizations.Models;
using Craftify.Geometry.Extensions;
using Craftify.Geometry.Extensions.Points;

namespace Craftify.Geometry.BoundingBoxes.Visualizations;

public class CornersBoundingBoxVisualization : IBoundingBoxVisualization
{
    public void VisualizeIn(BoundingBoxXYZ boundingBox, Document document, Action<BoundingBoxVisualizationOption>? configOptions = null)
    {
        var options = new BoundingBoxVisualizationOption();
        configOptions?.Invoke(options);
        var cornerVertices = boundingBox
            .GetCornerVertices(options.ApplyTransform);
        document.CreateDirectShape(new[]
        {
            cornerVertices.Min.ToPoint(),
            cornerVertices.Max.ToPoint()
        });
    }
}