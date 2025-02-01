using Autodesk.Revit.DB;

namespace Craftify.Geometry.Extensions.Common.PointIntersectionModels;

public record ExactIntersectionResult(XYZ IntersectionPoint) : PointIntersectionResult;