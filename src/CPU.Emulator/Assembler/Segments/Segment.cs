using System.Text.RegularExpressions;

namespace CPU.Emulator.Assembler.Segments;

public class Segment
{
    public const string SegmentPattern =
        @"(?<segmentName>.*) segment '(?<segmentType>.*)' begin\n (?<data>[a-z \n;0-9,A-Z:_]*)\nend";

    public const string MarkPattern = @"[A-Za-z_]*:";

    public SegmentType Type { get; set; }
    public string Name { get; set; }
    public IEnumerable<string> Data { get; set; }

    public static IEnumerable<Segment> ReadAllSegmentsFromFile(string file)
    {
        var segments = new List<Segment>();
        var marks = new Dictionary<string, uint>();

        foreach (Match m in Regex.Matches(file, SegmentPattern,
                     RegexOptions.Multiline))
        {
            var name = m.Groups["segmentName"].Value;
            SegmentType type;
            Enum.TryParse(m.Groups["segmentType"].Value,
                ignoreCase: true,
                out type);
            var data = m.Groups["data"].Value;

            var splits = data.Split('\n')
                .Select(s => s.Trim())
                .Select((s, idx) =>
                {
                    if (!Regex.IsMatch(s, MarkPattern))
                    {
                        return s;
                    }
                    var match = Regex.Match(s, MarkPattern);

                    marks.Add(match.Value.Replace(":",""), 
                        (uint)(idx - marks.Count));
                    return "";
                })
                .ToList();

            splits.RemoveAll(string.IsNullOrWhiteSpace);

            if (marks.Count > 0)
            {
                var regex = MarksToRegex(marks);
                splits = splits
                    .Select(s =>
                    {
                        if (!Regex.IsMatch(s, regex))
                        {
                            return s;
                        }
                    
                        var m = Regex.Match(s, regex);
            
                        var addr = marks[m.Value];
                        return s.Replace(m.Value, $"0x{Convert.ToString(addr, 16)}");
                    })
                    .ToList();   
            }

            segments.Add(new Segment()
            {
                Type = type,
                Name = name,
                Data = splits
            });
        }

        return segments;
    }

    public static string MarksToRegex(Dictionary<string, uint> marks)
    {
        var keys = marks
            .Select(kv => kv.Key)
            .ToList();

        return $"(?:{string.Join("|", keys)})";
    }
}
