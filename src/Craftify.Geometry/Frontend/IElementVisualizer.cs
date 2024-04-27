using Craftify.Geometry.Frontend.VisualizationComponentFeature;

namespace Craftify.Geometry.Frontend;

public interface IElementVisualizer
{
    void VisualizeApplyingStyle(
        IVisualizationComponent visualizationComponent,
        IGeometryStyle geometryStyle);
}