using CPU.Emulator;
using CPU.Emulator.Commands;
using CPU.Emulator.Commands.Serializer;
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

var program = @"sl 0x0, d
sl 0x9, e
lm d
sub b
jlz 0x7
add b
ma b
inc d
dec e
jgz 0x2
stop";

var cs = new CommandSerializer();

var cpu = ICpuBuilder
    .Builder((ushort)EnumUtils.GetValues<RegistersAddress>().Count())
    .SetDataMemory(b =>
        b
            .Add(32)
            .Add(data.Select((v, index) =>
                new KeyValuePair<uint, int>((uint)(index), v))))
    .SetInstructionMemory(b => b
        .Add(32)
        .Add(
            program.Split("\n")
                .Select(cmd => cs.DeserializeCommand(cmd))
                .Select(cmd => cmd.ToInt())
                .Select((cmd, index) =>
                    new KeyValuePair<uint, int>((uint)index, cmd))
        ))
    .Build();

cpu.Start(info => Console.WriteLine(info.Stats()));


