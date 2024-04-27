using Autodesk.Revit.DB;

namespace Craftify.Geometry.Frontend;

public interface IReadPatternIdRepository
{
    ElementId GetByName(string patternName);
}