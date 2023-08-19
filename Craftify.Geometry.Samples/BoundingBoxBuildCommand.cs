using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Craftify.Geometry.BoundingBoxVisualizations;
using Craftify.Geometry.Builders;
using Craftify.Geometry.Enums;
using Craftify.Geometry.Extensions;

namespace Craftify.Geometry.Samples;

[Transaction(TransactionMode.Manual)]
[Regeneration(RegenerationOption.Manual)]
public class BoundingBoxBuildCommand : IExternalCommand
{
    public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
    {
        var uiApplication = commandData.Application;
        var application = uiApplication.Application;
        var uiDocument = uiApplication.ActiveUIDocument;
        var document = uiDocument.Document;
        
        //create bounding box using BoundingBoxBuilder
        var box = new BoundingBoxBuilder()
            .OfLength(3)
            .OfWidth(5)
            .OfHeight(4)
            .WithTransform(Transform.CreateTranslation(new XYZ(2, 5, 0)))
            .Build();
        
        //create visualization using chains
        var visualization = new SolidBoundingBoxVisualization()
            .CombineWith(new CornersBoundingBoxVisualization())
            .CombineWith(new CenterBoundingBoxVisualization())
            .CombineWith(new FacesBoundingBoxVisualization())
            .CombineWith(new TransformBoundingBoxVisualization());
        //create visualization using builder
        var visualizationByBuilder = new BoundingBoxVisualizationBuilder()
            .Add(visualization)
            .AddSolidVisualization()
            .AddFacesVisualization()
            .Build();
        
        using (var transaction = new Transaction(document, "Visualize Box"))
        {
            transaction.Start();
            box.VisualizeIn(
                document,
                visualization,
                ApplyTransform.Yes);
            transaction.Commit();
        }
        return Result.Succeeded;
    }
}