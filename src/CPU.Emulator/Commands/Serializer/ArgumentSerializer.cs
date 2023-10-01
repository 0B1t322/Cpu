namespace CPU.Emulator.Commands.Serializer;

public abstract class ArgumentSerializer
{
    public abstract string SerializeArgument(int value);

    public abstract int DeserializeArgument(string value);
}
