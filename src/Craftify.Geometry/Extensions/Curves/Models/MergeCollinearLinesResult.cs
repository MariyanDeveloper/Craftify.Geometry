using Autodesk.Revit.DB;

namespace Craftify.Geometry.Extensions.Curves.Models;

public abstract record MergeCollinearLinesResult()
{
    public static implicit operator MergeCollinearLinesResult(Line line) => new SuccessfulMergeResult(line);
};