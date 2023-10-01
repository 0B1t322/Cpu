namespace CPU.Emulator.Commands.Serializer;

public class RegisterArgumentSerializer : ArgumentSerializer
{
    private readonly RegisterArgumentsSerializerOptions _options;

    public RegisterArgumentSerializer(
        RegisterArgumentsSerializerOptions? options = null)
    {
        _options = options ?? new RegisterArgumentsSerializerOptions();
    }

    public override string SerializeArgument(int value)
    {
        return _options.ValueToRegisterMappings[value];
    }

    public override int DeserializeArgument(string value)
    {
        return _options.RegisterToValueMappings[value];
    }
}
