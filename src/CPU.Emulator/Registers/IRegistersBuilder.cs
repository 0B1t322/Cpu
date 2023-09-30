namespace CPU.Emulator.Registers;

public interface IRegistersBuilder
{
    IRegistersBuilder Set(KeyValuePair<ushort, int> pair);
    IRegistersBuilder Set(IEnumerable<KeyValuePair<ushort, int>> pairs);
    IRegistersBuilder Set(ushort addr, int value);
    IRegisters Build();

    static IRegistersBuilder Builder(ushort maxRegisters = 32) =>
        new RegisterBuilder(maxRegisters);
}
