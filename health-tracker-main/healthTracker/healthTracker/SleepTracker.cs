using System.Text.Json;
using System.Text.Json.Serialization;

namespace Health_tracker;

public class SleepTracker
{
    private static List<SleepTracker> _sleepTrackers = new List<SleepTracker>();

    private static int IDCounter = 0;
    [JsonInclude]
    private int TrackerID { get; set; }
    [JsonInclude]
    private DateTime Date { get; set; }
    [JsonInclude]
    private int _sleepHours;
    [JsonIgnore]
    public int SleepHours
    {
        get => _sleepHours;
        private set
        {
            if (value < 0)
            {
                throw new ArgumentException("Sleep hours cannot be negative.");
            }
            _sleepHours = value;
        }
    }
    //qJE
    

    [JsonInclude]
    private Quality SleepQuality { get; set; }
    private User _user;

    public SleepTracker()
    {
        TrackerID = IDCounter++;
    }
    public static  List<SleepTracker> GetAllInstances()
    {
        return new List<SleepTracker>(_sleepTrackers);
    }
    public static void WriteClass()
    {
        WriteAllData();
    }
    public User GetUser()
    {
        return _user;
    }
    public void SetUser(User user)
    {
        if (user == null) throw new ArgumentNullException(nameof(user));
        if (_user != null)
        {
            _user.RemoveSleepTracker(this);
        }
        _user = user;
        if (_user != null && !_user.GetSleepTrackers().Contains(this))
        {
            _user.AddSleepTracker(this);
        }
    }
    public void RemoveUser()
    {
        if (_user != null)
        {
            _user.RemoveSleepTracker(this);
        }
        _user = null;
    }
    public static void InitializeClass()
    {
        ReadAllData();
    }
    public SleepTracker(DateTime date, int sleepHours, Quality sleepQuality)
    {
        TrackerID = IDCounter++;
        Date = date;
        SleepHours = sleepHours;
        SleepQuality = sleepQuality;

        Add(this);
    }
    public SleepTracker(DateTime date, int sleepHours, Quality sleepQuality,User user)
    {
        TrackerID = IDCounter++;
        Date = date;
        SleepHours = sleepHours;
        SleepQuality = sleepQuality;
        _user = user;
        _user.AddSleepTracker(this);
        Add(this);
    }
    private void Add(SleepTracker instacne)
    {
        _sleepTrackers.Add(instacne);
    }
    private static void ReadAllData()
    {
        string jsonFilePath = "resources/sleepTrackers.json";

        if (!File.Exists(jsonFilePath) || string.IsNullOrWhiteSpace(File.ReadAllText(jsonFilePath)))
        {
            Console.WriteLine("The " + jsonFilePath + " file is empty");
            _sleepTrackers = new List<SleepTracker>();
            return;
        }

        _sleepTrackers = JsonSerializer
            .Deserialize<List<SleepTracker>>(File.ReadAllText("resources/sleepTrackers.json"));
    }
    private static void WriteAllData()
    {
        foreach (var VARIABLE in _sleepTrackers)
        {
            JsonService.SaveJsonFile(VARIABLE, "resources/sleepTrackers.json");
        }
    }
}