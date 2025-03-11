using System.Text.Json;
using System.Text.Json.Serialization;

namespace Health_tracker;

public class Coach : Staff
{
    [JsonInclude]
    private ExperienceLevel ExperienceLevel { get; set; }
    [JsonInclude]
    private List<Workout> Workouts { get; set; }
    [JsonInclude]
    private Coach SupervisingCoach { get; set; }
    [JsonIgnore]
    private List<Coach> SupervisedCoaches { get; set; } = new List<Coach>();
    private static List<Coach> _coaches = new List<Coach>();
    
    private List<Workout> _workouts = new List<Workout>();
    
    public List<Workout> GetWorkouts()
    {
        return new List<Workout>(_workouts);
    }

    public void AddWorkout(Workout workout)
    {
        if (workout == null) throw new ArgumentNullException();
        if (GetAllInstances().Exists(a => a.GetWorkouts().Contains(workout))) throw new ArgumentException("Workout already added to another coach");
        _workouts.Add(workout);
        if (workout.GetCoach() == null || !workout.GetCoach().Equals(this)) workout.SetCoach(this);
    }

    public bool RemoveWorkout(Workout workout)
    {
        bool res = _workouts.Remove(workout);
        if (workout.GetCoach() != null && workout.GetCoach().Equals(this)) workout.RemoveCoach();
        return res;
    }

    public bool EditWorkouts(Workout oldWorkout, Workout newWorkout)
    {
        if (!_workouts.Remove(oldWorkout))
        {
            return false;
        }
        oldWorkout.RemoveCoach();

        _workouts.Add(newWorkout);
        newWorkout.SetCoach(this);
        return true;
    }

    public void DeleteWorkouts()
    {
        foreach (Workout workout in _workouts)
        {
            workout.RemoveCoach();
            foreach (User_Workout userWorkout in workout.GetUsers())
            {
                User u = userWorkout.GetUser();
                workout.RemoveUser(u);
            }

            foreach (Exercise exercise in workout.GetExercises())
            {
                workout.RemoveExercise(exercise);
            }
        }
    }

    public static void WriteClass()
    {
        WriteAllData();
    }

    public static void InitializeClass()
    {
        ReadAllData();
    }


    public Coach(string Name, DateTime DateOfBirth, ExperienceLevel ExperienceLevel)
        : base(Name, DateOfBirth)
    {
        this.ExperienceLevel = ExperienceLevel;
        Add(this);
    }
    public Coach(string Name, DateTime DateOfBirth, List<Workout> workouts, ExperienceLevel ExperienceLevel, Coach supervisingCoach)
        : base(Name, DateOfBirth)
    {
        this.ExperienceLevel = ExperienceLevel;
        this.Workouts = workouts;
        this.SupervisingCoach = supervisingCoach;
        if (supervisingCoach != null)
        {
            supervisingCoach.AddSupervisedCoach(this);
        }
        Add(this);
    }
    public void AddSupervisedCoach(Coach coach)
    {
        if (!SupervisedCoaches.Contains(coach))
        {
            SupervisedCoaches.Add(coach);
        }
    }
    public void RemoveSupervisedCoach(Coach coach)
    {
        SupervisedCoaches.Remove(coach);
    }
    public List<Coach> GetSupervisedCoaches()
    {
        return new List<Coach>(SupervisedCoaches);
    }
    public Coach GetSupervisingCoach()
    {
        return SupervisingCoach;
    }
    public void SetSupervisingCoach(Coach supervisingCoach)
    {
        if (SupervisingCoach != null)
        {
            SupervisingCoach.RemoveSupervisedCoach(this);
        }
        SupervisingCoach = supervisingCoach;
        if (SupervisingCoach != null)
        {
            SupervisingCoach.AddSupervisedCoach(this);
        }
    }
    public void RemoveSupervisingCoach()
    {
        if (SupervisingCoach != null)
        {
            SupervisingCoach.RemoveSupervisedCoach(this);
        }
        SupervisingCoach = null;
    }
    public Coach()
    {

    }

    public static List<Coach> GetAllInstances()
    {
        return new List<Coach>(_coaches);
    }

    public override string ToString()
    {
        return "Name: " + Name + "\n DateOf Birth: " + DateOfBirth
               + "\n Age: "+ Age + "\n Expe:" + ExperienceLevel
               + "\n Workouts:" + string.Join("\n", Workouts.Select(workout => workout.ToString()));
    }
    private void Add(Coach instacne)
    {
        _coaches.Add(instacne);
    }
    private static void WriteAllData()
    {
        foreach (var VARIABLE in _coaches)
        {
            JsonService.SaveJsonFile(VARIABLE, "resources/coaches.json");
        }
    }
    private static void ReadAllData()
    {
        string jsonFilePath = "resources/coaches.json";

        if (!File.Exists(jsonFilePath) || string.IsNullOrWhiteSpace(File.ReadAllText(jsonFilePath)))
        {
            Console.WriteLine("The " + jsonFilePath + " file is empty");
            _coaches = new List<Coach>();
            return;
        }

        _coaches = JsonSerializer
            .Deserialize<List<Coach>>(File.ReadAllText("resources/coaches.json"));
    }
}