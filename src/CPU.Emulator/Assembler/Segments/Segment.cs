using System.Text.RegularExpressions;

namespace CPU.Emulator.Assembler.Segments;

public class Segment
{
    public const string SegmentPatter =
        @"(?<segmentName>.*) segment '(?<segmentType>.*)' begin\n (?<data>[a-z \n;0-9,]*)\nend";

    public SegmentType Type { get; set; }
    public string Name { get; set; }
    public IEnumerable<string> Data { get; set; }

    public static IEnumerable<Segment> ReadAllSegmentsFromFile(string file)
    {
        var segments = new List<Segment>();
        RegexOptions options = RegexOptions.Multiline;

        foreach (Match m in Regex.Matches(file, SegmentPatter, options))
        {
            var name = m.Groups["segmentName"].Value;
            SegmentType type;
            Enum.TryParse(m.Groups["segmentType"].Value,
                ignoreCase: true,
                out type);
            var data = m.Groups["data"].Value;

            var splits = data.Split('\n')
                .Select(s => s.Trim())
                .ToList();
            
            segments.Add(new Segment()
            {
                Type = type,
                Name = name,
                Data = splits
            });
        }

        return segments;
    }
}
