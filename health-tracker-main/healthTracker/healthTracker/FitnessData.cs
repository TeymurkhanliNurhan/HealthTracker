using System.Text.Json;
using System.Text.Json.Serialization;

namespace Health_tracker;

public class FitnessData
{
    private static List<FitnessData> _fitnessDatas = new List<FitnessData>();
    private static int IDCounter = 0;
    private User _user;

    [JsonInclude]
    private int DataID { get; set; }
    [JsonInclude]
    private DateTime Date { get; set; }
    [JsonInclude]
    private int _stepCount;
    [JsonIgnore]
    public int StepCount
    {
        get => _stepCount;
        private set
        {
            if (value < 0)
            {
                throw new ArgumentException("Step count cannot be negative");
            }
            _stepCount = value;
        }
    }
    [JsonInclude]
    private int _caloriesBurnt;
    [JsonIgnore]
    public int CaloriesBurnt
    {
        get => _caloriesBurnt;
        private set
        {
            if (value < 0)
            {
                throw new ArgumentException("Calories burnt cannot be negative");
            }
            _caloriesBurnt = value;
        }
    }
    [JsonInclude]
    private float _distanceTravelled;
    [JsonIgnore]
    public float DistanceTravelled
    {
        get => _distanceTravelled;
        private set
        {
            if (value < 0)
            {
                throw new ArgumentException("Distance travelled cannot be negative");
            }
            _distanceTravelled = value;
        }
    }
    
    public User GetUser()
    {
        return _user;
    }

    public void SetUser(User user)
    {
        if (user == null) throw new ArgumentNullException();
        if (_user != null) _user.RemoveFitnessData(this);
        _user = user;
        if (!_user.GetFitnessDatas().Contains(this)) _user.AddFitnessData(this);
    }

    public void RemoveUser()
    {
        if (_user.GetFitnessDatas().Contains(this)) _user.RemoveFitnessData(this);
        _user = null;
    }
    
    public static  List<FitnessData> GetAllInstances()
    {
        return new List<FitnessData>(_fitnessDatas);
    }
    
    public static void WriteClass()
    {
        WriteAllData();
    }

    public static void InitializeClass()
    {
        ReadAllData();
    }

    public FitnessData()
    {
        DataID = IDCounter++;
    }

    public FitnessData(DateTime date, int stepCount, int caloriesBurnt, float distanceTravelled)
    {
        DataID = IDCounter++;
        Date = date;
        StepCount = stepCount;
        CaloriesBurnt = caloriesBurnt;
        DistanceTravelled = distanceTravelled;
        
        Add(this);
    }
    
    public FitnessData(DateTime date, int stepCount, int caloriesBurnt, float distanceTravelled, User user)
    {
        DataID = IDCounter++;
        Date = date;
        StepCount = stepCount;
        CaloriesBurnt = caloriesBurnt;
        DistanceTravelled = distanceTravelled;
        _user = user;
        _user.AddFitnessData(this);
        
        Add(this);
    }
    private void Add(FitnessData instacne)
    {
        _fitnessDatas.Add(instacne);
    }
    private static void ReadAllData()
    {
        string jsonFilePath = "resources/fitnessData.json";
        
        if (!File.Exists(jsonFilePath) || string.IsNullOrWhiteSpace(File.ReadAllText(jsonFilePath)))
        {
            Console.WriteLine("The " + jsonFilePath + " file is empty");
            _fitnessDatas = new List<FitnessData>();
            return;
        }
        
        _fitnessDatas = JsonSerializer
            .Deserialize<List<FitnessData>>(File.ReadAllText("resources/fitnessData.json"));
    }
    
    private static void WriteAllData()
    {
        foreach (var VARIABLE in _fitnessDatas)
        {
            JsonService.SaveJsonFile(VARIABLE, "resources/fitnessData.json");
        }
    }

}