namespace CPU.Emulator.Registers;

public interface IRegisters
{
    public Int32Register Get(ushort addr);
    public Int16Register GetLow(ushort addr);
    public Int16Register GetHigh(ushort addr);
    public Int32Register this[ushort addr] { get; }
}
