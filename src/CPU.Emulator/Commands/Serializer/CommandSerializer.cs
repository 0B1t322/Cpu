using System.Text;
using CPU.Emulator.Commands.Serializer.Extensions;

namespace CPU.Emulator.Commands.Serializer;

public class CommandSerializer
{
    private readonly InstructionSerializer _instructionSerializer;
    private readonly ArgumentsSerializer _argumentsSerializer;

    public CommandSerializer(
        InstructionSerializerOptions? instructionSerializerOptions = null,
        ArgumentsSerializerOptions? argumentsSerializerOptions = null)
    {
        _instructionSerializer =
            new InstructionSerializer(instructionSerializerOptions);

        _argumentsSerializer =
            new ArgumentsSerializer(argumentsSerializerOptions);
    }

    public string SerializeCommand(Command command)
    {
        var argumentsTypes = GetCommandsArgumentsTypes(command.Instruction);
        var arguments = Argument.CreateFromCommand(command, argumentsTypes);

        var b = new StringBuilder();
        b.Append(
            _instructionSerializer.SerializeInstruction(command.Instruction));

        if (arguments is null || arguments.Length == 0)
        {
            return b.ToString();
        }

        b.Append(" ");
        for (int i = 0; i < arguments.Length; i++)
        {
            b.Append(_argumentsSerializer.SerializeArgument(arguments[i].Type,
                arguments[i].Value));
            if (i + 1 < arguments.Length)
                b.Append(", ");
        }

        return b.ToString();
    }

    public Command DeserializeCommand(string command)
    {
        command = command.ToLower();
        var splited = command.Split(" ", 2);
        var rawInstruction = splited[0];

        var instruction =
            _instructionSerializer.DeserializeInstruction(rawInstruction);

        var argumentsTypes = GetCommandsArgumentsTypes(instruction);
        if (argumentsTypes is null || argumentsTypes.Length == 0)
        {
            return new Command(instruction);
        }

        var commandArguments = splited[1].Split(",")
            .Select(s => s.Trim().Replace(" ", ""))
            .ToList();

        var arguments = new List<Argument>();
        for (int i = 0; i < argumentsTypes.Length; i++)
        {
            var type = argumentsTypes[i];
            var argument = commandArguments[i];

            arguments.Add(new Argument(type,
                _argumentsSerializer.DeserializeArgument(type, argument)));
        }

        return arguments.ToCommand(instruction);
    }

    /// <summary>
    /// Return array of argument types
    /// <br/>
    /// If array is null mean command don't have arguments
    /// <br/>
    /// If command have a few arguments they store in command in order like in array
    /// </summary>
    /// <param name="instruction"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentOutOfRangeException"></exception>
    private ArgumentType[]? GetCommandsArgumentsTypes(Instruction instruction)
    {
        return instruction switch
        {
            // Empty commands
            Instruction.Empty
                or Instruction.Stop => CreateArgumentsTypes(),
            // Commands with only register address
            Instruction.Add
                or Instruction.Dec
                or Instruction.Sub
                or Instruction.Inc
                or Instruction.MoveToMemory
                or Instruction.MoveFromAccumulator
                or Instruction.LoadToAccumulator
                or Instruction.LoadFromMemory =>
                CreateArgumentsTypes(ArgumentType.RegisterAddress),
            // Command with only literal
            Instruction.Jump
                or Instruction.JumpIfEZ
                or Instruction.JumpIfGZ
                or Instruction.JumpIfLZ =>
                CreateArgumentsTypes(ArgumentType.Literal),
            // Commands with liter and register address
            Instruction.SetLow
                or Instruction.SetHigh => CreateArgumentsTypes(
                    ArgumentType.Literal, ArgumentType.RegisterAddress),
            _ => throw new ArgumentOutOfRangeException(),
        };
    }

    private static ArgumentType[]? CreateArgumentsTypes(
        params ArgumentType[] types)
    {
        if (types.Length == 0) return null;

        return types;
    }
}
