using System.Collections.Generic;

namespace Craftify.Geometry.Frontend.VisualizationComponentFeature;

public record CompositeVisualizationComponent(
    IEnumerable<IVisualizationComponent> GeometryComponents,
    IsUnion IsUnion) : IVisualizationComponent;