using System.Collections.Generic;
using System.Linq;
using Autodesk.Revit.DB;
using Craftify.Geometry.Extensions.Points;

namespace Craftify.Geometry.Extensions.Faces;

public static class FaceAnalysisExtensions
{

    public static bool CenterNormalMatchesDirection(
        this Face face, XYZ direction)
    {
        return face.GetCenterNormal().IsAlmostEqualTo(direction);
    }
    
    public static UV GetCenterNormalUV(
        this Face face)
    {
        var sumDivision = 2;
        var uvBounds = face
            .GetBoundsOfBoundingBox()
            .ToArray();
        var normalUV = new UV(
            uvBounds.Sum(uv => uv.U) / sumDivision,
            uvBounds.Sum(uv => uv.V) / sumDivision
        );
        return normalUV;
    }

    public static IEnumerable<UV> GetBoundsOfBoundingBox(this Face face) =>
        face.GetBoundingBox().SelectBounds();

    public static XYZ GetCenterNormal(
        this Face face)
    {
        return face.ComputeNormal(face.GetCenterNormalUV());
    }
    
    public static XYZ GetCenterPoint(
        this Face face)
    {
        return face.Evaluate(face.GetCenterNormalUV());
    }
    
    public static Curve GetCenterNormalAsCurve(
        this Face face)
    {
        var centerPoint = face.GetCenterPoint();
        return Line.CreateBound(centerPoint, centerPoint.MoveAlongVector(face.GetCenterNormal()));
    }

    public static Plane GetPlaneAtCenter(this Face face)
    {
        return Plane.CreateByNormalAndOrigin(face.GetCenterNormal(), face.GetCenterPoint());
    }
}