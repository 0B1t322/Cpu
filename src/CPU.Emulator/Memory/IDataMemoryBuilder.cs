namespace CPU.Emulator.Memory;

public interface IDataMemoryBuilder
{
    IDataMemoryBuilder Add(KeyValuePair<uint, int> pair);
    IDataMemoryBuilder Add(IEnumerable<KeyValuePair<uint, int>> pairs);
    IDataMemoryBuilder Add(uint count);
    IDataMemory Build();

    static IDataMemoryBuilder Builder => new MemoryBuilder();
}
