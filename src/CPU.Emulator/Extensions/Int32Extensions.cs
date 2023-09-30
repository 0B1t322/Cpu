namespace CPU.Emulator.Extensions;

public static class Int32Extensions
{
    public static (Int16, Int16) Deconstruct(this Int32 value)
    {
        short h = (short)(value >> 16);
        short l = (short)(value);

        return (h, l);
    }
    
    public static void Deconstruct(this Int32 value, out short h, out short l)
    {
        h = (short)(value >> 16);
        l = (short)(value);
    }
}
