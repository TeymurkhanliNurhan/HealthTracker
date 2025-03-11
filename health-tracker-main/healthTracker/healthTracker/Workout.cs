using System.Text.Json;
using System.Text.Json.Serialization;

namespace Health_tracker;

public class Workout
{
    private static int IDCounter = 0;
    private static List<Workout> _workouts = new List<Workout>();
    [JsonInclude]
    private int WorkoutID { get; set; }
    [JsonInclude]
    private int _duration;
    [JsonIgnore]
    public int Duration
    {
        get => _duration;
        private set
        {
            if (value <= 0)
            {
                throw new ArgumentException("Duration must be positive.");
            }
            _duration = value;
        }
    }
    [JsonInclude]
    private float _calories;
    [JsonIgnore]
    public float Calories
    {
        get => _calories;
        private set
        {
            if (value < 0)
            {
                throw new ArgumentException("Calories cannot be negative.");
            }
            _calories = value;
        }
    }
    [JsonInclude]
    private string _workoutName;
    [JsonIgnore]
    public string WorkoutName
    {
        get => _workoutName;
        private set
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentException("Workout name cannot be null or empty.");
            }
            _workoutName = value;
        }
    }
    [JsonInclude]
    private List<Exercise> _exercises = new List<Exercise>();
    
    [JsonInclude]
    private List<User_Workout> _users = new List<User_Workout>();
    
    [JsonInclude]
    private Coach _coach;
    
    public List<Exercise> GetExercises()
    {
        return new List<Exercise>(_exercises);
    }

    public void AddExercise(Exercise exercise)
    {
        if (exercise == null) throw new ArgumentNullException();
        if (!_exercises.Contains(exercise)) _exercises.Add(exercise);
        if (!exercise.GetWorkouts().Contains(this)) exercise.AddWorkout(this);
    }

    public bool RemoveExercise(Exercise exercise)
    {
        bool res = _exercises.Remove(exercise);
        if (exercise.GetWorkouts().Contains(this)) exercise.RemoveWorkout(this);
        return res;
    }

    public bool EditExercises(Exercise oldExercise, Exercise newExercise)
    {
        if (!_exercises.Remove(oldExercise))
        {
            return false;
        }
        if (oldExercise.GetWorkouts().Contains(this)) oldExercise.RemoveWorkout(this);

        _exercises.Add(newExercise);
        newExercise.AddWorkout(this);
        return true;
    }

    public Coach GetCoach()
    {
        return _coach;
    }

    public void SetCoach(Coach coach)
    {
        if (coach == null) throw new ArgumentNullException();
        if (_coach != null) _coach.RemoveWorkout(this);
        _coach = coach;
        if (!_coach.GetWorkouts().Contains(this)) _coach.AddWorkout(this);
    }

    public void RemoveCoach()
    {
        if (_coach.GetWorkouts().Contains(this)) _coach.RemoveWorkout(this);
        _coach = null;
    }

    public List<User_Workout> GetUsers()
    {
        return new List<User_Workout>(_users);
    }
    
    public void AddUser(User user)
    {
        if (user == null) throw new ArgumentNullException();
        if (user.GetWorkouts().Any(a => a.GetWorkout() == this))
        {
            if (!_users.Any(u => u.GetUser() == user))
            {
                _users.Add(user.GetWorkouts().First(a => a.GetWorkout() == this));
            }
        }
        else
        {
            User_Workout userWorkout = new User_Workout(DateTime.Now);
            _users.Add(userWorkout);
            userWorkout.SetWorkout(this);
            userWorkout.SetUser(user);
        }
    }

    public bool RemoveUser(User user)
    {
        User_Workout userWorkout = _users.FirstOrDefault(a => a.GetUser() == user);
        bool res = _users.Remove(userWorkout);
        if (userWorkout != null && userWorkout.GetWorkout() != null && userWorkout.GetWorkout().Equals(this)) userWorkout.RemoveWorkout();
        return res;
    }

    public bool EditUser(User oldUser, User newUser)
    {
        User_Workout oldUserWorkout = _users.FirstOrDefault(a => a.GetUser() == oldUser);
        if (!_users.Remove(oldUserWorkout))
        {
            return false;
        }
        oldUserWorkout.RemoveUser();
        
        User_Workout newUserWorkout = new User_Workout(DateTime.Now);
        _users.Add(newUserWorkout);
        newUserWorkout.SetWorkout(this);
        newUserWorkout.SetUser(newUser);
        return true;
    }


    public static void WriteClass()
    {
        WriteAllData();
    }

    public static void InitializeClass()
    {
        ReadAllData();
    }
    
    public static  List<Workout> GetAllInstances()
    {
        return new List<Workout>(_workouts);
    }

    public static void SaveWorkout(Workout workout)
    {
        if (workout == null)
        {
            throw new ArgumentException(nameof(workout));
        }
    }
    protected Workout()
    {
        WorkoutID = IDCounter++;
    }

    public Workout(int duration, float calories, string workoutName, Coach coach)
    {
        WorkoutID = IDCounter++;
        Duration = duration;
        Calories = calories;
        WorkoutName = workoutName;
        SetCoach(coach);
        
        Add(this);
    }
    
    public Workout(int duration, float calories, string workoutName, Coach coach, List<User> users, List<Exercise> exercises)
    {
        WorkoutID = IDCounter++;
        Duration = duration;
        Calories = calories;
        WorkoutName = workoutName;
        SetCoach(coach);
        AssociateUsers(users);
        AssociateExercises(exercises);
        
        Add(this);
    }
    
    private void AssociateExercises(List<Exercise> exercises)
    {
        foreach (Exercise exercise in exercises)
        {
            AddExercise(exercise);
        }
    }
    
    private void AssociateUsers(List<User> users)
    {
        foreach (User user in users)
        {
            AddUser(user);
        }
    }

    public override string ToString()
    {
        return "Name: " + WorkoutName + "\n Calories: " + Calories + "\n Duration: " + Duration + "\n Exercises: " + string.Join("\n", _exercises.Select(workout => workout.ToString()));
    }
    private void Add(Workout instacne)
    {
        _workouts.Add(instacne);
    }
    private static void ReadAllData()
    {
        string jsonFilePath = "resources/workouts.json";
        
        if (!File.Exists(jsonFilePath) || string.IsNullOrWhiteSpace(File.ReadAllText(jsonFilePath)))
        {
            Console.WriteLine("The " + jsonFilePath + " file is empty");
            _workouts = new List<Workout>();
            return;
        }
        
        _workouts = JsonSerializer
            .Deserialize<List<Workout>>(File.ReadAllText("resources/workouts.json"));
    }
    
    private static void WriteAllData()
    {
        foreach (var VARIABLE in _workouts)
        {
            JsonService.SaveJsonFile(VARIABLE, "resources/workouts.json");
        }
    }
}