using Craftify.Geometry.BoundingBoxVisualizations;

namespace Craftify.Geometry.Extensions;

public static class BoundingBoxVisualizationExtensions
{
    public static IBoundingBoxVisualization CombineWith(this IBoundingBoxVisualization current,
        IBoundingBoxVisualization next)
    {
        return new ChainedBoundingBoxVisualization(current, next);
    }
}