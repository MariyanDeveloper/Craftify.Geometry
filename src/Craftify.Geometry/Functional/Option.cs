using static Craftify.Geometry.Functional.F;
using System;
using System.Collections.Generic;

namespace Craftify.Geometry.Functional;
public static partial class F
{
    public static Option<T> Some<T>(T value)
        => new(value ?? throw new ArgumentNullException(nameof(value)));

    public static NoneType None => default;
}
public readonly struct Option<T> : IEquatable<NoneType>, IEquatable<Option<T>>
{
    private readonly T? _value;
    private readonly bool _isSome;
    private bool IsNone => !_isSome;

    internal Option(T t) => (_isSome, _value) = (true, t);

    public static implicit operator Option<T>(NoneType _) => default;

    public static implicit operator Option<T>(T t)
        => t is null ? None : new Option<T>(t);

    public TR Match<TR>(Func<TR> none, Func<T, TR> some)
        => _isSome ? some(_value!) : none();

    public IEnumerable<T> AsEnumerable()
    {   
        if (_isSome) yield return _value!;
    }

    public static bool operator true(Option<T> @this) => @this._isSome;
    public static bool operator false(Option<T> @this) => @this.IsNone;
    public static Option<T> operator | (Option<T> l, Option<T> r) => l._isSome ? l : r;


    public bool Equals(Option<T> other)
        => _isSome == other._isSome
           && (IsNone || _value!.Equals(other._value));

    public bool Equals(NoneType _) => IsNone;

    public static bool operator ==(Option<T> @this, Option<T> other) => @this.Equals(other);
    public static bool operator !=(Option<T> @this, Option<T> other) => !(@this == other);

    public override bool Equals(object? other)
        => other is Option<T> option && Equals(option);

    public override int GetHashCode()
        => IsNone ? 0 : _value!.GetHashCode();

    public override string ToString() => _isSome ? $"Some({_value})" : "None";
}