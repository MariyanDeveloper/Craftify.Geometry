using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Craftify.Geometry.BoundingBoxes;
using Craftify.Geometry.BoundingBoxes.Builders;
using Craftify.Geometry.BoundingBoxes.Visualizations;
using Craftify.Geometry.Extensions.Points;

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