using CPU.Emulator.Registers.Exceptions;

namespace CPU.Emulator.Registers;

public class Registers : IRegisters
{
    private readonly ushort _maxRegisters;
    private readonly Dictionary<ushort, Int32Register> _registers;

    public Registers(ushort maxRegisters = 32)
    {
        _registers = new Dictionary<ushort, Int32Register>();
        _maxRegisters = maxRegisters;

        for (ushort addr = 0; addr < _maxRegisters; addr++)
        {
            _registers[addr] = new Int32Register();
        }
    }

    public Int32Register Get(ushort addr)
    {
        if (addr > MaxAddress)
        {
            throw new RegisterAddrOutOfRange(addr, MaxAddress);
        }

        return _registers[addr];
    }

    public Int16Register GetLow(ushort addr)
    {
        return Get(addr).Low;
    }

    public Int16Register GetHigh(ushort addr)
    {
        return Get(addr).High;
    }

    public Int32Register this[ushort addr] => Get(addr);

    public IReadOnlyDictionary<ushort, Int32Register> RegistersTable =>
        _registers.AsReadOnly();


    private ushort MaxAddress => (ushort)(_maxRegisters - 1);

    public override string ToString()
    {
        return string.Join("\n",
            _registers.Select(kvp => $"{kvp.Key}\t{kvp.Value.Value}"));
    }
}
