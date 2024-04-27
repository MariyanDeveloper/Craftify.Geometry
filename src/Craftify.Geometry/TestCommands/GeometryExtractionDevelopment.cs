using System.Linq;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;

namespace Craftify.Geometry.TestCommands;

[Transaction(TransactionMode.Manual)]
[Regeneration(RegenerationOption.Manual)]
public class GeometryExtractionDevelopment : IExternalCommand
{
    public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
    {
        var uiApplication = commandData.Application;
        var application = uiApplication.Application;
        var uiDocument = uiApplication.ActiveUIDocument;
        var document = uiDocument.Document;
        var wall = uiDocument
            .Selection
            .GetElementIds()
            .Select(x => document.GetElement((ElementId)x))
            .First();

        // var geometryElement = wall
        //     .get_Geometry(new Autodesk.Revit.DB.Options()); 
        // var result = geometryElement
        //     .Flatten();
        // var solids = geometryElement
        //     .SelectChildrenOfType<Solid>()
        //     .Aggregate((a, n) => a.UnionWith());
        return Result.Succeeded;
    }
}