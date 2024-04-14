using Autodesk.Revit.DB;
using Craftify.Functional;

namespace Craftify.Geometry.Frontend.VisualizationComponentFeature;

public record VisualizationComponent(
        GeometryObject GeometryObject, Option<IGeometryStyle> GeometryStyle)
    : IVisualizationComponent;