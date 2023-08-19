using System;
using Autodesk.Revit.DB;
using Craftify.Geometry.Options;

namespace Craftify.Geometry.BoundingBoxVisualizations;

public class AggregateBoundingBoxVisualization : IBoundingBoxVisualization
{
    private readonly IBoundingBoxVisualization[] _visualizations;

    public AggregateBoundingBoxVisualization(params IBoundingBoxVisualization[] visualizations)
    {
        _visualizations = visualizations ?? throw new ArgumentNullException(nameof(visualizations));
    }
    public void VisualizeIn(BoundingBoxXYZ boundingBox, Document document, Action<BoundingBoxVisualizationOption>? configOptions = null)
    {
        foreach (var boundingBoxVisualization in _visualizations)
        {
            boundingBoxVisualization.VisualizeIn(boundingBox, document, configOptions);
        }
    }
}