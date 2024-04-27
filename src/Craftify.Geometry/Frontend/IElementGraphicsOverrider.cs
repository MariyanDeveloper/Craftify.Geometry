using Autodesk.Revit.DB;

namespace Craftify.Geometry.Frontend;

public interface IElementGraphicsOverrider
{
    void Override(ElementId id, OverrideGraphicSettings overrideGraphicSettings);
}