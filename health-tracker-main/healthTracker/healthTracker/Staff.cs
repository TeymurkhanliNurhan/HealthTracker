using System.Text.Json.Serialization;

namespace Health_tracker;

public abstract class Staff : Person
{
    private static int IDCounter = 0;
    [JsonInclude]
    private int _staffID;
    protected Staff()
    {
        _staffID = IDCounter++;
    }
    protected Staff(string Name, DateTime DateOfBitrh)
    :base(Name, DateOfBitrh)
    {
        _staffID = IDCounter++;
    }
}