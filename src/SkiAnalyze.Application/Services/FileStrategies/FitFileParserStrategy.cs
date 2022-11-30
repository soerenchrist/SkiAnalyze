using Dynastream.Fit;
using FitDecode;
using SkiAnalyze.Core.Common;
using SkiAnalyze.Core.Entities.TrackAggregate;
using SkiAnalyze.Core.Interfaces;

namespace SkiAnalyze.Application.Services.FileStrategies;

public class FitFileParserStrategy : ITrackFileParserStrategy
{
    public FileReadResult ReadFileContents(Stream fileContent)
    {
        FitDecoder decoder = new FitDecoder(fileContent, Dynastream.Fit.File.Activity);

        decoder.Decode();

        if (decoder.Messages.Activity == null)
            throw new FitException("Could not decode activity");

        var runs = ReadRuns(decoder);
        runs = runs.Where(x => x.Coordinates.Count > 0).ToList();

        var name = decoder.Messages.Sessions[0].GetSport()?.ToString() ?? "";

        return new FileReadResult(name, runs);
    }

    private List<Run> ReadRuns(FitDecoder decoder)
    {
        var laps = new Queue<LapMesg>(decoder.Messages.Laps);
        var lap = laps.Count > 0 ? laps.Dequeue() : null;
        var lapId = 0;

        var timezoneOffset = decoder.Messages.Activity?.TimezoneOffset() ?? TimeSpan.Zero;
        var runs = new Run[decoder.Messages.Laps.Count];
        for (int i = 0; i < runs.Length; i++)
            runs[i] = new Run
            {
                Number = i + 1,
                Downhill = true,
            };

        if (lap != null)
            SetLapValuesToRun(runs[0], lap, timezoneOffset);

        foreach (var record in decoder.Messages.Records)
        {
            while(lap != null && record.GetTimestamp().GetTimeStamp() > lap.GetTimestamp().GetTimeStamp())
            {
                lap = laps.Count > 0 ? laps.Dequeue() : null;
                lapId++;
                if (lap != null)
                    SetLapValuesToRun(runs[lapId], lap, timezoneOffset);
            }
            var lat = record.GetLatitude();
            var lon = record.GetLongitude();

            if (lat == null || lon == null)
                continue;

            var trackPoint = new TrackPoint
            {
                Latitude = lat.Value,
                Longitude = lon.Value,
                DateTime = record.GetTimestamp().GetDateTime(),
                Elevation = record.GetEnhancedAltitude() ?? record.GetAltitude(),
                Speed = record.GetEnhancedSpeed() ?? record.GetSpeed(),
                Distance = record.GetDistance(),
                HeartRate = record.GetHeartRate(),
            };
            runs[lapId].Coordinates.Add(trackPoint);
        }

        return runs.ToList();
    }

    private void SetLapValuesToRun(Run run, LapMesg lap, TimeSpan offset)
    {
        run.AverageHeartRate = lap.GetAvgHeartRate() ?? 0;
        run.AverageSpeed = lap.GetEnhancedAvgSpeed() ?? 0;
        run.MaxHeartRate = lap.GetMaxHeartRate() ?? 0;
        run.MaxSpeed = lap.GetEnhancedMaxSpeed() ?? 0;
        run.TotalDistance = lap.GetTotalDistance() ?? 0;
        run.TotalCalories = lap.GetTotalCalories();
        run.TotalElevation = (lap.GetTotalDescent() ?? 0) * -1;
        run.Start = lap.GetStartTime()?.GetDateTime() + offset ?? System.DateTime.Now;
        run.End = FitExtensions.GetEndTime(lap)?.GetDateTime() + offset ?? System.DateTime.Now;
    }
}
