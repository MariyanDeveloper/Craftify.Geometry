using System;

namespace Craftify.Geometry.Functional;

public static class PipeExtensions
{
    public static TOutput Pipe<TInput, TOutput>(this TInput input, Func<TInput, TOutput> func)
        => func(input);
    
    public static TR Pipe<TA, TB, TR>(this TA self, Func<TA, TB> func1, Func<TB, TR> func2)
    {
        return func2(func1(self));
    }
}