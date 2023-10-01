namespace CPU.Emulator.Commands.Serializer.Extensions;

public static class ArgumentsExtensions
{
    public static Command ToCommand(this IEnumerable<Argument> arguments,
        Instruction instruction)
    {
        var cmd = new Command(instruction);

        foreach (var argument in arguments)
        {
            switch (argument.Type)
            {
                case ArgumentType.Literal:
                    cmd.Literal = (short)argument.Value;
                    break;
                case ArgumentType.RegisterAddress:
                    cmd.RegisterAddress = (ushort)argument.Value;
                    break;
            }
        }

        return cmd;
    }

    public static Command ToCommand(this Argument[] arguments,
        Instruction instruction)
    {
        return ToCommand(arguments.AsEnumerable(), instruction);
    }
}
