using CPU.Emulator.Commands.Serializer.Extensions;

namespace CPU.Emulator.Commands.Serializer;

public class RegisterArgumentsSerializerOptions
{
    public readonly IReadOnlyDictionary<int, string> ValueToRegisterMappings;
    public readonly IReadOnlyDictionary<string, int> RegisterToValueMappings;

    public RegisterArgumentsSerializerOptions(
        IEnumerable<Tuple<int, string>>? mappings = null)
    {
        mappings = mappings ?? DefaultMappings();

        ValueToRegisterMappings =
            new Dictionary<int, string>(mappings.ToPairs())
                .AsReadOnly();

        RegisterToValueMappings =
            ValueToRegisterMappings
                .ToDictionary(x => x.Value, x => x.Key)
                .AsReadOnly();
    }

    private static IEnumerable<Tuple<int, string>> DefaultMappings()
    {
        var result = new List<Tuple<int, string>>();
        for (int i = 0; i < 25; i++)
        {
            char ch = (char)(97 + i);
            result.Add(Tuple.Create(i + 1, ch.ToString()));
        }

        return result;
    }
}
