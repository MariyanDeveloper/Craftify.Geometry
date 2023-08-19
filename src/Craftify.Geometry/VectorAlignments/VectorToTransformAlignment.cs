using Craftify.Geometry.Interfaces;

namespace Craftify.Geometry.VectorAlignments;

public static class VectorToTransformAlignment
{
    public static IVectorToTransformAlignment DefaultViewTransformAlignment = new YVectorToTransformAlignment();
}