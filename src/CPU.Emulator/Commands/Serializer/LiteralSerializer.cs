namespace CPU.Emulator.Commands.Serializer;

public class LiteralSerializer : ArgumentSerializer
{
    public override string SerializeArgument(int value)
    {
        return $"0x{Convert.ToString(value, 16)}";
    }

    public override int DeserializeArgument(string value)
    {
        return Convert.ToInt32(value, 16);
    }
}
