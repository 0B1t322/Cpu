namespace CPU.Emulator.Registers;

public class RegisterBuilder : IRegistersBuilder
{
    private readonly IRegisters _registers;

    public RegisterBuilder(ushort maxRegisters = 32)
    {
        _registers = new Registers(maxRegisters);
    }

    public IRegistersBuilder Set(KeyValuePair<ushort, int> pair)
    {
        _registers[pair.Key].Value = pair.Value;

        return this;
    }

    public IRegistersBuilder Set(IEnumerable<KeyValuePair<ushort, int>> pairs)
    {
        foreach (var pair in pairs)
        {
            _registers[pair.Key].Value = pair.Value;
        }

        return this;
    }

    public IRegistersBuilder Set(ushort addr, int value) =>
        Set(new KeyValuePair<ushort, int>(addr, value));

    public IRegisters Build()
    {
        return _registers;
    }
}
