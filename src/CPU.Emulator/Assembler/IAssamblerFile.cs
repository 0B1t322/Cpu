using CPU.Emulator.Commands;

namespace CPU.Emulator.Assembler;

public class IAssamblerFile
{
    public IList<Command> Commands { get; }
    public IList<uint> StaticData { get; }
}
