using System;
using Autodesk.Revit.DB;
using Craftify.Geometry.Extensions;
using Craftify.Geometry.Options;

namespace Craftify.Geometry.BoundingBoxVisualizations;

public class TransformBoundingBoxVisualization : IBoundingBoxVisualization
{
    public void VisualizeIn(BoundingBoxXYZ boundingBox, Document document, Action<BoundingBoxVisualizationOption>? configOptions = null)
    {
        boundingBox.Transform.VisualizeIn(document);
    }
}