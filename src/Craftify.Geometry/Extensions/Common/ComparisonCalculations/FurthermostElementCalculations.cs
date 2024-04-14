using System;
using System.Collections.Generic;
using Autodesk.Revit.DB;

namespace Craftify.Geometry.Extensions.Curves;

public static class FurthermostElementCalculations
{
    public static (T Left, T Right) SelectFurthermostPairAlongVector<T>(
        this IEnumerable<T> elements,
        XYZ vectorToMeasureBy,
        Func<T, T, XYZ, double> vectorMeasurementFunc)
    {
        return elements.SelectFurthermostPair(
            measureDistanceFunc: (from, to) => vectorMeasurementFunc(from, to, vectorToMeasureBy));
    }
    
    public static (T Left, T Right) SelectFurthermostPair<T>(
        this IEnumerable<T> elements,
        Func<T, T, double> measureDistanceFunc)
    {
        return elements
            .SelectBestMatchingPairByComparison(
                measureDistanceFunc,
                (current, best) => current > best,
                double.MinValue
            );
    }
    
    public static TTo SelectFurthermostElement<TFrom, TTo>(
        this TFrom element,
        IEnumerable<TTo> elements,
        Func<TFrom, TTo, double> measureDistanceFunc)
    {
        return element
            .SelectFurthermostElementInternal(
                elements: elements,
                comparisonCriteriaFunc: measureDistanceFunc);
    }
    public static TTo SelectFurthermostElementAlongVector<TFrom, TTo>(
        this TFrom element,
        IEnumerable<TTo> elements,
        XYZ vectorToMeasureBy,
        Func<TFrom, TTo, XYZ, double> vectorMeasurementFunc)
    {
        return element
            .SelectFurthermostElementInternal(
                elements: elements,
                comparisonCriteriaFunc: (from, to) => vectorMeasurementFunc(from, to, vectorToMeasureBy));
    }
    
    private static TTo SelectFurthermostElementInternal<TFrom, TTo>(
        this TFrom element,
        IEnumerable<TTo> elements,
        Func<TFrom, TTo, double> comparisonCriteriaFunc)
    {
        return element.SelectBestMatchingElementByComparison(
            elements: elements,
            comparisonValueSelector: comparisonCriteriaFunc,
            comparisonCriteria: (from, to) => from > to,
            comparisonBaseline: double.MinValue
        );
    }
}