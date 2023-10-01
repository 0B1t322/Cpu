using CPU.Emulator.Commands.Serializer.Extensions;

namespace CPU.Emulator.Commands.Serializer;

using InstructionToStringMappings = Dictionary<Instruction, string>;
using InstructionNameMapping = Tuple<Instruction, string>;

public sealed class InstructionSerializerOptions
{
    public readonly IReadOnlyDictionary<Instruction, string>
        InstructionToStringMappings;

    public readonly IReadOnlyDictionary<string, Instruction>
        StringToInstructionMappings;

    public InstructionSerializerOptions(
        IEnumerable<InstructionNameMapping>? instructionMappings = null)
    {
        instructionMappings =
            instructionMappings ?? GetDefaultInstructionMappings();
        InstructionToStringMappings =
            new InstructionToStringMappings(instructionMappings.ToPairs())
                .AsReadOnly();

        StringToInstructionMappings =
            InstructionToStringMappings
                .ToDictionary(x => x.Value, x => x.Key)
                .AsReadOnly();
    }

    private IEnumerable<InstructionNameMapping> GetDefaultInstructionMappings()
    {
        return new List<InstructionNameMapping>(
            new[]
            {
                Tuple.Create(Instruction.Empty, "nop"),
                Tuple.Create(Instruction.Stop, "stop"),
                Tuple.Create(Instruction.Add, "add"),
                Tuple.Create(Instruction.Sub, "sub"),
                Tuple.Create(Instruction.Inc, "inc"),
                Tuple.Create(Instruction.Dec, "dec"),
                Tuple.Create(Instruction.SetLow, "sl"),
                Tuple.Create(Instruction.SetHigh, "sh"),
                Tuple.Create(Instruction.MoveToMemory, "mm"),
                Tuple.Create(Instruction.LoadFromMemory, "lm"),
                Tuple.Create(Instruction.MoveFromAccumulator, "ma"),
                Tuple.Create(Instruction.LoadToAccumulator, "la"),
                Tuple.Create(Instruction.JumpIfGZ, "jgz"),
                Tuple.Create(Instruction.JumpIfEZ, "jez"),
                Tuple.Create(Instruction.JumpIfLZ, "jlz"),
                Tuple.Create(Instruction.Jump, "jmp"),
            });
    }
}
