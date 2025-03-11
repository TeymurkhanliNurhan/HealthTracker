using System.Text.Json;
using System.Text.Json.Serialization;

namespace Health_tracker;

public class Diet
{
    private static List<Diet> _diet = new List<Diet>();
    private static int IDCounter = 0;

    [JsonInclude]
    private int DietID { get; set; }
    [JsonInclude]

    private double _caloriesTarget;
    [JsonIgnore]
    public double CaloriesTarget
    {
        get => _caloriesTarget;
        private set
        {
            if (value <= 0)
            {
                throw new ArgumentException("Calories target must be positive.");
            }
            _caloriesTarget = value;
        }
    }
    [JsonInclude]
    private List<Meal> Meals { get; set; }
    [JsonInclude]
    private Doctor _doctor;
        [JsonInclude]
        private List<User> _users = new List<User>();
        public List<User> GetUsers()
        {
            return new List<User>(_users);
        }
        public void AddUser(User user)
        {
            if (user == null) throw new ArgumentNullException(nameof(user));

            if (!_users.Contains(user))
            {
                _users.Add(user);
                if (user.GetDiet() != this)
                {
                    user.SetDiet(this);
                }
            }
        }
        public bool RemoveUser(User user)
        {
            if (_users.Remove(user))
            {
                if (user.GetDiet() == this)
                {
                    user.RemoveDiet();
                }
                return true;
            }
            return false;
        }
    public static  List<Diet> GetAllInstances()
    {
        return new List<Diet>(_diet);
    }

    public static void WriteClass()
    {
        WriteAllData();
    }

    public static void InitializeClass()
    {
        ReadAllData();
    }
    public Diet()
    {
        DietID = IDCounter++;
    }

    public Diet(double caloriesTarget)
    {
        DietID = IDCounter++;
        CaloriesTarget = caloriesTarget;
        Meals = new List<Meal>();

        Add(this);
    }
    public Diet(double caloriesTarget, List<Meal> meals, Doctor doctor,User user)
    {
        DietID = IDCounter++;
        CaloriesTarget = caloriesTarget;
        Meals = new List<Meal>();
        SetDoctor(doctor);
        AddUser(user);
        Add(this);
    }
    public Doctor GetDoctor()
    {
        return _doctor;
    }
    public void SetDoctor(Doctor doctor)
    {
        if (_doctor == doctor)
        {
            return;
        }
        if (_doctor != null)
        {
            if (_doctor != null) {
                _doctor.RemoveDiet(this);
            }
        }
        _doctor = doctor;
        if (_doctor != null)
        {
            _doctor.AddDiet(this);
        }
    }
    public void RemoveDoctor()
    {
        if (_doctor != null)
        {
            _doctor.RemoveDiet(this);
        }
        _doctor = null;
    }
    private void Add(Diet instacne)
    {
        _diet.Add(instacne);
    }
    private static void ReadAllData()
    {
        string jsonFilePath = "resources/diets.json";

        if (!File.Exists(jsonFilePath) || string.IsNullOrWhiteSpace(File.ReadAllText(jsonFilePath)))
        {
            Console.WriteLine("The " + jsonFilePath + " file is empty");
            _diet = new List<Diet>();
            return;
        }

        _diet = JsonSerializer
            .Deserialize<List<Diet>>(File.ReadAllText("resources/diets.json"));
    }

    private static void WriteAllData()
    {
        foreach (var VARIABLE in _diet)
        {
            JsonService.SaveJsonFile(VARIABLE, "resources/diet.json");
        }
    }
}