using CPU.Emulator;
using CPU.Emulator.Commands;
using CPU.Emulator.Utils.Enums;

var data = new List<int>(new[]
{
    150,
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
    // Load to register E counts of elements
    // Addr: 1
    new Command(Instruction.SetLow,
        registerAddress: RegistersAddress.E.Address(),
        literal: (short)data.Count()),
    // Addr: 2
    new Command(Instruction.LoadFromMemory,
        registerAddress: RegistersAddress.D.Address()),
    // Addr: 3
    new Command(Instruction.Sub, RegistersAddress.B.Address()),
    // Addr: 4
    new Command(Instruction.JumpIfLZ, literal: 0x7),
    // Addr: 5
    new Command(Instruction.Add,
        registerAddress: RegistersAddress.B.Address()),
    // Curr maximum store in B register
    // Addr: 6
    new Command(Instruction.MoveFromAccumulator,
        registerAddress: RegistersAddress.B.Address()),
    // Addr: 7
    new Command(Instruction.Inc, RegistersAddress.D.Address()),
    // Addr: 8
    new Command(Instruction.Dec, RegistersAddress.E.Address()),
    // Addr: 9
    new Command(Instruction.JumpIfGZ, literal: 0x2),
    // Addr: 10
    new Command(Instruction.Stop)
});


var cpu = ICpuBuilder
    .Builder((ushort)EnumUtils.GetValues<RegistersAddress>().Count())
    .SetDataMemory(b =>
        b
            .Add(32)
            .Add(data.Select((v, index) =>
                new KeyValuePair<uint, int>((uint)(index), v))))
    .SetInstructionMemory(b => b
        .Add(32)
        .Add(commands.Select(
                (cmd, index) =>
                    new KeyValuePair<uint, int>((uint)index, cmd.ToInt()))
            .ToList()))
    .Build();

cpu.Start(info => Console.WriteLine(info.Stats()));
