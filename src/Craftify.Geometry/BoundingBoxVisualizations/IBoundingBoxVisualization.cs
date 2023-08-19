using System;
using Autodesk.Revit.DB;
using Craftify.Geometry.Options;

namespace Craftify.Geometry.BoundingBoxVisualizations;

public interface IBoundingBoxVisualization
{
    void VisualizeIn(BoundingBoxXYZ boundingBox, Document document, Action<BoundingBoxVisualizationOption>? configOptions = null);
}