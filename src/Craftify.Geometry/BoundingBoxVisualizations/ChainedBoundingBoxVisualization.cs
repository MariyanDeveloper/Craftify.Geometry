using System;
using Autodesk.Revit.DB;
using Craftify.Geometry.Options;

namespace Craftify.Geometry.BoundingBoxVisualizations;

public class ChainedBoundingBoxVisualization : IBoundingBoxVisualization
{
    private readonly IBoundingBoxVisualization _currentVisualization;
    private readonly IBoundingBoxVisualization _nextVisualization;

    public ChainedBoundingBoxVisualization(IBoundingBoxVisualization currentVisualization, IBoundingBoxVisualization nextVisualization)
    {
        _currentVisualization = currentVisualization ?? throw new ArgumentNullException(nameof(currentVisualization));
        _nextVisualization = nextVisualization ?? throw new ArgumentNullException(nameof(nextVisualization));
    }
    public void VisualizeIn(BoundingBoxXYZ boundingBox, Document document, Action<BoundingBoxVisualizationOption>? configOptions = null)
    {
        _currentVisualization.VisualizeIn(boundingBox, document, configOptions);
        _nextVisualization.VisualizeIn(boundingBox, document, configOptions);
    }
}