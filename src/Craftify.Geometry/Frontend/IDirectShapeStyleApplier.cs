using Autodesk.Revit.DB;
using Craftify.Geometry.Frontend.VisualizationComponentFeature;

namespace Craftify.Geometry.Frontend;

public interface IDirectShapeStyleApplier
{
    DirectShape Apply(DirectShape directShape, IGeometryStyle geometryStyle);
}