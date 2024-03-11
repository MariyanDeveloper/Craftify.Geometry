using System.Linq;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Craftify.Geometry.BoundingBoxVisualizations;
using Craftify.Geometry.Builders;
using Craftify.Geometry.Extensions;
using Craftify.Geometry.Extensions.BoundingBoxes;

namespace Craftify.Geometry.TestCommands;

[Transaction(TransactionMode.Manual)]
[Regeneration(RegenerationOption.Manual)]
public class Command : IExternalCommand
{
    public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
    {
        var uiApplication = commandData.Application;
        var application = uiApplication.Application;
        var uiDocument = uiApplication.ActiveUIDocument;
        var document = uiDocument.Document;
        var box = new BoundingBoxBuilder()
            .OfWidth(-10)
            .OfLength(-20)
            .OfHeight(-10)
            .Build();
        using (var transaction = new Transaction(document, "Name"))
        {
            transaction.Start();
            box.Min.VisualizeIn(document);
            box.Max.VisualizeIn(document);
            box.VisualizeIn(document, new SolidBoundingBoxVisualization());
            transaction.Commit();
        }
        return Result.Succeeded;
    }
}

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
            .Select(x => document.GetElement(x))
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