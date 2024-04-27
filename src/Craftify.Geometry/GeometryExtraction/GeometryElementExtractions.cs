using System;
using System.Collections.Generic;
using System.Linq;
using Autodesk.Revit.DB;
using Craftify.Shared;

namespace Craftify.Geometry.GeometryExtraction;

public static class GeometryElementExtractions
{
    public static T Match<T>(
        this GeometryObject geometryObject,
        Func<GeometryObject, T> leaf,
        Func<GeometryElement, T> geometryElementBranch,
        Func<GeometryInstance, T> geometryInstanceBranch)
    {
        return geometryObject switch
        {
            GeometryElement ge => geometryElementBranch(ge),
            GeometryInstance gi => geometryInstanceBranch(gi),
            not null => leaf(geometryObject),
            _ => throw new ArgumentOutOfRangeException(nameof(geometryObject), geometryObject, null)
        };
    }

    
    public static IEnumerable<T> SelectChildrenOfType<T>(
        this GeometryElement geometryElement,
        GeometryExtractionConfig? config = default) where T : GeometryObject
    {
        var options = config ?? GeometryExtractionConfig.Default;
        return geometryElement.Flatten(options)
            .OfType<T>();
    }
    

    
    
    public static IEnumerable<T> SelectChildrenOfType<T>(
        this GeometryElement geometryElement,
        Action<GeometryExtractionConfigBuilder> configureSettings) where T : GeometryObject
    {
        return geometryElement.Flatten(configureSettings)
            .OfType<T>();
    }

    public static IEnumerable<GeometryObject> Flatten(
        this GeometryElement geometryElement,
        GeometryExtractionConfig? config = default)
    {
        var options = config ?? GeometryExtractionConfig.Default;
        return geometryElement.SelectMany(x =>
        {
            return x.Match(
                leaf: l => l.AsEnumerable(),
                geometryElementBranch: e => e.Flatten(options),
                geometryInstanceBranch: i => options.GeometryRepresentation.Match(
                    symbol: () => i.GetSymbolGeometry().Flatten(options),
                    instance: () => i.GetInstanceGeometry().Flatten(options)
                ));
        });
    }
    
    public static IEnumerable<GeometryObject> Flatten(
        this GeometryElement geometryElement,
        Action<GeometryExtractionConfigBuilder> configSetup)
    {
        var builder = GeometryExtractionConfigBuilder.Create();
        configSetup.Invoke(builder);
        var options = builder.Build();
        return geometryElement.Flatten(options);
    }
}