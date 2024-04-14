using System;
using Autodesk.Revit.DB;
using Craftify.Geometry.BoundingBoxes.Visualizations.Models;

namespace Craftify.Geometry.BoundingBoxes.Visualizations;

public interface IBoundingBoxVisualization
{
    void VisualizeIn(BoundingBoxXYZ boundingBox, Document document, Action<BoundingBoxVisualizationOption>? configOptions = null);
}