using CPU.Emulator.Extensions;
using CPU.Emulator.Utils.Int32;

namespace CPU.Emulator.Registers;

public class Int32Register
{
    public Int16Register Low { get; }
    public Int16Register High { get; }

    public Int32 Value
    {
        get => Int32Utils.Concatenate(High.Value, Low.Value);
        set
        {
            var (high, low) = value;
            High.Value = high;
            Low.Value = low;
        }
    }

    public Int32Register()
    {
        Low = new Int16Register();
        High = new Int16Register();
    }

    public Int32Register(Int32 value) : this()
    {
        Value = value;
    }

    public void Deconstruct(out Int16Register high, out Int16Register low)
    {
        high = new Int16Register((short)(Value >> 16));
        low = new Int16Register((short)Value);
    }
}
