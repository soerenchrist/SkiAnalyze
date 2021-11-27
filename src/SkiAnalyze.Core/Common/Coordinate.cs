using SkiAnalyze.Core.Interfaces.Common;

namespace SkiAnalyze.Core.Common;

public struct Coordinate : ICoordinate
{
    private double _latitude;
    public double Latitude { 
        get => _latitude;
        set
        {
            if (value > 90)
                _latitude = 90;
            else if (value < -90)
                _latitude = -90;
            else
                _latitude = value;
        } 
    }

    private double _longitude;
    public double Longitude { 
        get => _longitude;
        set
        {
            if (value > 180)
                _longitude = 180;
            else if (value < -180)
                _longitude = -180;
            else
                _longitude = value;
        }
    }
}
