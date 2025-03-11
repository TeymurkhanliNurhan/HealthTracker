using System.Text.Json;
using System.Text.Json.Serialization;
using Health_tracker;

public class User : Person
{
    private static List<User> _users = new List<User>();
    private static int IDCounter = 0;

    [JsonInclude]
    private int UserID { get; set; }

    [JsonInclude]
    private float _weight;
    [JsonIgnore]
    public float Weight
    {
        get => _weight;
        private set
        {
            if (value <= 0)
            {
                throw new ArgumentException("Weight must be a positive value.");
            }
            _weight = value;
        }
    }
    [JsonInclude]
    private float _height;
    [JsonIgnore]
    public float Height
    {
        get => _height;
        private set
        {
            if (value <= 0)
            {
                throw new ArgumentException("Height must be a positive value.");
            }
            _height = value;
        }
    }
    [JsonInclude]
    private string _username;
    [JsonIgnore]
    public string Username
    {
        get => _username;
        private set
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentException("Username cannot be null or empty.");
            }
            _username = value;
        }
    }
    [JsonInclude]
    private string _password;
    [JsonIgnore]
    public string Password
    {
        get => _password;
        private set
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentException("Password cannot be null or empty.");
            }
            _password = value;
        }
    }
    [JsonInclude]
    private List<HealthMetric> HealthMetrics { get; set; }

    [JsonInclude]
    private Diet? Diet { get; set; }
    [JsonInclude]
    private List<Meal> Meals { get; set; }
    [JsonInclude]
    private List<Device> Devices { get; set; }

    [JsonInclude]
    private static int UsersCount { get; set; } = 0;
    [JsonInclude]
    private List<FitnessData> _fitnessDatas = new List<FitnessData>();
    [JsonInclude]
    private HealthMetric _healthMetric;
    [JsonInclude]
    private List<SleepTracker> _sleepTrackers = new List<SleepTracker>();
    [JsonInclude]
    private List<Meal> _meals = new List<Meal>();
    [JsonInclude]
    private List<User_Achievement> _achievements = new List<User_Achievement>();
    [JsonInclude]
    private List<User_Workout> _workouts = new List<User_Workout>();
    [JsonInclude]
    private List<Device> _devices = new List<Device>();
    public List<Device> GetDevices() => new List<Device>(_devices);
    public void AddDevice(Device device)
    {
        if (device == null) throw new ArgumentNullException(nameof(device));

        if (!_devices.Contains(device))
        {
            _devices.Add(device);
            if (!device.GetUsers().Contains(this))
            {
                device.AddUser(this);
            }
        }
    }
    public bool RemoveDevice(Device device)
    {
        if (_devices.Remove(device))
        {
            if (device.GetUsers().Contains(this))
            {
                device.RemoveUser(this);
            }
            return true;
        }
        return false;
    }
    public bool EditDevice(Device oldDevice, Device newDevice)
    {
        if (oldDevice == null || newDevice == null) throw new ArgumentNullException();

        if (!_devices.Remove(oldDevice)) return false;

        oldDevice.RemoveUser(this);
        AddDevice(newDevice);
        return true;
    }
    public List<User_Workout> GetWorkouts()
    {
        return new List<User_Workout>(_workouts);
    }

    public void AddWorkout(Workout workout)
    {
        if (workout == null) throw new ArgumentNullException();
        if (workout.GetUsers().Any(a => a.GetUser() == this))
        {
            if (!_workouts.Any(a => a.GetWorkout() == workout))
            {
                _workouts.Add(workout.GetUsers().First(a => a.GetUser() == this));
            }
        }
        else
        {
            User_Workout userWorkout = new User_Workout(DateTime.Now);
            _workouts.Add(userWorkout);
            userWorkout.SetUser(this);
            userWorkout.SetWorkout(workout);
        }
    }

    public bool RemoveWorkout(Workout workout)
    {
        User_Workout userWorkout = _workouts.FirstOrDefault(a => a.GetWorkout() == workout);
        bool res = _workouts.Remove(userWorkout);
        if (userWorkout != null && userWorkout.GetUser() != null && userWorkout.GetUser().Equals(this)) userWorkout.RemoveUser();
        return res;
    }

    public bool EditWorkout(Workout oldWorkout, Workout newWorkout)
    {
        User_Workout oldUserWorkout = _workouts.FirstOrDefault(a => a.GetWorkout() == oldWorkout);
        if (!_workouts.Remove(oldUserWorkout))
        {
            return false;
        }
        oldUserWorkout.RemoveUser();

        User_Workout newUserWorkout = new User_Workout(DateTime.Now);
        _workouts.Add(newUserWorkout);
        newUserWorkout.SetUser(this);
        newUserWorkout.SetWorkout(newWorkout);
        return true;
    }


    public Diet? GetDiet()
    {
        return Diet;
    }
    public void SetDiet(Diet diet)
    {
        if (diet == null) throw new ArgumentNullException(nameof(diet));

        if (this.Diet != null)
        {
            RemoveDiet();
        }
        this.Diet = diet;
        if (!diet.GetUsers().Contains(this))
        {
            diet.AddUser(this);
        }
    }
    public bool RemoveDiet()
    {
        if (this.Diet == null) return false;

        Diet oldDiet = this.Diet;
        this.Diet = null;

        oldDiet.RemoveUser(this);
        return true;
    }
    public bool EditDiet(Diet newDiet)
    {
        if (newDiet == null) throw new ArgumentNullException(nameof(newDiet));
        RemoveDiet();
        SetDiet(newDiet);
        return true;
    }
    public List<User_Achievement> GetAchievements()
    {
        return new List<User_Achievement>(_achievements);
    }

    public void AddAchievement(Achievement achievement)
    {
        if (achievement == null) throw new ArgumentNullException();
        if (achievement.GetUsers().Any(a => a.GetUser() == this))
        {
            if (!_achievements.Any(a => a.GetAchievement() == achievement))
            {
                _achievements.Add(achievement.GetUsers().First(a => a.GetUser() == this));
            }
        }
        else
        {
            User_Achievement userAchievement = new User_Achievement(DateTime.Now);
            _achievements.Add(userAchievement);
            userAchievement.SetUser(this);
            userAchievement.SetAchievement(achievement);
        }
    }

    public bool RemoveAchievement(Achievement achievement)
    {
        User_Achievement userAchievement = _achievements.FirstOrDefault(a => a.GetAchievement() == achievement);
        bool res = _achievements.Remove(userAchievement);
        if (userAchievement != null && userAchievement.GetUser() != null && userAchievement.GetUser().Equals(this)) userAchievement.RemoveUser();
        return res;
    }

    public bool EditAchievement(Achievement oldAchievement, Achievement newAchievement)
    {
        User_Achievement oldUserAchievement = _achievements.FirstOrDefault(a => a.GetAchievement() == oldAchievement);
        if (!_achievements.Remove(oldUserAchievement))
        {
            return false;
        }
        oldUserAchievement.RemoveUser();

        User_Achievement newUserAchievement = new User_Achievement(DateTime.Now);
        _achievements.Add(newUserAchievement);
        newUserAchievement.SetUser(this);
        newUserAchievement.SetAchievement(newAchievement);
        return true;
    }

    public List<Meal> GetMeals()
    {
        return new List<Meal>(_meals);
    }

    public void AddMeal(Meal meal)
    {
        if (meal == null) throw new ArgumentNullException();
        if (!_meals.Contains(meal)) _meals.Add(meal);
        if (meal.GetUser() == null || !meal.GetUser().Equals(this)) meal.SetUser(this);
    }

    public bool RemoveMeal(Meal meal)
    {
        bool res = _meals.Remove(meal);
        if (meal.GetUser() != null && meal.GetUser().Equals(this)) meal.RemoveUser();
        return res;
    }

    public bool EditMeals(Meal oldMeal, Meal newMeal)
    {
        if (!_meals.Remove(oldMeal))
        {
            return false;
        }
        oldMeal.RemoveUser();

        _meals.Add(newMeal);
        newMeal.SetUser(this);
        return true;
    }
    public List<SleepTracker> GetSleepTrackers()
    {
        return new List<SleepTracker>(_sleepTrackers);
    }
    public void AddSleepTracker(SleepTracker sleepTracker)
    {
        if (sleepTracker == null) throw new ArgumentNullException();
        if (!_sleepTrackers.Contains(sleepTracker))
        {
            _sleepTrackers.Add(sleepTracker);
            if (sleepTracker.GetUser() == null || !sleepTracker.GetUser().Equals(this))
            {
                sleepTracker.SetUser(this);
            }
        }
    }
    public bool RemoveSleepTracker(SleepTracker sleepTracker)
    {
        if (_sleepTrackers.Remove(sleepTracker))
        {
            if (sleepTracker.GetUser() != null && sleepTracker.GetUser().Equals(this))
            {
                sleepTracker.RemoveUser();
            }
            return true;
        }
        return false;
    }
    public bool EditSleepTracker(SleepTracker oldTracker, SleepTracker newTracker)
    {
        if (oldTracker == null || newTracker == null)
        {
            throw new ArgumentNullException();
        }
        if (!_sleepTrackers.Remove(oldTracker))
        {
            return false;
        }
        oldTracker.RemoveUser();
        AddSleepTracker(newTracker);
        return true;
    }
    public HealthMetric GetHealthMetric()
    {
        return _healthMetric;
    }
    public void AddHealthMetric(HealthMetric healthMetric)
    {
        if (healthMetric == null) throw new ArgumentNullException();
        if (_healthMetric != null)
        {
            RemoveHealthMetric();
        }
        _healthMetric = healthMetric;
        if (healthMetric.GetUser() == null || !healthMetric.GetUser().Equals(this))
        {
            healthMetric.SetUser(this);
        }
    }
    public bool RemoveHealthMetric()
    {
        if (_healthMetric == null)
        {
            return false;
        }
        var metricToRemove = _healthMetric;
        _healthMetric = null;

        if (metricToRemove.GetUser() == this)
        {
            metricToRemove.RemoveUser();
        }
        return true;
    }
    public bool EditHealthMetric(HealthMetric newHealthMetric)
    {
        if (newHealthMetric == null)
        {
            throw new ArgumentNullException(nameof(newHealthMetric));
        }

        if (_healthMetric == null)
        {
            throw new InvalidOperationException("No health metric exists for this user. Add one instead.");
        }
        RemoveHealthMetric();
        AddHealthMetric(newHealthMetric);
        return true;
    }
    public List<FitnessData> GetFitnessDatas()
    {
        return new List<FitnessData>(_fitnessDatas);
    }

    public void AddFitnessData(FitnessData fitnessData)
    {
        if (fitnessData == null) throw new ArgumentNullException();
        if (!_fitnessDatas.Contains(fitnessData)) _fitnessDatas.Add(fitnessData);
        if (fitnessData.GetUser() == null || !fitnessData.GetUser().Equals(this)) fitnessData.SetUser(this);
    }

    public bool RemoveFitnessData(FitnessData fitnessData)
    {
        bool res = _fitnessDatas.Remove(fitnessData);
        if (fitnessData.GetUser() != null && fitnessData.GetUser().Equals(this)) fitnessData.RemoveUser();
        return res;
    }

    public bool EditFitnessData(FitnessData oldFitnessData, FitnessData newFitnessData)
    {
        if (!_fitnessDatas.Remove(oldFitnessData))
        {
            return false;
        }
        oldFitnessData.RemoveUser();

        _fitnessDatas.Add(newFitnessData);
        newFitnessData.SetUser(this);
        return true;
    }


    public static  List<User> GetAllInstances()
    {
        return new List<User>(_users);
    }


    public static void WriteClass()
    {
        WriteAllData();
    }

    public static void InitializeClass()
    {
        ReadAllData();
    }


    public User()
    {
        this.UserID = IDCounter++;
        UsersCount++;
    }

    public User(string name, DateTime dateOfBirth, float weight, float height, string username, string password)
    :base(name, dateOfBirth)
    {
        this.UserID = IDCounter++;
        UsersCount++;
        this.Weight = weight;
        this.Height = height;
        this.Username = username;
        this.Password = password;
        this._workouts = new List<User_Workout>();
        this.HealthMetrics = new List<HealthMetric>();
        this._sleepTrackers = new List<SleepTracker>();
        this._fitnessDatas = new List<FitnessData>();
        this._achievements = new List<User_Achievement>();
        this.Meals = new List<Meal>();
        this.Devices = new List<Device>();

        Add(this);
    }

    public User(string name,
        DateTime dateOfBirth,
        float weight, float height,
        string username, string password,
        List<Device> devices,
        List<FitnessData> fitnessData,
        List<HealthMetric> healthMetrics,
        List<SleepTracker> sleepTrackers,
        List<Meal> meals,
        List<Achievement> achievements,
        Diet? diet,
        List<Workout> workouts)
        : base(name, dateOfBirth)
    {
        this.UserID = IDCounter++;
        UsersCount++;
        this.Weight = weight;
        this.Height = height;
        this.Username = username;
        this.Password = password;
        AssociateWorkouts(workouts);
        AssociateHealthMetrics(healthMetrics);
        AssociateSleepTrackers(sleepTrackers);
        AssociateFitnessData(fitnessData);
        AssociateMeals(meals);
        AssociateAchievements(achievements);
        AssociateDiet(diet);
        AssociateDevices(devices);
        this.Meals = new List<Meal>();
        this.Devices = new List<Device>();

        Add(this);
    }
    private void AssociateDevices(List<Device> devices)
    {
        foreach (var device in devices)
        {
            AddDevice(device);
        }
    }


    private void AssociateWorkouts(List<Workout> workouts)
    {
        foreach (Workout workout in workouts)
        {
            AddWorkout(workout);
        }
    }

    private void AssociateDiet(Diet? diet)
    {
        if (diet == null)
        {
            return;
        }
        SetDiet(diet);
    }
    private void AssociateAchievements(List<Achievement> achievements)
    {
        foreach (Achievement achievement in achievements)
        {
            AddAchievement(achievement);
        }
    }
    private void AssociateHealthMetrics(List<HealthMetric> healthMetrics)
    {
        foreach (HealthMetric healthMetric in healthMetrics)
        {
            AddHealthMetric(healthMetric);
        }
    }
    private void AssociateSleepTrackers(List<SleepTracker> sleepTrackers)
    {
        foreach (SleepTracker sleepTracker in sleepTrackers)
        {
            AddSleepTracker(sleepTracker);
        }
    }

    private void AssociateFitnessData(List<FitnessData> fitnessData)
    {
        foreach (FitnessData fitness in fitnessData)
        {
            AddFitnessData(fitness);
        }
    }

    private void AssociateMeals(List<Meal> meals)
    {
        foreach (Meal meal in meals)
        {
            AddMeal(meal);
        }
    }
    private static void ReadAllData()
    {
        string jsonFilePath = "resources/users.json";

        if (!File.Exists(jsonFilePath) || string.IsNullOrWhiteSpace(File.ReadAllText(jsonFilePath)))
        {
            Console.WriteLine("The " + jsonFilePath + " file is empty");
            _users = new List<User>();
            return;
        }

        _users = JsonSerializer
            .Deserialize<List<User>>(File.ReadAllText("resources/users.json"));
    }

    private static void WriteAllData()
    {
        foreach (var VARIABLE in _users)
        {
            JsonService.SaveJsonFile(VARIABLE, "resources/users.json");
        }
    }
    private void Add(User instacne)
    {
        _users.Add(instacne);
    }

}
