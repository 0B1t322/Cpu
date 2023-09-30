namespace CPU.Emulator.Utils.Int32;

public static class Int32Utils
{
    public static System.Int32 Concatenate(Int16 high, Int16 low)
    {
        var h = ((int)high) << 16;
        var l = low;
        var value = h | (ushort)l;
        return value;
    }
}
