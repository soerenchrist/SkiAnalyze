using NetTopologySuite.IO;
using SkiAnalyze.Core.TrackAggregate;
using System.Xml;

namespace SkiAnalyze.Core.Services.Gpx;

public class GpxFileLoader
{
    public IEnumerable<GpxFile> LoadGpxFiles(List<Track> tracks)
    {
        foreach (var track in tracks)
        {
            using var stringReader = new StringReader(track.GpxFileContents);
            using var xmlReader = new XmlTextReader(stringReader);
            var file = GpxFile.ReadFrom(xmlReader, new GpxReaderSettings
            {
                TimeZoneInfo = TimeZoneInfo.Local
            });

            yield return file;
        }
    }

    public GpxFile LoadGpxFile(Track track)
    {
        using var stringReader = new StringReader(track.GpxFileContents);
        using var xmlReader = new XmlTextReader(stringReader);
        var file = GpxFile.ReadFrom(xmlReader, new GpxReaderSettings
        {
            TimeZoneInfo = TimeZoneInfo.Local
        });
        return file;
    }
}
