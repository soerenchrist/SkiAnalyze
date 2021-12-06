using Dynastream.Fit;

namespace FitDecode.Models;

public class ExtendedRecordMesg : RecordMesg
{
    public EventType EventType { get; }
    public ExtendedRecordMesg(RecordMesg mesg) : base(mesg)
    {
        EventType = EventType.Invalid;
    }

    public ExtendedRecordMesg(EventMesg mesg)
    {
        SetTimestamp(mesg.GetTimestamp());
        EventType = mesg.GetEventType() ?? EventType.Invalid;
    }
}
