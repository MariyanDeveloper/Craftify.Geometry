using System.Collections.Generic;
using Autodesk.Revit.DB;

namespace Craftify.Geometry.Extensions.Common.PointIntersectionModels;

public record MultiPointIntersectionResult(IReadOnlyCollection<XYZ> IntersectionPoints) : PointIntersectionResult;