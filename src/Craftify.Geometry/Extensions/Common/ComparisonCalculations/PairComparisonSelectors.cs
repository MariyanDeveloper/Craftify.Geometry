using System;
using System.Collections.Generic;
using System.Linq;

namespace Craftify.Geometry.Extensions.Common.ComparisonCalculations;

public static class PairComparisonSelectors
{
    public static TTo SelectBestMatchingElementByComparison<TFrom, TTo, TComparerValue>(
        this TFrom element,
        IEnumerable<TTo> elements,
        Func<TFrom, TTo, TComparerValue> comparisonValueSelector,
        Func<TComparerValue, TComparerValue, bool> comparisonCriteria,
        TComparerValue comparisonBaseline)
    {
        var currentBest = comparisonBaseline;
        TTo result = default;
        foreach (var toElement in elements)
        {
            var comparisonValue = comparisonValueSelector(element, toElement);
            if (comparisonCriteria(comparisonValue, currentBest) is false)
            {
                continue;
            }
            currentBest = comparisonValue;
            result = toElement;
        }
        return result ?? throw new InvalidOperationException(nameof(result));

    }
    
    public static (T Left, T Right) SelectBestMatchingPairByComparison<T, TComparerValue>(
        this IEnumerable<T> elements,
        Func<T, T, TComparerValue> comparisonValueSelector,
        Func<TComparerValue, TComparerValue, bool> comparisonCriteria,
        TComparerValue comparisonBaseline)
    {
        T leftValue = default!;
        T rightValue = default!;
        var currentBest = comparisonBaseline;
        var array = elements.ToArray();
        for (var i = 0; i < array.Length; i++)
        {
            for (var j = i + 1; j < array.Length; j++)
            {
                var fromComponent = array[i];
                var toComponent = array[j];
                var comparisonValue = comparisonValueSelector(
                    fromComponent,
                    toComponent);
                if (comparisonCriteria(comparisonValue, currentBest) is false)
                {
                    continue;
                }

                leftValue = fromComponent;
                rightValue = toComponent;
                currentBest = comparisonValue;
            }
        }

        return (leftValue, rightValue);
    }

}