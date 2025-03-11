using System.Text.Json;
using System.Text.Json.Serialization;

namespace Health_tracker;

public class Doctor : Staff
{
    [JsonInclude]
    private Specialization Specialization { get; set; }
    [JsonInclude]
    private List<Diet> Diets { get; set; }
    private static List<Doctor> _doctors = new List<Doctor>();

    public static List<Doctor> GetAllInstances()
    {
        return new List<Doctor>(_doctors);
    }
    public int GetDietCount()
    {
        return Diets.Count;
    }

    public bool HasDiet(Diet diet)
    {
        return Diets.Contains(diet);
    }

    public static void WriteClass()
    {
        WriteAllData();
    }

    public static void InitializeClass()
    {
        ReadAllData();
    }


    public Doctor(string Name, DateTime DateOfBirth, Specialization Specialization)
    :base(Name, DateOfBirth)
    {
        this.Specialization = Specialization;
        this.Diets = new List<Diet>();

        Add(this);
    }

    public Doctor(string Name, DateTime DateOfBirth, Specialization Specialization, List<Diet> diets)
        : base(Name, DateOfBirth)
    {
        this.Specialization = Specialization;
        this.Diets = new List<Diet>();
        if (diets != null)
        {
            foreach (var diet in diets)
            {
                AddDiet(diet);
            }
        }
        Add(this);
    }
    public IReadOnlyList<Diet> GetDiets()
    {
        return Diets.AsReadOnly();
    }
    public void AddDiet(Diet diet)
    {
        if (diet == null)
        {
            throw new ArgumentNullException(nameof(diet), "Diet cannot be null.");
        }
        if (!Diets.Contains(diet))
        {
            Diets.Add(diet);
            diet.SetDoctor(this);
        }
    }
    public bool RemoveDiet(Diet diet)
    {
        if (diet == null)
        {
            return false;
        }
        if (Diets.Remove(diet))
        {
            diet.RemoveDoctor();
            return true;
        }
        return false;
    }
    public bool EditDiet(Diet oldDiet, Diet newDiet)
    {
        if (oldDiet == null || newDiet == null)
        {
            return false;
        }
        if (Diets.Remove(oldDiet))
        {
            oldDiet.RemoveDoctor();
            Diets.Add(newDiet);
            newDiet.SetDoctor(this);
            return true;
        }
        return false;
    }
    private static void ReadAllData()
    {
        string jsonFilePath = "resources/doctors.json";

        if (!File.Exists(jsonFilePath) || string.IsNullOrWhiteSpace(File.ReadAllText(jsonFilePath)))
        {
            Console.WriteLine("The " + jsonFilePath + " file is empty");
            _doctors = new List<Doctor>();
            return;
        }

        _doctors = JsonSerializer
            .Deserialize<List<Doctor>>(File.ReadAllText("resources/doctors.json"));
    }

    private static void WriteAllData()
    {
        foreach (var VARIABLE in _doctors)
        {
            JsonService.SaveJsonFile(VARIABLE, "resources/doctors.json");
        }
    }
    private void Add(Doctor instacne)
    {
        _doctors.Add(instacne);
    }

}