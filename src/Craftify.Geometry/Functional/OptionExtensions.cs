using System;
using Unit = System.ValueTuple;
namespace Craftify.Geometry.Functional;

public static class OptionExtensions
{
    public static Option<Unit> ForEach<T>(this Option<T> self, Action<T> action) =>
        self.Map(action.ToFunc());
    
    
    public static Option<T> Where<T>(this Option<T> self, Func<T, bool> predicate)
    {
        return self.Match(
            () => F.None,
            value => predicate(value) ? self : F.None);
    }
    public static Option<TR> Bind<T, TR>(this Option<T> self, Func<T, Option<TR>> func) =>
        self.Match(
            () => F.None,
            func);

    public static Option<TR> Map<T, TR>(this Option<T> self, Func<T, TR> func) =>
        self.Match<Option<TR>>(
            () => F.None,
            value => func(value));
}