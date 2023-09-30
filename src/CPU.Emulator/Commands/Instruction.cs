namespace CPU.Emulator.Commands;

public enum Instruction
{
    /// <summary>
    /// Empty command. Mean do nothing
    /// </summary>
    Empty,

    /// <summary>
    /// Mean stop a CPU.
    /// </summary>
    Stop,

    /// <summary>
    /// Add to accumulator provided register
    /// </summary>
    Add,

    /// <summary>
    /// Sub from accumulator provided register
    /// </summary>
    Sub,

    /// <summary>
    /// Increment provided register
    /// </summary>
    Inc,

    /// <summary>
    /// Decrement provided register
    /// </summary>
    Dec,

    /// <summary>
    /// Set literal to provided low 16 bits of register
    /// </summary>
    SetLow,

    /// <summary>
    /// Set literal to provided high 16 bits of register
    /// </summary>
    SetHigh,

    /// <summary>
    /// Move to memory value in A register by address in provided register
    /// </summary>
    MoveToMemory,

    /// <summary>
    /// Move value from memory with address in provided register to A register
    /// </summary>
    LoadFromMemory,

    /// <summary>
    /// Move value in accumulator to provided register
    /// </summary>
    MoveFromAccumulator,

    /// <summary>
    /// Load value from provided register to accumulator
    /// </summary>
    LoadToAccumulator,

    /// <summary>
    /// Set program counter to literal value if GZ flag is true
    /// </summary>
    JumpIfGZ,
    
    /// <summary>
    /// Set program counter to literal value if EZ flag is true
    /// </summary>
    JumpIfEZ,
    
    /// <summary>
    /// Set program counter to literal value if LZ flag is true
    /// </summary>
    JumpIfLZ,
    
    /// <summary>
    /// Set program counter to literal
    /// </summary>
    Jump,
}
