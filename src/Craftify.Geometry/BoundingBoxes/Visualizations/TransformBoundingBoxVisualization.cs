using System;
using Autodesk.Revit.DB;
using Craftify.Geometry.BoundingBoxes.Visualizations.Models;
using Craftify.Geometry.Extensions;
using Craftify.Geometry.Extensions.Transforms;

namespace Craftify.Geometry.BoundingBoxes.Visualizations;

public class TransformBoundingBoxVisualization : IBoundingBoxVisualization
{
    public void VisualizeIn(
        BoundingBoxXYZ boundingBox,
        Document document,
        Action<BoundingBoxVisualizationOption>? configOptions = null)
    {
        boundingBox.Transform.VisualizeIn(document);
    }
}