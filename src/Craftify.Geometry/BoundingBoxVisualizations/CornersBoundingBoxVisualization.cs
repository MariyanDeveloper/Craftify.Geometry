using System;
using Autodesk.Revit.DB;
using Craftify.Geometry.Extensions;
using Craftify.Geometry.Options;

namespace Craftify.Geometry.BoundingBoxVisualizations;

public class CornersBoundingBoxVisualization : IBoundingBoxVisualization
{
    public void VisualizeIn(BoundingBoxXYZ boundingBox, Document document, Action<BoundingBoxVisualizationOption>? configOptions = null)
    {
        var options = new BoundingBoxVisualizationOption();
        configOptions?.Invoke(options);
        var cornerVertices = boundingBox
            .GetCornerVertices(options.ApplyTransform);
        cornerVertices.VisualizeIn(document);
    }
}