using System.Linq;
using Craftify.Geometry.Enums;
using Craftify.Geometry.Interfaces;

namespace Craftify.Geometry.Options;

public class BoundingBoxVisualizationOption
{
    public ApplyTransform ApplyTransform { get; set; } = ApplyTransform.Yes;

}