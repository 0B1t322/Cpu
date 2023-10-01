namespace CPU.Emulator.Commands.Serializer;

public class DefaultArgumentSerializer : ArgumentSerializer
{
    public override string SerializeArgument(int value)
    {
        return $"{value}";
    }

    public override int DeserializeArgument(string value)
    {
        return int.Parse(value);
    }
}
