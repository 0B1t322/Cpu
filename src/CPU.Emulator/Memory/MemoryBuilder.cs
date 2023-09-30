using System.Collections;

namespace CPU.Emulator.Memory;

public class MemoryBuilder : IInstructionMemoryBuilder, IDataMemoryBuilder
{
    private readonly Dictionary<uint, int> _pairs = new();

    private MemoryBuilder Add(KeyValuePair<uint, int> pair)
    {
        _pairs[pair.Key] = pair.Value;
        return this;
    }

    private MemoryBuilder Add(IEnumerable<KeyValuePair<uint, int>> pairs)
    {
        foreach (var pair in pairs)
        {
            Add(pair);
        }

        return this;
    }

    private MemoryBuilder Add(uint count)
    {
        for (uint i = 0; i < count; i++)
        {
            _pairs[i] = 0;
        }

        return this;
    }

    IDataMemoryBuilder IDataMemoryBuilder.Add(uint count) => Add(count);

    IInstructionMemoryBuilder IInstructionMemoryBuilder.Add(uint count) =>
        Add(count);

    IInstructionMemoryBuilder IInstructionMemoryBuilder.Add(
        KeyValuePair<uint, int> pair) => this.Add(pair);


    IInstructionMemoryBuilder IInstructionMemoryBuilder.Add(
        IEnumerable<KeyValuePair<uint, int>> pairs) => this.Add(pairs);

    IDataMemoryBuilder IDataMemoryBuilder.Add(
        IEnumerable<KeyValuePair<uint, int>> pairs) => this.Add(pairs);

    IDataMemoryBuilder IDataMemoryBuilder.Add(KeyValuePair<uint, int> pair) =>
        this.Add(pair);

    IInstructionMemory IInstructionMemoryBuilder.Build() => new Memory(_pairs);

    IDataMemory IDataMemoryBuilder.Build() => new Memory(_pairs);
}
