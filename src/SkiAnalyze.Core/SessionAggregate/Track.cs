using SkiAnalyze.SharedKernel;

namespace SkiAnalyze.Core.SessionAggregate;

public class Track : BaseEntity<int>
{
    public string Name { get; set; } = string.Empty;
    public string GpxFileContents { get; set; } = string.Empty;
    public string HexColor { get; set; } = string.Empty;
    public Guid UserSessionId { get; set; }
    public UserSession? UserSession { get; set; }
}
