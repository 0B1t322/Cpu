namespace CPU.Emulator.Memory;

public interface IDataMemory
{
    public int Get(uint addr);
    public void Set(uint addr, int value);

    public int this[uint addr] { get; set; }

    public string ToString<TKOutType, TVOutType>(
        Func<uint, TKOutType> keyMapper, Func<int, TVOutType> valueMapper);
}
