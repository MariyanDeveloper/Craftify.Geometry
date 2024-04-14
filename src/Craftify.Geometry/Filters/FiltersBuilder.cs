using System;
using System.Collections.Generic;
using System.Linq;
using Autodesk.Revit.DB;

namespace Craftify.Geometry.Filters;

public static class FiltersBuilder
{
   
}

public enum JoinFilterType
{
    Or,
    And
}

public static class FiltersOperations
{
    public static ElementFilter JoinWith(this ElementFilter fromElementFilter, ElementFilter toElementFilter,
        JoinFilterType joinFilterType)
    {
        if (joinFilterType == JoinFilterType.And)
        {
            return new LogicalAndFilter(fromElementFilter, toElementFilter);
        }
        return new LogicalOrFilter(fromElementFilter, toElementFilter);
    }

    public static ElementFilter And(this ElementFilter elementFilter, ElementFilter toFilter)
    {
        return elementFilter.JoinWith(toFilter, JoinFilterType.And);
    }
    public static ElementFilter Or(this ElementFilter elementFilter, ElementFilter toFilter)
    {
        return elementFilter.JoinWith(toFilter, JoinFilterType.Or);
    }

    public static ElementFilter ToExclusionFilter(this IEnumerable<ElementId> ids) =>
        new ExclusionFilter(ids.ToArray());
    public static ElementFilter ToClassFilter(
        this Type type, bool isInverted = false) => new ElementClassFilter(type, isInverted);

    public static ElementFilter ToCategoryFilter(
        this BuiltInCategory builtIntCategory, bool isInverted = false) => new ElementCategoryFilter(
        builtIntCategory, isInverted);

}