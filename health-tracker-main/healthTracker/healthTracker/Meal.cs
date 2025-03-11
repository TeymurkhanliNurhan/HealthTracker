using System.Text.Json;
using System.Text.Json.Serialization;

namespace Health_tracker;

public class Meal
{
    private static List<Meal> _meals = new List<Meal>();
    private static int IDCounter = 0;
    
    [JsonInclude]
    private int MealID { get; set; }
    [JsonInclude]
    private string _dish;
    [JsonIgnore]
    public string Dish
    {
        get => _dish;
        private set
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentException("Dish name cannot be null or empty");
            }
            _dish = value;
        }
    }
    [JsonInclude]
    private int _calories;
    [JsonIgnore]
    public int Calories
    {
        get => _calories;
        private set
        {
            if (value <= 0)
            {
                throw new ArgumentException("Calories must be positive");
            }
            _calories = value;
        }
    }
    
    private User _user;

    public User GetUser()
    {
        return _user;
    }

    public void SetUser(User user)
    {
        if (user == null) throw new ArgumentNullException();
        if (_user != null) _user.RemoveMeal(this);
        _user = user;
        if (!_user.GetMeals().Contains(this)) _user.AddMeal(this);
    }

    public void RemoveUser()
    {
        if (_user.GetMeals().Contains(this)) _user.RemoveMeal(this);
        _user = null;
    }
    
    public static  List<Meal> GetAllInstances()
    {
        return new List<Meal>(_meals);
    }

    public static void WriteClass()
    {
        WriteAllData();
    }

    public static void InitializeClass()
    {
        ReadAllData();
    }

    public Meal()
    {
        MealID = IDCounter++;
    }
    
    public Meal(string dish, int calories)
    {
        MealID = IDCounter++;
        Dish = dish;
        Calories = calories;
        
        Add(this);
    }
    
    public Meal(string dish, int calories, User user)
    {
        MealID = IDCounter++;
        Dish = dish;
        Calories = calories;
        _user = user;
        
        Add(this);
    }

    public override string ToString()
    {
        return MealID + " - " + Dish;
    }
    private void Add(Meal instacne)
    {
        _meals.Add(instacne);
    }
    private static void ReadAllData()
    {
        string jsonFilePath = "resources/meals.json";
        
        if (!File.Exists(jsonFilePath) || string.IsNullOrWhiteSpace(File.ReadAllText(jsonFilePath)))
        {
            Console.WriteLine("The " + jsonFilePath + " file is empty");
            _meals = new List<Meal>();
            return;
        }
        
        _meals = JsonSerializer
            .Deserialize<List<Meal>>(File.ReadAllText("resources/meals.json"));
    }
    
    private static void WriteAllData()
    {
        foreach (var VARIABLE in _meals)
        {
            JsonService.SaveJsonFile(VARIABLE, "resources/meals.json");
        }
    }
}