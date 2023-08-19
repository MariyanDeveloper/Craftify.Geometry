using Craftify.Geometry.Enums;

namespace Craftify.Geometry.Interfaces;

public class BoundingBoxSolidConverterOptions
{
    public ApplyTransform ApplyTransform { get; set; } = ApplyTransform.Yes;
}