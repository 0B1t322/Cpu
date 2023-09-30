namespace CPU.Emulator.Memory;

public interface IInstructionMemory
{
    public int Get(uint addr);
    public int this[uint addr] { get; }
}
