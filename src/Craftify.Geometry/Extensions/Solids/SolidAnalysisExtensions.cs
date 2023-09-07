using System;
using Autodesk.Revit.DB;
using Craftify.Shared;

namespace Craftify.Geometry.Extensions.Solids;

public static class SolidAnalysisExtensions
{
    public static bool HasFaces(
        this Solid solid) => solid.Faces.Size > 0;
    
    public static bool HasVolume(
        this Solid solid) => solid.Volume > 0;
    
    public static SolidRelationship CalculateRelationshipWith(
        this Solid fromSolid,
        Solid toSolid,
        double volumeTolerance = 0.00001,
        int areaDecimalPrecision = 5 )
    {
        var unionSolid = fromSolid.CreateUnionSolidWith(toSolid);
        var intersectedSolid = fromSolid.CreateIntersectedSolidWith(toSolid);

        var sumArea = Math.Round(Math.Abs(fromSolid.SurfaceArea + toSolid.SurfaceArea), areaDecimalPrecision);
        var sumFacesCount = Math.Abs(fromSolid.Faces.Size + toSolid.Faces.Size);
        var unionArea = Math.Round(Math.Abs(unionSolid.SurfaceArea), areaDecimalPrecision);
        var sumUnionFacesCount = Math.Abs(unionSolid.Faces.Size);
        
        if (sumArea.IsAlmostEqualTo(unionArea) && sumFacesCount == sumUnionFacesCount && intersectedSolid.Volume < volumeTolerance)
        {
            return SolidRelationship.NoConnection;
        }

        if (sumArea > unionArea && sumFacesCount > sumUnionFacesCount && intersectedSolid.Volume > volumeTolerance)
        {
            return SolidRelationship.Intersecting;
        }

        if (sumArea > unionArea && sumFacesCount > sumUnionFacesCount && intersectedSolid.Volume < volumeTolerance)
        {
            return SolidRelationship.Touching;
        }
        return SolidRelationship.NoConnection;
    }
}