using System;

namespace Craftify.Geometry.Functional;

public static class ActionExtensions
{
    public static Func<ValueTuple> ToFunc(this Action action) =>
        () =>
        {
            action();
            return default;
        };

    public static Func<T, ValueTuple> ToFunc<T>(this Action<T> action) =>
        (value) =>
        {
            action(value);
            return default;
        };
}