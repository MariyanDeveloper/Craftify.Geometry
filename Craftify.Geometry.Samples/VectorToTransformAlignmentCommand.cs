using System.Linq;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
using Craftify.Geometry.Extensions;
using Craftify.Geometry.VectorAlignments;

namespace Craftify.Geometry.Samples;

[Transaction(TransactionMode.Manual)]
[Regeneration(RegenerationOption.Manual)]
public class VectorToTransformAlignmentCommand : IExternalCommand
{
    public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
    {
        var uiApplication = commandData.Application;
        var application = uiApplication.Application;
        var uiDocument = uiApplication.ActiveUIDocument;
        var document = uiDocument.Document;
        //Make sure to select curve-based elements
        var curves = uiDocument
            .Selection
            .PickObjects(ObjectType.Element)
            .Select(r => document.GetElement((Reference)r))
            .Select(e => (e.Location as LocationCurve)!.Curve)
            .ToList();
        using (var transaction = new Transaction(document, "Visualize Transforms"))
        {
            transaction.Start();
            foreach (var curve in curves)
            {
                var transform = curve
                    .GetNormalizedVector()
                    .AlignToTransform(new XVectorToTransformAlignment());
                transform.VisualizeIn(document);
            }
            transaction.Commit();
        }
        return Result.Succeeded;
    }
}