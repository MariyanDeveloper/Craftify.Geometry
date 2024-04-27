using Autodesk.Revit.DB;
using Craftify.Functional;
using Craftify.Geometry.Frontend.VisualizationComponentFeature;

namespace Craftify.Geometry.Frontend;

public interface IOverrideGraphicsByStyleProvider
{
    Validation<OverrideGraphicSettings> Get(IGeometryStyle style);
}