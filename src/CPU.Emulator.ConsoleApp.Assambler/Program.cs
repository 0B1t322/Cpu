using CPU.Emulator;
using CPU.Emulator.Assembler;
using CPU.Emulator.Commands.Serializer;
using CPU.Emulator.Utils.Enums;

var file = AssemblerFile.ReadFromFiler("max_in_mass.txt");

var cpu = ICpuBuilder
    .Builder((ushort)EnumUtils.GetValues<RegistersAddress>().Count())
    .SetDataMemory(b =>
        b
            .Add((uint)file.StaticData.Count)
            .Add(file.StaticData.Select((v, index) =>
                new KeyValuePair<uint, int>((uint)(index), v))))
    .SetInstructionMemory(b => b
        .Add((uint)file.Commands.Count)
        .Add(
            file.Commands
                .Select(cmd => cmd.ToInt())
                .Select((cmd, index) =>
                    new KeyValuePair<uint, int>((uint)index, cmd))
        ))
    .Build();

cpu.Start(info => Console.WriteLine(info.Stats()));


