namespace CPU.Emulator.Commands.Serializer;

public class ArgumentsSerializer
{
    private readonly ArgumentsSerializerOptions _options;

    private readonly ArgumentSerializer defaultSerializer =
        new DefaultArgumentSerializer();

    public ArgumentsSerializer(ArgumentsSerializerOptions? options = null)
    {
        _options = options ?? new ArgumentsSerializerOptions();
    }

    public string SerializeArgument(ArgumentType type, int value)
    {
        return GetSerializer(type).SerializeArgument(value);
    }

    public int DeserializeArgument(ArgumentType type, string value)
    {
        return GetSerializer(type).DeserializeArgument(value);
    }

    private ArgumentSerializer GetSerializer(ArgumentType type) =>
        _options.ArgumentSerializers[type] ?? defaultSerializer;
}
