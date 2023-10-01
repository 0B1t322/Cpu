using CPU.Emulator.Commands.Serializer.Extensions;

namespace CPU.Emulator.Commands.Serializer;

public class ArgumentsSerializerOptions
{
    public readonly IReadOnlyDictionary<ArgumentType, ArgumentSerializer>
        ArgumentSerializers;

    public ArgumentsSerializerOptions(
        IEnumerable<Tuple<ArgumentType, ArgumentSerializer>>?
            argumentsSerializers = null)
    {
        argumentsSerializers = argumentsSerializers ?? DefaultSerializers();

        ArgumentSerializers =
            new Dictionary<ArgumentType, ArgumentSerializer>(
                argumentsSerializers.ToPairs());
    }

    private static IEnumerable<Tuple<ArgumentType, ArgumentSerializer>>
        DefaultSerializers()
    {
        return new List<Tuple<ArgumentType, ArgumentSerializer>>(
            new[]
            {
                new Tuple<ArgumentType, ArgumentSerializer>(
                    ArgumentType.RegisterAddress,
                    new RegisterArgumentSerializer()),
                new Tuple<ArgumentType, ArgumentSerializer>(
                    ArgumentType.Literal, new LiteralSerializer())
            });
    }
}
