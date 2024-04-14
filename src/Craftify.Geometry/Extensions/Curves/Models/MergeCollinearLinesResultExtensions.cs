using System;
using Autodesk.Revit.DB;

namespace Craftify.Geometry.Extensions.Curves.Models;

public static class MergeCollinearLinesResultExtensions
{
    public static T Cast<T>(this MergeCollinearLinesResult result) where T : MergeCollinearLinesResult
    {
        return (T)result;
    }
    public static Line GetLineUnsafe(this MergeCollinearLinesResult result)
    {
        return result.Cast<SuccessfulMergeResult>().Line;
    }
    public static T Match<T>(
        this MergeCollinearLinesResult result,
        Func<T> linesNotCollinear,
        Func<Line, T> successful) =>
        result switch
        {
            LinesNotCollinearResult => linesNotCollinear(),
            SuccessfulMergeResult successfulMergeResult => successful(successfulMergeResult.Line),
            _ => throw new ArgumentOutOfRangeException(nameof(result))
        };
}