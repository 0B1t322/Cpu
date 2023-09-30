namespace CPU.Emulator.Memory;

public interface IInstructionMemoryBuilder
{
    IInstructionMemoryBuilder Add(KeyValuePair<uint, int> pair);
    IInstructionMemoryBuilder Add(IEnumerable<KeyValuePair<uint, int>> pairs);
    IInstructionMemoryBuilder Add(uint count);
    IInstructionMemory Build();

    static IInstructionMemoryBuilder Builder => new MemoryBuilder();
}
