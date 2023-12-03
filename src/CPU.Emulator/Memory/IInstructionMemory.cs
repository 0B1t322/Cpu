namespace CPU.Emulator.Memory;

public interface IInstructionMemory
{
    public int Get(uint addr);
    public int this[uint addr] { get; }
    public string ToString<TKOutType, TVOutType>(
        Func<uint, TKOutType> keyMapper, Func<int, TVOutType> valueMapper);
}
