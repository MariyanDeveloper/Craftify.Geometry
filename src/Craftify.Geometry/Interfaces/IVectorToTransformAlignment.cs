using Autodesk.Revit.DB;

namespace Craftify.Geometry.Interfaces;

public interface IVectorToTransformAlignment
{
    Transform Align(XYZ vector);
}