namespace SkiAnalyze.Core.Common;

public struct Coordinate
{
    private float _latitude;
    public float Latitude { 
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

    private float _longitude;
    public float Longitude { 
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
