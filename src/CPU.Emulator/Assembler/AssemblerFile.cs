using System.Collections;
using CPU.Emulator.Assembler.Segments;
using CPU.Emulator.Commands;
using CPU.Emulator.Commands.Serializer;

namespace CPU.Emulator.Assembler;

public class AssemblerFile : IAssamblerFile
{
    public IList<Command> Commands { get; set; }
    public IList<int> StaticData { get; set; }

    public static AssemblerFile ReadFromFiler(string path)
    {
        var assemblerFile = new AssemblerFile();
        
        var file = File.ReadAllText(path);

        var segments = Segment.ReadAllSegmentsFromFile(file);

        foreach (var segment in segments)
        {
            switch (segment.Type)
            {
                case SegmentType.Program:
                    assemblerFile.Commands = ReadProgram(segment);
                    break;
                case SegmentType.Data:
                    assemblerFile.StaticData = ReadStaticData(segment);
                    break;
            }
        }

        return assemblerFile;
    }

    public static IList<Command> ReadProgram(Segment segment)
    {
        var cs = new CommandSerializer();
        
        return segment.Data
            .Select(data => cs.DeserializeCommand(data))
            .ToList();
    }

    public static IList<int> ReadStaticData(Segment segment)
    {
        return segment.Data
            .Select(data => data.Replace(";", ""))
            .Select(data => int.Parse(data))
            .ToList();
    }
}
