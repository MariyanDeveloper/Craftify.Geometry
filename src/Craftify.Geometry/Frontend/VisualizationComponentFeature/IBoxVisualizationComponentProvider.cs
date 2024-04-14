using Autodesk.Revit.DB;
using Craftify.Geometry.Enums;

namespace Craftify.Geometry.Frontend.VisualizationComponentFeature;

public interface IBoxVisualizationComponentProvider
{
    IVisualizationComponent Resolve(
        BoundingBoxXYZ boundingBox,
        ApplyTransform applyTransform);
}