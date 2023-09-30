using CPU.Emulator.Memory;
using CPU.Emulator.Registers;

namespace CPU.Emulator;

public class CpuBuilder : ICpuBuilder
{
    private IInstructionMemory _instructionMemory;
    private IDataMemory _dataMemory;
    private IRegisters _registers;
    private readonly ushort maxRegister;

    public CpuBuilder(ushort maxRegister)
    {
        this.maxRegister = maxRegister;
    }

    public ICpuBuilder SetInstructionMemory(IInstructionMemory memory)
    {
        _instructionMemory = memory;
        return this;
    }

    public ICpuBuilder SetInstructionMemory(
        IInstructionMemoryBuilder memoryBuilder) =>
        SetInstructionMemory(memoryBuilder.Build());

    public ICpuBuilder SetInstructionMemory(
        Func<IInstructionMemoryBuilder, IInstructionMemoryBuilder>
            instructionMemoryBuilder) =>
        SetInstructionMemory(
            instructionMemoryBuilder(IInstructionMemoryBuilder.Builder));

    public ICpuBuilder SetInstructionMemory(
        Func<IInstructionMemoryBuilder, IInstructionMemory>
            instructionMemoryBuilder) =>
        SetInstructionMemory(
            instructionMemoryBuilder(IInstructionMemoryBuilder.Builder));

    public ICpuBuilder SetInstructionMemory(
        Action<IInstructionMemoryBuilder> instructionMemoryBuilder)
    {
        var b = IInstructionMemoryBuilder.Builder;
        instructionMemoryBuilder(b);
        return SetInstructionMemory(b);
    }

    public ICpuBuilder SetDataMemory(IDataMemory memory)
    {
        _dataMemory = memory;
        return this;
    }

    public ICpuBuilder SetDataMemory(IDataMemoryBuilder memoryBuilder) =>
        SetDataMemory(memoryBuilder.Build());

    public ICpuBuilder SetDataMemory(
        Func<IDataMemoryBuilder, IDataMemoryBuilder> dataMemoryBuilder) =>
        SetDataMemory(dataMemoryBuilder(IDataMemoryBuilder.Builder));

    public ICpuBuilder SetDataMemory(
        Func<IDataMemoryBuilder, IDataMemory> dataMemoryBuilder) =>
        SetDataMemory(dataMemoryBuilder(IDataMemoryBuilder.Builder));

    public ICpuBuilder SetDataMemory(
        Action<IDataMemoryBuilder> dataMemoryBuilder)
    {
        var b = IDataMemoryBuilder.Builder;
        dataMemoryBuilder(b);
        return SetDataMemory(b);
    }

    public ICpuBuilder SetRegisters(IRegisters registers)
    {
        _registers = registers;

        return this;
    }

    public ICpuBuilder SetRegisters(IRegistersBuilder registersBuilder) =>
        SetRegisters(registersBuilder.Build());

    public ICpuBuilder SetRegisters(
        Func<IRegistersBuilder, IRegistersBuilder> registerBuilder) =>
        SetRegisters(registerBuilder(IRegistersBuilder.Builder(maxRegister)));

    public ICpuBuilder SetRegisters(
        Func<IRegistersBuilder, IRegisters> registerBuilder) =>
        SetRegisters(registerBuilder(IRegistersBuilder.Builder(maxRegister)));

    public ICpuBuilder SetRegisters(Action<IRegistersBuilder> registerBuilder)
    {
        var b = IRegistersBuilder.Builder(maxRegister);
        registerBuilder(b);
        return SetRegisters(b);
    }

    public ICpu Build() =>
        new Cpu(_registers ?? IRegistersBuilder.Builder(maxRegister).Build(),
            _instructionMemory ?? IInstructionMemoryBuilder.Builder.Build(),
            _dataMemory ?? IDataMemoryBuilder.Builder.Build());
}
