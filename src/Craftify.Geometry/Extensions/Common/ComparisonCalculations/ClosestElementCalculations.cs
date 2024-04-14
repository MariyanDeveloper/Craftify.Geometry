using System;
using System.Collections.Generic;
using Autodesk.Revit.DB;

namespace Craftify.Geometry.Extensions.Curves;

public static class ClosestElementCalculations
{
    public static (T Left, T Right) SelectClosestPairAlongVector<T>(
        this IEnumerable<T> elements,
        XYZ vectorToMeasureBy,
        Func<T, T, XYZ, double> vectorMeasurementFunc)
    {
        return elements
            .SelectClosestPair(
                measureDistanceFunc: (from, to) => vectorMeasurementFunc(from, to, vectorToMeasureBy));
    }
    
    public static (T Left, T Right) SelectClosestPair<T>(
        this IEnumerable<T> elements,
        Func<T, T, double> measureDistanceFunc)
    {
        return elements
            .SelectBestMatchingPairByComparison(
                measureDistanceFunc,
                (current, best) => current < best,
                double.MaxValue
            );
    }
    
    public static TTo SelectClosestElement<TFrom, TTo>(
        this TFrom element,
        IEnumerable<TTo> elements,
        Func<TFrom, TTo, double> measureDistanceFunc)
    {
        return element
            .SelectClosestElementInternal(
                elements: elements,
                comparisonCriteriaFunc: measureDistanceFunc);
    }
    
    
    public static TTo SelectClosestElementAlongVector<TFrom, TTo>(
        this TFrom element,
        IEnumerable<TTo> elements,
        XYZ vectorToMeasureBy,
        Func<TFrom, TTo, XYZ, double> vectorMeasurementFunc)
    {
        return element
            .SelectClosestElementInternal(
                elements: elements,
                comparisonCriteriaFunc: (from, to) => vectorMeasurementFunc(from, to, vectorToMeasureBy));
    }
    
    private static TTo SelectClosestElementInternal<TFrom, TTo>(
        this TFrom element,
        IEnumerable<TTo> elements,
        Func<TFrom, TTo, double> comparisonCriteriaFunc)
    {
        return element.SelectBestMatchingElementByComparison(
            elements: elements,
            comparisonValueSelector: comparisonCriteriaFunc,
            comparisonCriteria: (from, to) => from < to,
            comparisonBaseline: double.MaxValue
        );
    }
}