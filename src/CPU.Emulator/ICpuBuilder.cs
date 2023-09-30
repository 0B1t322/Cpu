using CPU.Emulator.Memory;
using CPU.Emulator.Registers;

namespace CPU.Emulator;

public interface ICpuBuilder
{
    ICpuBuilder SetInstructionMemory(IInstructionMemory memory);

    ICpuBuilder SetInstructionMemory(
        Func<IInstructionMemoryBuilder, IInstructionMemoryBuilder>
            instructionMemoryBuilder);

    ICpuBuilder SetInstructionMemory(
        Func<IInstructionMemoryBuilder, IInstructionMemory>
            instructionMemoryBuilder);

    ICpuBuilder SetInstructionMemory(
        Action<IInstructionMemoryBuilder> instructionMemoryBuilder);

    ICpuBuilder SetDataMemory(IDataMemory memory);

    ICpuBuilder SetDataMemory(
        Func<IDataMemoryBuilder, IDataMemoryBuilder> dataMemoryBuilder);

    ICpuBuilder SetDataMemory(
        Func<IDataMemoryBuilder, IDataMemory> dataMemoryBuilder);

    ICpuBuilder SetDataMemory(
        Action<IDataMemoryBuilder> dataMemoryBuilder);

    ICpuBuilder SetRegisters(IRegisters registers);

    ICpuBuilder SetRegisters(
        Func<IRegistersBuilder, IRegistersBuilder> registerBuilder);

    ICpuBuilder SetRegisters(
        Func<IRegistersBuilder, IRegisters> registerBuilder);

    ICpuBuilder SetRegisters(
        Action<IRegistersBuilder> registerBuilder);

    ICpu Build();

    static ICpuBuilder Builder(ushort maxRegisters = 32) =>
        new CpuBuilder(maxRegisters);
}
