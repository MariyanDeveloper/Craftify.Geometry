using Autodesk.Revit.DB;
using Craftify.Functional;
using Craftify.Geometry.BoundingBoxes;
using Craftify.Geometry.Enums;
using static Craftify.Functional.F;
namespace Craftify.Geometry.Frontend.VisualizationComponentFeature;

public class SolidVisualizationComponentProvider : IBoxVisualizationComponentProvider
{
    private readonly Option<IGeometryStyle> _geometryStyle;
    public SolidVisualizationComponentProvider(IGeometryStyle? geometryStyle = default)
    {
        _geometryStyle = geometryStyle is null? 
            None
            : Some(geometryStyle);
    }
    public IVisualizationComponent Resolve(
        BoundingBoxXYZ boundingBox,
        ApplyTransform applyTransform)
    {
        var solid = boundingBox.ConvertToSolid(configOptions: options =>
        {
            options.ApplyTransform = applyTransform;
        });
        return new VisualizationComponent(
            solid, _geometryStyle);
    }
}