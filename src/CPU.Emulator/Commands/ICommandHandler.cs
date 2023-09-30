using System.Reflection.Metadata;

namespace CPU.Emulator.Commands;

public interface ICommandHandler
{
    void Handle(Command command);
}
