using System;
using Craftify.Geometry.Enums;

namespace Craftify.Geometry.GeometryExtraction;

public static class GeometryRepresentationExtensions
{
    public static TR Match<TR>(
        this GeometryRepresentation geometryRepresentation,
        Func<TR> symbol,
        Func<TR> instance)
        => geometryRepresentation switch
        {
            GeometryRepresentation.Symbol => symbol(),
            GeometryRepresentation.Instance => instance(),
            _ => throw new ArgumentOutOfRangeException(nameof(geometryRepresentation), geometryRepresentation, null)
        };
}