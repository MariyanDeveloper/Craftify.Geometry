using Autodesk.Revit.DB;

namespace Craftify.Geometry.Extensions.Common.Intersections;

public record ExactIntersectionResult(XYZ IntersectionPoint) : PointIntersectionResult;