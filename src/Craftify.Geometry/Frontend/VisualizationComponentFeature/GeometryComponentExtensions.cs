using System;

namespace Craftify.Geometry.Frontend.VisualizationComponentFeature;

public static class GeometryComponentExtensions
{
    public static TR Match<TR>(this IVisualizationComponent visualizationComponent,
        Func<VisualizationComponent, TR> defaultHandler,
        Func<CompositeVisualizationComponent, TR> compositeHandler)
    {
        return visualizationComponent switch
        {
            CompositeVisualizationComponent x => compositeHandler(x),
            VisualizationComponent x => defaultHandler(x),
            _ => throw new ArgumentOutOfRangeException(nameof(visualizationComponent))
        };
    }

    public static TR Cast<TR>(this IVisualizationComponent visualizationComponent) where TR : IVisualizationComponent
    {
        return (TR)visualizationComponent;
    }
}