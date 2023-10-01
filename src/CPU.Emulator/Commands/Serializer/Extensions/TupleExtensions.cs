namespace CPU.Emulator.Commands.Serializer.Extensions;

public static class TupleExtensions
{
    public static KeyValuePair<T1, T2> ToPair<T1, T2>(this Tuple<T1, T2> tuple)
    {
        return new KeyValuePair<T1, T2>(tuple.Item1, tuple.Item2);
    }

    public static IEnumerable<KeyValuePair<T1, T2>> ToPairs<T1, T2>(
        this IEnumerable<Tuple<T1, T2>> tuples)
    {
        return tuples.Select(tuple => tuple.ToPair());
    }
}
