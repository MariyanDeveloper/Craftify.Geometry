using Autodesk.Revit.DB;
using Craftify.Geometry.Enums;
using Craftify.Geometry.Frontend.VisualizationComponentFeature;

namespace Craftify.Geometry.Frontend;

public interface IBoundingBoxRenderer
{
    void Render(
        BoundingBoxXYZ boundingBox,
        IBoxVisualizationComponentProvider boxVisualizationComponentProvider,
        ApplyTransform applyTransform);
}