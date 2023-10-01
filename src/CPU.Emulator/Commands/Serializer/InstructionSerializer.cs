namespace CPU.Emulator.Commands.Serializer;

public class InstructionSerializer
{
    private readonly InstructionSerializerOptions _options;

    public InstructionSerializer(InstructionSerializerOptions? options = null)
    {
        this._options = options ?? new InstructionSerializerOptions();
    }

    public string SerializeInstruction(Instruction instruction)
    {
        return _options.InstructionToStringMappings[instruction];
    }

    public Instruction DeserializeInstruction(string instruction)
    {
        return _options.StringToInstructionMappings[instruction];
    }
}
