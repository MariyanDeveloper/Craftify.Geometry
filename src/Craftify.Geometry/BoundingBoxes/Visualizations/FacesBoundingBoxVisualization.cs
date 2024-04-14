using System;
using Autodesk.Revit.DB;
using Craftify.Geometry.BoundingBoxes.Visualizations.Models;
using Craftify.Geometry.Extensions;

namespace Craftify.Geometry.BoundingBoxes.Visualizations;

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