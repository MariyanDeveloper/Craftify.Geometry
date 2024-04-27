using Craftify.Functional;

namespace Craftify.Geometry.Frontend;

public sealed record PatternNotFoundError(string Message) : Error(Message);