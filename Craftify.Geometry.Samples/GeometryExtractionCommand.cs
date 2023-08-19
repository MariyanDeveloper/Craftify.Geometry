using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Craftify.Geometry.Extensions;

namespace Craftify.Geometry.Samples;

[Transaction(TransactionMode.Manual)]
[Regeneration(RegenerationOption.Manual)]
public class GeometryExtractionCommand : IExternalCommand
{
    public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
    {
        var uiApplication = commandData.Application;
        var application = uiApplication.Application;
        var uiDocument = uiApplication.ActiveUIDocument;
        var document = uiDocument.Document;
        var wall = new FilteredElementCollector(document)
            .OfClass(typeof(Wall))
            .FirstElement();
        var solids = wall
            .get_Geometry(new Autodesk.Revit.DB.Options())
            .ExtractRootGeometries<Solid>()
            .ToSolids();
        using (var transaction = new Transaction(document, "Visualize Wall Solids"))
        {
            transaction.Start();
            solids.VisualizeIn(document);
            transaction.Commit();
        }
        return Result.Succeeded;
    }
}