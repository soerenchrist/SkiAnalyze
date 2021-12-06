using Dynastream.Fit;
using FitDecode.Exceptions;
using FitDecode.Models;

namespace FitDecode;

public class FitDecoder
{
    public FitMessages Messages { get; }
    private readonly Stream _inputStream;
    private readonly Dynastream.Fit.File _fileType;

    public FitDecoder(Stream stream, Dynastream.Fit.File fileType)
    {
        _inputStream = stream;
        _fileType = fileType;

        Messages = new FitMessages();
    }

    public bool Decode()
    {
        var decoder = new Decode();

        if (!decoder.IsFIT(_inputStream))
            throw new FileTypeException($"Expected FIT File type: {_fileType}, received a non FIT file");

        var broadcaster = new MesgBroadcaster();

        // connect the decode and message broadcasters
        decoder.MesgEvent += broadcaster.OnMesg;
        decoder.MesgDefinitionEvent += broadcaster.OnMesgDefinition;
        decoder.DeveloperFieldDescriptionEvent += OnDeveloperFieldDescriptionEvent;

        broadcaster.ActivityMesgEvent += OnActivityMesg;
        broadcaster.DeviceInfoMesgEvent += OnDeviceInfoMesg;
        broadcaster.EventMesgEvent += OnEventMesg;
        broadcaster.FileIdMesgEvent += OnFileIdMesg;
        broadcaster.HrMesgEvent += OnHrMesg;
        broadcaster.HrvMesgEvent += OnHrvMesg;
        broadcaster.LapMesgEvent += OnLapMesg;
        broadcaster.LengthMesgEvent += OnLengthMesg;
        broadcaster.RecordMesgEvent += OnRecordMesg;
        broadcaster.SegmentLapMesgEvent += OnSegmentLapMesg;
        broadcaster.SessionMesgEvent += OnSessionMesg;
        broadcaster.UserProfileMesgEvent += OnUserProfileMesg;
        broadcaster.WorkoutMesgEvent += OnWorkoutMesg;
        broadcaster.WorkoutStepMesgEvent += OnWorkoutStepMesg;
        broadcaster.ZonesTargetMesgEvent += OnZonesTargetMesg;

        // try decoding
        bool readOk = decoder.Read(_inputStream);

        // merge the heartrates with the records
        if (readOk && Messages.HeartRates.Count > 0)
        {
            HrToRecordMesgHelper.MergeHeartRates(Messages);
        }

        return readOk;
    }


    #region MessageHandlers

    private void OnActivityMesg(object sender, MesgEventArgs e)
    {
        Messages.Activity = (ActivityMesg)e.mesg;
    }

    private void OnDeviceInfoMesg(object sender, MesgEventArgs e)
    {
        Messages.DeviceInfos.Add((DeviceInfoMesg) e.mesg);
    }

    private void OnEventMesg(object sender, MesgEventArgs e)
    {
        var eventMesg = (EventMesg) e.mesg;
        Messages.Events.Add(eventMesg);

        if (eventMesg?.GetEvent() == Event.Timer && eventMesg?.GetTimestamp() != null)
        {
            Messages.Records.Add(new ExtendedRecordMesg(eventMesg));
        }
    }

    private void OnFileIdMesg(object sender, MesgEventArgs e)
    {
        Messages.FileId = (FileIdMesg)e.mesg;
        if (((FileIdMesg)e.mesg).GetType() != _fileType)
        {
            throw new FileTypeException($"Expected FIT File Type: {_fileType}, recieved File Type: {((FileIdMesg)e.mesg).GetType()}");
        }
    }

    private void OnHrMesg(object sender, MesgEventArgs e)
    {
        Messages.HeartRates.Add((HrMesg)e.mesg);
    }

    private void OnHrvMesg(object sender, MesgEventArgs e)
    {
        Messages.HeartRateVariabilities.Add((HrvMesg)e.mesg);
    }

    private void OnLapMesg(object sender, MesgEventArgs e)
    {
        Messages.Laps.Add((LapMesg)e.mesg);
    }

    private void OnLengthMesg(object sender, MesgEventArgs e)
    {
        Messages.Lengths.Add((LengthMesg)e.mesg);
    }

    private void OnRecordMesg(object sender, MesgEventArgs e)
    {
        Messages.Records.Add(new ExtendedRecordMesg((RecordMesg) e.mesg));

        foreach (Field field in e.mesg.Fields)
        {
            if (field.Name.ToLower() != "unknown")
            {
                Messages.RecordFieldNames.Add(field.Name);
            }
        }

        foreach (DeveloperField devField in e.mesg.DeveloperFields)
        {
            Messages.RecordDeveloperFieldNames.Add(devField.Name);
        }
    }

    private void OnSegmentLapMesg(object sender, MesgEventArgs e)
    {
        Messages.SegmentLaps.Add((SegmentLapMesg)e.mesg);
    }

    private void OnSessionMesg(object sender, MesgEventArgs e)
    {
        Messages.Sessions.Add((SessionMesg)e.mesg);
    }

    private void OnUserProfileMesg(object sender, MesgEventArgs e)
    {
        Messages.UserProfile = (UserProfileMesg)e.mesg;
    }

    private void OnWorkoutMesg(object sender, MesgEventArgs e)
    {
        Messages.Workout = (WorkoutMesg)e.mesg;
    }

    private void OnWorkoutStepMesg(object sender, MesgEventArgs e)
    {
        Messages.WorkoutSteps.Add((WorkoutStepMesg)e.mesg);
    }

    private void OnZonesTargetMesg(object sender, MesgEventArgs e)
    {
        Messages.ZonesTarget = (ZonesTargetMesg)e.mesg;
    }

    private void OnDeveloperFieldDescriptionEvent(object? sender, DeveloperFieldDescriptionEventArgs e)
    {
        Messages.DeveloperFieldDescriptions.Add(e.Description);
    }
    #endregion
}
