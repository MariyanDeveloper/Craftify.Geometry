using Autodesk.Revit.DB;

namespace Craftify.Geometry.Extensions.Curves.Models;

public static class MergeCollinearLinesResults
{
    public static MergeCollinearLinesResult CreateNotCollinearLinesResult()
        => new LinesNotCollinearResult();
    
    public static MergeCollinearLinesResult CreateSuccessfulResult(Line line)
        => new SuccessfulMergeResult(line);
    
}