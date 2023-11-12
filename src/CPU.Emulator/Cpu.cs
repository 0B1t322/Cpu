using System.Text;
using CPU.Emulator.Commands;
using CPU.Emulator.Memory;
using CPU.Emulator.Registers;

namespace CPU.Emulator;

public class Cpu : ICpu
{
    private readonly IRegisters _registers;
    private readonly IInstructionMemory _instructionMemory;
    private readonly IDataMemory _dataMemory;
    private readonly Queue<Action> _tasks = new Queue<Action>();

    private readonly FlagRegister _flagRegister;
    private uint _programmCounter = 0;

    private bool _stoped = false;

    public Cpu(IRegisters registers, IInstructionMemory instructionMemory,
        IDataMemory dataMemory)
    {
        _registers = registers;
        _instructionMemory = instructionMemory;
        _dataMemory = dataMemory;

        _flagRegister =
            new FlagRegister(_registers[RegistersAddress.Flags.Address()]);
    }

    private void LaunchTasks()
    {
        foreach (var task in _tasks)
        {
            task();
        }

        _tasks.Clear();
    }

    private void Add(Int16Register a, Int16Register b)
    {
        short result = (short)(a.Value + b.Value);
        CheckOverflow(a.Value, b.Value, result);
        CheckResult(result);

        a.Value = result;
    }

    private void Add(Int32Register a, Int16Register b)
    {
        int result = (a.Value + b.Value);
        CheckOverflow(a.Value, b.Value, result);
        CheckResult(result);

        a.Value = result;
    }

    private void Add(Int32Register a, Int32Register b)
    {
        int result = (a.Value + b.Value);
        CheckOverflow(a.Value, b.Value, result);
        CheckResult(result);

        a.Value = result;
    }

    private void Add(Int32Register a, int b)
    {
        int result = (a.Value + b);
        CheckOverflow(a.Value, b, result);
        CheckResult(result);

        a.Value = result;
    }

    private void LoadLow(Int32Register a, short b)
    {
        a.Low.Value = b;
    }

    private void LoadHigh(Int32Register a, short b)
    {
        a.High.Value = b;
    }

    private void MoveToMemory(Int32Register valueRegister,
        Int32Register addressRegister)
    {
        _dataMemory.Set((uint)addressRegister.Value, valueRegister.Value);
    }

    private void LoadFromMemory(Int32Register valueRegister,
        Int32Register addressRegister)
    {
        var value = _dataMemory.Get((uint)addressRegister.Value);
        valueRegister.Value = value;
    }

    private void MoveToRegister(Int32Register valueRegister,
        Int32Register destRegister)
    {
        destRegister.Value = valueRegister.Value;
    }

    private void CheckOverflow(IComparable a, IComparable b, IComparable result)
    {
        if ((a.CompareTo(0) > 0 && b.CompareTo(0) > 0 &&
             result.CompareTo(0) < 0) ||
            (a.CompareTo(0) < 0 && b.CompareTo(0) < 0 &&
             result.CompareTo(0) > 0))
        {
            SetCF(true);
        }
        else
            SetCF(false);
    }

    private void Jump(uint to)
    {
        _programmCounter = to;
    }

    private void Jump(short to)
    {
        _programmCounter = (uint)(to-1);
    }

    private void CheckResult(IComparable result)
    {
        switch (result.CompareTo(0))
        {
            case 1:
                SetGZ();
                break;
            case 0:
                SetEZ();
                break;
            case -1:
                SetLZ();
                break;
        }
    }

    private void SetCF(bool value)
    {
        _flagRegister.CF = value;
        // _tasks.Enqueue(() => _flagRegister.CF = false);
    }

    private void SetGZ()
    {
        _flagRegister.GZ = true;
        _flagRegister.EZ = false;
        _flagRegister.LZ = false;
        // _tasks.Enqueue(() => _flagRegister.GZ = false);
    }

    private void SetEZ()
    {
        _flagRegister.GZ = false;
        _flagRegister.EZ = true;
        _flagRegister.LZ = false;
        // _tasks.Enqueue(() => _flagRegister.EZ = false);
    }

    private void SetLZ()
    {
        _flagRegister.GZ = false;
        _flagRegister.EZ = false;
        _flagRegister.LZ = true;
        // _tasks.Enqueue(() => _flagRegister.LZ = false);
    }

    public void Sub(Int16Register a, Int16Register b)
    {
        var result = (short)(a.Value - b.Value);
        CheckOverflow(a.Value, (short)(0 - b.Value), result);
        CheckResult(result);
        a.Value = (short)result;
    }

    public void Sub(Int32Register a, Int16Register b)
    {
        int result = a.Value - b.Value;
        CheckOverflow(a.Value, (short)(0 - b.Value), result);
        CheckResult(result);
        a.Value = (short)result;
    }

    public void Sub(Int32Register a, Int32Register b)
    {
        int result = a.Value - b.Value;
        CheckOverflow(a.Value, -b.Value, result);
        CheckResult(result);
        a.Value = (short)result;
    }

    public Command ReadCommand()
    {
        var cmd = _instructionMemory.Get(_programmCounter);
        return new Command(cmd);
    }

    public void ExecuteCommand(Command cmd)
    {
        switch (cmd.Instruction)
        {
            case Instruction.Add:
            {
                var a = _registers.Get(RegistersAddress.A.Address());
                var b = _registers.Get(cmd.RegisterAddress);
                Add(a, b);
                return;
            }
            case Instruction.Empty:
            {
                break;
            }
            case Instruction.Sub:
            {
                var a = _registers.Get(RegistersAddress.A.Address());
                var b = _registers.Get(cmd.RegisterAddress);
                Sub(a, b);
                break;
            }
            case Instruction.Inc:
            {
                var a = _registers.Get(cmd.RegisterAddress);
                Add(a, 1);
                break;
            }
            case Instruction.Dec:
            {
                var a = _registers.Get(cmd.RegisterAddress);
                Add(a, -1);
                break;
            }
            case Instruction.SetLow:
            {
                var a = _registers.Get(cmd.RegisterAddress);
                LoadLow(a, cmd.Literal);
                break;
            }
            case Instruction.SetHigh:
            {
                var a = _registers.Get(cmd.RegisterAddress);
                LoadHigh(a, cmd.Literal);
                break;
            }
            case Instruction.MoveToMemory:
            {
                var a = _registers.Get(RegistersAddress.A.Address());
                var b = _registers.Get(cmd.RegisterAddress);
                MoveToMemory(a, b);
                break;
            }
            case Instruction.LoadFromMemory:
            {
                var a = _registers.Get(RegistersAddress.A.Address());
                var b = _registers.Get(cmd.RegisterAddress);
                LoadFromMemory(a, b);
                break;
            }
            case Instruction.LoadToAccumulator:
            {
                var a = _registers.Get(RegistersAddress.A.Address());
                var b = _registers.Get(cmd.RegisterAddress);
                MoveToRegister(b, a);
                break;
            }
            case Instruction.MoveFromAccumulator:
            {
                var a = _registers.Get(RegistersAddress.A.Address());
                var b = _registers.Get(cmd.RegisterAddress);
                MoveToRegister(a, b);
                break;
            }
            case Instruction.Jump:
            {
                Jump(cmd.Literal);
                break;
            }
            case Instruction.JumpIfEZ:
            {
                if (_flagRegister.EZ)
                    Jump(cmd.Literal);
                break;
            }
            case Instruction.JumpIfGZ:
            {
                if (_flagRegister.GZ)
                    Jump(cmd.Literal);
                break;
            }
            case Instruction.JumpIfLZ:
            {
                if (_flagRegister.LZ)
                    Jump(cmd.Literal);
                break;
            }
            case Instruction.Stop:
            {
                _stoped = true;
                break;
            }
        }

        return;
    }

    public void Start(Action<ICpuInfo>? afterStep)
    {
        while (!_stoped)
        {
            LaunchTasks();

            var cmd = ReadCommand();
            ExecuteCommand(cmd);
            if (afterStep is not null) afterStep(this);
            _programmCounter+=1;
        }
    }

    public void Start() => Start(null);

    public override string ToString()
    {
        return Stats();
    }

    public string Stats()
    {
        var b = new StringBuilder();
        b.Append($"ProgramCounter: {_programmCounter}\n");
        b.Append("InstructionMemory\n");
        b.Append("-------------------\n");
        b.Append(_instructionMemory.ToString());
        b.Append("\n");
        b.Append("DataMemory\n");
        b.Append("-------------------\n");
        b.Append(_dataMemory.ToString());
        b.Append("\n");
        b.Append("Registers\n");
        b.Append("-------------------\n");
        b.Append(_registers.ToString((u) => (RegistersAddress)u));
        b.Append("\n");

        return b.ToString();
    }
}
