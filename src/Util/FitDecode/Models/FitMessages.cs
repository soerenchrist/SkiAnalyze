using Dynastream.Fit;

namespace FitDecode.Models;

public class FitMessages
{
    public ActivityMesg? Activity { get; set; }
    public List<DeviceInfoMesg> DeviceInfos { get; set; } = new();
    public List<EventMesg> Events { get; set; } = new();
    public FileIdMesg? FileId { get; set; }
    public List<HrMesg> HeartRates { get; set; } = new();
    public List<HrvMesg> HeartRateVariabilities { get; set; } = new();
    public List<LapMesg> Laps { get; set; } = new();
    public List<LengthMesg> Lengths { get; set; } = new();
    public List<SegmentLapMesg> SegmentLaps { get; set; } = new();
    public List<SessionMesg> Sessions { get; set; } = new();
    public UserProfileMesg? UserProfile{ get; set; }
    public WorkoutMesg? Workout { get; set; }
    public List<ExtendedRecordMesg> Records = new ();
    public List<WorkoutStepMesg> WorkoutSteps { get; set; } = new();
    public ZonesTargetMesg? ZonesTarget { get; set; }
    public List<DeveloperFieldDescription> DeveloperFieldDescriptions { get; set; } = new();
    public HashSet<string> RecordFieldNames { get; set; } = new();
    public HashSet<string> RecordDeveloperFieldNames { get; set; } = new();
}
