namespace CPU.Emulator.Registers;

public class Int16Register
{
    public Int16 Value { get; set; } = 0;

    public Int16Register()
    {
        Value = 0;
    }

    public Int16Register(Int16 value)
    {
        Value = value;
    }

    public static Int32Register Concatenate(Int16Register high, Int16Register low)
    {
        var h = high.Value << 16;
        var l = low.Value;
        var value = h | l;
        return new Int32Register(value);
    }
}

// 0111_1111_1111_1111__0000_0000_0000_0000
