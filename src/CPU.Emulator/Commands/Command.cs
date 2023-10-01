namespace CPU.Emulator.Commands;

public struct Command
{
    public Instruction Instruction;
    public ushort RegisterAddress;
    public short Literal;

    public Command(Instruction instruction, ushort registerAddress = 0,
        short literal = 0)
    {
        Instruction = instruction;
        RegisterAddress = registerAddress;
        Literal = literal;
    }

    private const int CommandLen = 32;

    private const int InstructionBitsLen = 8;

    private const int InstructionMask = ((0b1 << InstructionBitsLen) - 1) <<
                                        (CommandLen - InstructionBitsLen);

    private const int LiteralBitsLen = 16;

    private const int LiteralMask = ((0b1 << LiteralBitsLen) - 1) <<
                                    (CommandLen - (InstructionBitsLen +
                                                   LiteralBitsLen));

    private const int AddressBitsLen = 8;

    private const int AddressMask = ((0b1 << AddressBitsLen) - 1) <<
                                    (CommandLen - (InstructionBitsLen +
                                                   LiteralBitsLen +
                                                   AddressBitsLen));

    public Command(int command)
    {
        var instruction = (command & InstructionMask) >>
                          (CommandLen - InstructionBitsLen);

        var literal = (command & LiteralMask) >>
                      (CommandLen - (InstructionBitsLen + LiteralBitsLen));

        var address = (command & AddressMask);

        this.Instruction = (Instruction)instruction;
        this.Literal = (short)literal;
        this.RegisterAddress = (ushort)address;
    }

    public int ToInt()
    {
        var cmd = (
                      (int)(Instruction) << (CommandLen - InstructionBitsLen)) |
                  (Literal <<
                   (CommandLen - (InstructionBitsLen + LiteralBitsLen))) |
                  RegisterAddress;

        return cmd;
    }

    public override string ToString()
    {
        return
            $"Instruction={this.Instruction} RegisterAddress={this.RegisterAddress} Literal={this.Literal}";
    }
}
