using System.Text.Json;
using System.Text.Json.Serialization;

namespace Health_tracker;

public class Exercise
{
    private static List<Exercise> _exercises = new List<Exercise>();
    private static int IDCounter = 0;
    
    [JsonInclude]
    private int ExerciseID { get; set; }
    [JsonInclude]
    private string _name;
    [JsonIgnore]
    public string Name
    {
        get => _name;
        private set
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentException("Name cannot be null or empty");
            }
            _name = value;
        }
    }
    [JsonInclude]
    private DifficultyLevel Difficulty { get; set; }
    [JsonInclude]
    private List<Workout> _workouts = new List<Workout>();
    
    public List<Workout> GetWorkouts()
    {
        return new List<Workout>(_workouts);
    }

    public void AddWorkout(Workout workout)
    {
        if (workout == null) throw new ArgumentNullException();
        if (!_workouts.Contains(workout)) _workouts.Add(workout);
        if (!workout.GetExercises().Contains(this)) workout.AddExercise(this);
    }

    public bool RemoveWorkout(Workout workout)
    {
        bool res = _workouts.Remove(workout);
        if (workout.GetExercises().Contains(this)) workout.RemoveExercise(this);
        return res;
    }

    public bool EditWorkouts(Workout oldWorkout, Workout newWorkout)
    {
        if (!_workouts.Remove(oldWorkout))
        {
            return false;
        }
        if (oldWorkout.GetExercises().Contains(this)) oldWorkout.RemoveExercise(this);

        _workouts.Add(newWorkout);
        newWorkout.AddExercise(this);
        return true;
    }

    
    public static  List<Exercise> GetAllInstances()
    {
        return new List<Exercise>(_exercises);
    }


    public static void WriteClass()
    {
        WriteAllData();
    }

    public static void InitializeClass()
    {
        ReadAllData();
    }
    

    public Exercise()
    {
        ExerciseID = IDCounter++;
    }

    public Exercise(string name, DifficultyLevel difficulty)
    {
        ExerciseID = IDCounter++;
        Name = name;
        Difficulty = difficulty;
        
        Add(this);
    }
    
    public Exercise(string name, DifficultyLevel difficulty, List<Workout> workouts)
    {
        ExerciseID = IDCounter++;
        Name = name;
        Difficulty = difficulty;
        AssociateWorkouts(workouts);
        
        Add(this);
    }
    
    private void AssociateWorkouts(List<Workout> workouts)
    {
        foreach (Workout workout in workouts)
        {
            AddWorkout(workout);
        }
    }

    public override string ToString()
    {
        return "Name: " + Name + ", Difficulty: " + Difficulty;
    }
    private void Add(Exercise instacne)
    {
        _exercises.Add(instacne);
    }
    private static void ReadAllData()
    {
        string jsonFilePath = "resources/exercises.json";
        
        if (!File.Exists(jsonFilePath) || string.IsNullOrWhiteSpace(File.ReadAllText(jsonFilePath)))
        {
            Console.WriteLine("The " + jsonFilePath + " file is empty");
            _exercises = new List<Exercise>();
            return;
        }
        
        _exercises = JsonSerializer
            .Deserialize<List<Exercise>>(File.ReadAllText("resources/exercises.json"));
    }
    
    private static void WriteAllData()
    {
        foreach (var VARIABLE in _exercises)
        {
            JsonService.SaveJsonFile(VARIABLE, "resources/exercises.json");
        }
    }
}