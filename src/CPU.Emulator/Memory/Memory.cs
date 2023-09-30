namespace CPU.Emulator.Memory;

public class Memory : IDataMemory, IInstructionMemory
{
    private readonly Dictionary<uint, int> _memory;

    public Memory()
    {
        _memory = new Dictionary<uint, int>();
    }

    public Memory(IEnumerable<KeyValuePair<uint, int>> data)
    {
        _memory = new Dictionary<uint, int>(data);
    }

    public int Get(uint addr)
    {
        return _memory[addr];
    }

    public void Set(uint addr, int value)
    {
        _memory[addr] = value;
    }

    public int this[uint addr]
    {
        get => Get(addr);
        set => Set(addr, value);
    }

    public override string? ToString()
    {
        return string.Join("\n",
            _memory.Select(kvp => $"{kvp.Key}\t{kvp.Value}"));
    }
}
