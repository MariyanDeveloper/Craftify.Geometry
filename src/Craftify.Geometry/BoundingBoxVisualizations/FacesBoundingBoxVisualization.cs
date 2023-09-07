using System;
using Autodesk.Revit.DB;
using Craftify.Geometry.Extensions;
using Craftify.Geometry.Extensions.BoundingBoxes;
using Craftify.Geometry.Options;

namespace Craftify.Geometry.BoundingBoxVisualizations;

public class FacesBoundingBoxVisualization : IBoundingBoxVisualization
{
    public void VisualizeIn(BoundingBoxXYZ boundingBox, Document document, Action<BoundingBoxVisualizationOption>? configOptions = null)
    {
        var options = new BoundingBoxVisualizationOption();
        configOptions?.Invoke(options);
        var curveLoops = boundingBox.GetCurveLoopOfAllSides(options.ApplyTransform);
        foreach (var curveLoop in curveLoops)
        {
            curveLoop.VisualizeIn(document);
        }
        
    }
}