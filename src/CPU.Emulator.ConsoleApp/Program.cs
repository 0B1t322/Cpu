using CPU.Emulator;
using CPU.Emulator.Commands;
using CPU.Emulator.Utils.Enums;

var data = new List<int>(new[]
{
    9,
    3,
    8,
    10,
    22,
    15,
    7,
    8,
    1,
    5
});

var commands = new List<Command>(new[]
{
    // Load start address of array elements into register D
    // Addr: 0
    new Command(Instruction.SetLow,
        registerAddress: RegistersAddress.D.Address(), literal: 0x0),
    // Addr: 1
    // Load count of number in massive to reg A
    new Command(Instruction.LoadFromMemory,
        registerAddress: RegistersAddress.D.Address()),
    // Addr: 2
    // Move it to reg E
    new Command(Instruction.MoveFromAccumulator, 
        registerAddress: RegistersAddress.E.Address()),
    // Addr: 3
    // Inc addr
    new Command(Instruction.Inc,
        registerAddress: RegistersAddress.D.Address()),
    // Addr: 4
    // Load value from massive
    new Command(Instruction.LoadFromMemory,
        registerAddress: RegistersAddress.D.Address()),
    // Addr: 5
    new Command(Instruction.Sub, RegistersAddress.B.Address()),
    // Addr: 6
    new Command(Instruction.JumpIfLZ, literal: 0x9),
    // Addr: 7
    new Command(Instruction.Add,
        registerAddress: RegistersAddress.B.Address()),
    // Curr maximum store in B register
    // Addr: 8
    new Command(Instruction.MoveFromAccumulator,
        registerAddress: RegistersAddress.B.Address()),
    // Addr: 9
    new Command(Instruction.Inc, RegistersAddress.D.Address()),
    // Addr: 10
    new Command(Instruction.Dec, RegistersAddress.E.Address()),
    // Addr: 11
    new Command(Instruction.JumpIfGZ, literal: 0x4),
    // Addr: 12
    new Command(Instruction.Stop)
});


var cpu = ICpuBuilder
    .Builder((ushort)EnumUtils.GetValues<RegistersAddress>().Count())
    .SetDataMemory(b =>
        b
            .Add((uint)data.Count)
            .Add(data.Select((v, index) =>
                new KeyValuePair<uint, int>((uint)(index), v))))
    .SetInstructionMemory(b => b
        .Add((uint)commands.Count)
        .Add(commands.Select(
                (cmd, index) =>
                    new KeyValuePair<uint, int>((uint)index, cmd.ToInt()))
            .ToList()))
    .Build();

cpu.Start(info => Console.WriteLine(info.Stats()));
