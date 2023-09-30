namespace CPU.Emulator.Utils.Enums;

public static class EnumUtils
{
    public static IEnumerable<TEnumType> GetValues<TEnumType>() =>
        GetValues<TEnumType>(typeof(TEnumType));

    public static IEnumerable<TOut> GetValues<TEnumType, TOut>() =>
        GetValues<TOut>(typeof(TEnumType));

    public static IEnumerable<TOut> GetValues<TOut>(Type enumType) =>
        Enum.GetValues(enumType).Cast<TOut>();
}
