using System;
using System.Linq;
using Autodesk.Revit.DB;
using Craftify.Geometry.Enums;

namespace Craftify.Geometry.Extensions.BoundingBoxes;

public static class BoundingBoxExtractions
{
    public static XYZ GetCenter(this BoundingBoxXYZ boundingBox, ApplyTransform applyTransform = ApplyTransform.No)
    {
        if (boundingBox == null)
        {
            throw new ArgumentNullException(nameof(boundingBox));
        }

        var center = boundingBox.Max.MoveAlongVector(boundingBox.Min).Multiply(0.5);
        return applyTransform == ApplyTransform.No
            ? center
            : boundingBox.Transform.OfPoint(center);
    }
    public static (XYZ Left, XYZ Right) GetCornerVertices(this BoundingBoxXYZ box,
        ApplyTransform applyTransform = ApplyTransform.Yes)
    {
        if (box == null)
        {
            throw new ArgumentNullException(nameof(box));
        }
        var vertices = Enumerable.Range(0, 2)
            .Select(i =>
            {
                var vertex = box.get_Bounds(i);
                return applyTransform == ApplyTransform.Yes
                    ? box.Transform.OfPoint(vertex)
                    : vertex;
            }).ToArray();
        return (vertices[0], vertices[1]);
    }
    
}