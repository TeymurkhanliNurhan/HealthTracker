using System.Text.Json;
using System.Text.Json.Serialization;

namespace Health_tracker;

public class HealthMetric
{
    private static List<HealthMetric> _healthMetrics = new List<HealthMetric>();
    private static int IDCounter = 0;
    private User _user;

    [JsonInclude]
    private int MetricID { get; set; }
    [JsonInclude]
    private float? _bloodPressure;
    [JsonIgnore]
    public float? BloodPressure
    {
        get => _bloodPressure;
        private set
        {
            if (value != null && value <= 0)
            {
                throw new ArgumentException("Blood pressure must be a positive value");
            }
            _bloodPressure = value;
        }
    }
    [JsonInclude]
    private float? _heartRate;
    [JsonIgnore]
    public float? HeartRate
    {
        get => _heartRate;
        private set
        {
            if (value != null && value <= 0)
            {
                throw new ArgumentException("Heart rate must be a positive value");
            }
            _heartRate = value;
        }
    }


    public static  List<HealthMetric> GetAllInstances()
    {
        return new List<HealthMetric>(_healthMetrics);
    }

    public static void WriteClass()
    {
        WriteAllData();
    }


    public static void InitializeClass()
    {
        ReadAllData();
    }
    public HealthMetric()
    {
        MetricID = IDCounter++;
    }

    public HealthMetric(float bloodPressure, float heartRate)
    {
        MetricID = IDCounter++;
        BloodPressure = bloodPressure;
        HeartRate = heartRate;

        Add(this);
    }

    public HealthMetric(float bloodPressure, float heartRate, User user)
    {
        MetricID = IDCounter++;
        BloodPressure = bloodPressure;
        HeartRate = heartRate;
        _user = user;
        _user.AddHealthMetric(this);

        Add(this);
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
            _user.RemoveHealthMetric();
        }
        _user = user;

        if (_user != null && _user.GetHealthMetric() != this)
        {
            _user.AddHealthMetric(this);
        }
    }
    public void RemoveUser()
    {
        if (_user != null)
        {
            _user.RemoveHealthMetric();
        }
        _user = null;
    }





    
    private void Add(HealthMetric instacne)
    {
        _healthMetrics.Add(instacne);
    }
    private static void ReadAllData()
    {
        string jsonFilePath = "resources/healthMetrics.json";

        if (!File.Exists(jsonFilePath) || string.IsNullOrWhiteSpace(File.ReadAllText(jsonFilePath)))
        {
            Console.WriteLine("The " + jsonFilePath + " file is empty");
            _healthMetrics = new List<HealthMetric>();
            return;
        }

        _healthMetrics = JsonSerializer
            .Deserialize<List<HealthMetric>>(File.ReadAllText("resources/healthMetrics.json"));
    }
    private static void WriteAllData()
    {
        foreach (var VARIABLE in _healthMetrics)
        {
            JsonService.SaveJsonFile(VARIABLE, "resources/healthMetrics.json");
        }
    }
}