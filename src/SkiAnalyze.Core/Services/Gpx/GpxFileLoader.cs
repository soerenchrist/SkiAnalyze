using NetTopologySuite.IO;
using System.Xml;

namespace SkiAnalyze.Core.Services.Gpx;

public class GpxFileLoader
{
    public IEnumerable<GpxFile> LoadGpxFiles(List<SessionAggregate.Track> tracks)
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
}
