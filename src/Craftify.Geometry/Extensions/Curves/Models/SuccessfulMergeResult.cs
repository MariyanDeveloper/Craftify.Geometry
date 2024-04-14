using Autodesk.Revit.DB;

namespace Craftify.Geometry.Extensions.Curves.Models;

public record SuccessfulMergeResult(Line Line) : MergeCollinearLinesResult;