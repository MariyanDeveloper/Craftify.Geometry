using Autodesk.Revit.DB;
using Craftify.Geometry.BoundingBoxVisualizations;
using Craftify.Geometry.Enums;

namespace Craftify.Geometry.Extensions.BoundingBoxes;

public static class BoundingBoxVisualizationExtensions
{
    public static IBoundingBoxVisualization CombineWith(this IBoundingBoxVisualization current,
        IBoundingBoxVisualization next)
    {
        return new ChainedBoundingBoxVisualization(current, next);
    }
    
    public static void VisualizeIn(
        this BoundingBoxXYZ boundingBox,
        Document document,
        IBoundingBoxVisualization boundingBoxVisualization,
        ApplyTransform applyTransform = ApplyTransform.No)
    {
        boundingBoxVisualization.VisualizeIn(
            boundingBox,
            document,
            options => options.ApplyTransform = applyTransform);
    }
}