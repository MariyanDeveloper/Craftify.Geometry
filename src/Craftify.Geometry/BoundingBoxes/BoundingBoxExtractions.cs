using System;
using System.Collections.Generic;
using System.Linq;
using Autodesk.Revit.DB;
using Craftify.Geometry.Enums;
using Craftify.Geometry.Extensions.Points;

namespace Craftify.Geometry.BoundingBoxes;

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
    public static XYZ GetMin(this BoundingBoxXYZ boundingBox, ApplyTransform applyTransform = ApplyTransform.No)
    {
        if (applyTransform == ApplyTransform.No)
        {
            return boundingBox.Min;
        }
        return boundingBox.Transform.OfPoint(boundingBox.Min);
    }
    
    public static XYZ GetMax(this BoundingBoxXYZ boundingBox, ApplyTransform applyTransform = ApplyTransform.No)
    {
        if (applyTransform == ApplyTransform.No)
        {
            return boundingBox.Max;
        }
        return boundingBox.Transform.OfPoint(boundingBox.Max);
    }

    public static IEnumerable<XYZ> SelectCornerVertices(
        this BoundingBoxXYZ box,
        ApplyTransform applyTransform)
    {
        if (box == null)
        {
            throw new ArgumentNullException(nameof(box));
        }

        return Enumerable.Range(0, 2)
            .Select(i =>
            {
                var vertex = box.get_Bounds(i);
                return applyTransform == ApplyTransform.Yes
                    ? box.Transform.OfPoint(vertex)
                    : vertex;
            });
    }
    public static (XYZ Min, XYZ Max) GetCornerVertices(this BoundingBoxXYZ box,
        ApplyTransform applyTransform = ApplyTransform.Yes)
    {
        var vertices = box.SelectCornerVertices(applyTransform).ToArray();
        return (vertices[0], vertices[1]);
    }
    
}