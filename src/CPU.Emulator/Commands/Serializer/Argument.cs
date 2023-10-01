namespace CPU.Emulator.Commands.Serializer;

public struct Argument
{
    public ArgumentType Type;
    public int Value;

    public Argument(ArgumentType type, int value)
    {
        Type = type;
        Value = value;
    }

    public static Argument[]? CreateFromCommand(Command command,
        params ArgumentType[]? types)
    {
        if (types is null || types.Length == 0)
            return null;

        var arguments = new List<Argument>();

        foreach (var type in types)
        {
            switch (type)
            {
                case ArgumentType.Literal:
                    arguments.Add(new Argument(type, command.Literal));
                    break;
                case ArgumentType.RegisterAddress:
                    arguments.Add(new Argument(type, command.RegisterAddress));
                    break;
            }
        }

        return arguments.ToArray();
    }
}
