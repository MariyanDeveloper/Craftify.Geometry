using System.Collections.Generic;
using System.Linq;
using Autodesk.Revit.DB;
using Craftify.Geometry.Enums;

namespace Craftify.Geometry.Extensions.BoundingBoxes;

public static class BoundingBoxXYZSideLookup
{
     public static IEnumerable<CurveLoop> GetCurveLoopOfAllSides(
         this BoundingBoxXYZ boundingBox,
         ApplyTransform applyTransform = ApplyTransform.No)
     {
         return GetAllSides()
             .Select(x => boundingBox.GetCurveLoop(x, applyTransform));
     }

    public static CurveLoop GetCurveLoop(
        this BoundingBoxXYZ boundingBox,
        FaceSide faceSide,
        ApplyTransform applyTransform = ApplyTransform.No)
    {
        return faceSide switch
        {
            FaceSide.Bottom => boundingBox.GetBaseCurveLoop(applyTransform),
            FaceSide.Top => boundingBox.GetTopCurveLoop(applyTransform),
            FaceSide.Front => boundingBox.GetFrontCurveLoop(applyTransform),
            FaceSide.Back => boundingBox.GetBackCurveLoop(applyTransform),
            FaceSide.Left => boundingBox.GetLeftCurveLoop(applyTransform),
            _ => boundingBox.GetRightCurveLoop(applyTransform),
        };
    }
    
    private static CurveLoop GetRightCurveLoop(this BoundingBoxXYZ boundingBox, ApplyTransform applyTransform)
    {
        var (length, width, height) = boundingBox.CalculateDimension();
        var pt1 = boundingBox.Min.MoveAlongVector(XYZ.BasisX, length);
        var pt2 = pt1.MoveAlongVector(XYZ.BasisY * width);
        var pt3 = pt2.MoveAlongVector(XYZ.BasisZ * height);
        var pt4 = pt1.MoveAlongVector(XYZ.BasisZ * height);
        return CreateSideCurveLoop(pt1, pt2, pt3, pt4, boundingBox.Transform, applyTransform);
    }
    private static CurveLoop GetLeftCurveLoop(this BoundingBoxXYZ boundingBox, ApplyTransform applyTransform)
    {
        var boxDimension = boundingBox.CalculateDimension();
        var pt1 = boundingBox.Min;
        var pt2 = pt1.MoveAlongVector(XYZ.BasisY * boxDimension.Width);
        var pt3 = pt2.MoveAlongVector(XYZ.BasisZ * boxDimension.Height);
        var pt4 = pt1.MoveAlongVector(XYZ.BasisZ * boxDimension.Height);
        return CreateSideCurveLoop(pt1, pt2, pt3, pt4, boundingBox.Transform, applyTransform);
    }
    private static CurveLoop GetBackCurveLoop(this BoundingBoxXYZ boundingBox, ApplyTransform applyTransform)
    {
        var boxDimension = boundingBox.CalculateDimension();
        var pt1 = boundingBox.Min.MoveAlongVector(XYZ.BasisY, boxDimension.Width);
        var pt2 = pt1.MoveAlongVector(XYZ.BasisX * boxDimension.Length);
        var pt3 = pt2.MoveAlongVector(XYZ.BasisZ * boxDimension.Height);
        var pt4 = pt1.MoveAlongVector(XYZ.BasisZ * boxDimension.Height);
        return CreateSideCurveLoop(pt1, pt2, pt3, pt4, boundingBox.Transform, applyTransform);
    }
    private static CurveLoop GetFrontCurveLoop(this BoundingBoxXYZ boundingBox, ApplyTransform applyTransform)
    {
        var boxDimension = boundingBox.CalculateDimension();
        var pt1 = boundingBox.Min;
        var pt2 = pt1.MoveAlongVector(XYZ.BasisX * boxDimension.Length);
        var pt3 = pt2.MoveAlongVector(XYZ.BasisZ * boxDimension.Height);
        var pt4 = pt1.MoveAlongVector(XYZ.BasisZ * boxDimension.Height);
        return CreateSideCurveLoop(pt1, pt2, pt3, pt4, boundingBox.Transform, applyTransform);
    }
    private static CurveLoop GetTopCurveLoop(this BoundingBoxXYZ boundingBox, ApplyTransform applyTransform)
    {
        var boxDimension = boundingBox.CalculateDimension();
        var pt1 = boundingBox.Min.MoveAlongVector(XYZ.BasisZ * boxDimension.Height);
        var pt2 = pt1.MoveAlongVector(XYZ.BasisX * boxDimension.Length);
        var pt3 = pt2.MoveAlongVector(XYZ.BasisY * boxDimension.Width);
        var pt4 = pt1.MoveAlongVector(XYZ.BasisY * boxDimension.Width);
        return CreateSideCurveLoop(pt1, pt2, pt3, pt4, boundingBox.Transform, applyTransform);
    }
    private static CurveLoop GetBaseCurveLoop(this BoundingBoxXYZ boundingBox, ApplyTransform applyTransform)
    {
        var boxDimension = boundingBox.CalculateDimension();
        var pt1 = boundingBox.Min;
        var pt2 = pt1.MoveAlongVector(XYZ.BasisX * boxDimension.Length);
        var pt3 = pt2.MoveAlongVector(XYZ.BasisY * boxDimension.Width);
        var pt4 = pt1.MoveAlongVector(XYZ.BasisY * boxDimension.Width);
        return CreateSideCurveLoop(pt1, pt2, pt3, pt4, boundingBox.Transform, applyTransform);
    }
    
    private static IEnumerable<FaceSide> GetAllSides()
    {
        return new[]
        {
            FaceSide.Bottom, FaceSide.Top, FaceSide.Left, FaceSide.Right, FaceSide.Front, FaceSide.Back
        };
    }
    private static CurveLoop CreateSideCurveLoop(
        XYZ pt1,
        XYZ pt2,
        XYZ pt3,
        XYZ pt4,
        Transform transform,
        ApplyTransform applyTransform)
    {
        var firstLine = applyTransform == ApplyTransform.No
            ? Line.CreateBound(pt1, pt2)
            : Line.CreateBound(pt1, pt2).CreateTransformed(transform);
        var secondLine = applyTransform == ApplyTransform.No
            ? Line.CreateBound(pt2, pt3)
            : Line.CreateBound(pt2, pt3).CreateTransformed(transform);
        var thirdLine = applyTransform == ApplyTransform.No
            ? Line.CreateBound(pt3, pt4)
            : Line.CreateBound(pt3, pt4).CreateTransformed(transform);
        var forthLine = applyTransform == ApplyTransform.No
            ? Line.CreateBound(pt4, pt1)
            : Line.CreateBound(pt4, pt1).CreateTransformed(transform);
        var curveLoop = CurveLoop.Create(
            new List<Curve>()
            {
                firstLine,
                secondLine,
                thirdLine,
                forthLine
            }
        );
        return curveLoop;
    }
    
}