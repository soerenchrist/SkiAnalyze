using NetTopologySuite.IO;
using SkiAnalyze.Core.Entities.TrackAggregate;
using System.Xml;

namespace SkiAnalyze.Core.Services.Gpx;

public class GpxFileLoader
{
    public IEnumerable<GpxFile> LoadGpxFiles(List<Track> tracks)
    {
        foreach (var track in tracks)
        {
            using var stream = new MemoryStream(track.FileContents);
            using var xmlReader = new XmlTextReader(stream);
            var file = GpxFile.ReadFrom(xmlReader, new GpxReaderSettings
            {
                TimeZoneInfo = TimeZoneInfo.Local
            });

            yield return file;
        }
    }

    public GpxFile LoadGpxFile(Track track)
    {
        using var stream = new MemoryStream(track.FileContents);
        using var xmlReader = new XmlTextReader(stream);
        var file = GpxFile.ReadFrom(xmlReader, new GpxReaderSettings
        {
            TimeZoneInfo = TimeZoneInfo.Local
        });
        return file;
    }


    public GpxFile LoadGpxFile(string content)
    {
        using var stringReader = new StringReader(content);
        using var xmlReader = new XmlTextReader(stringReader);
        var file = GpxFile.ReadFrom(xmlReader, new GpxReaderSettings
        {
            TimeZoneInfo = TimeZoneInfo.Local
        });
        return file;
    }

    public GpxFile LoadGpxFile(Stream content)
    {
        using var xmlReader = new XmlTextReader(content);
        var file = GpxFile.ReadFrom(xmlReader, new GpxReaderSettings
        {
            TimeZoneInfo = TimeZoneInfo.Local
        });
        return file;
    }
}
