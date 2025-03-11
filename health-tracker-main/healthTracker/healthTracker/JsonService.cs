using System.Text.Json;

namespace Health_tracker;

public class JsonService
{
    private static readonly JsonSerializerOptions JsonSerializerOptions = new JsonSerializerOptions()
    {
        IncludeFields = true
    };

    public static void InitializeClasses()
    {
        Achievement.InitializeClass();
        Coach.InitializeClass();
        Diet.InitializeClass();
        Doctor.InitializeClass();
        Exercise.InitializeClass();
        FitnessData.InitializeClass();
        HealthMetric.InitializeClass();
        Meal.InitializeClass();
        MobileDevice.InitializeClass();
        SleepTracker.InitializeClass();
        User.InitializeClass();
        WearableDevice.InitializeClass();
        Workout.InitializeClass();
    }

    public static void WriteClasses()
    {
        Achievement.WriteClass();
        Coach.WriteClass();
        Diet.WriteClass();
        Doctor.WriteClass();
        Exercise.WriteClass();
        FitnessData.WriteClass();
        HealthMetric.WriteClass();
        Meal.WriteClass();
        MobileDevice.WriteClass();
        SleepTracker.WriteClass();
        User.WriteClass();
        WearableDevice.WriteClass();
        Workout.WriteClass();
    }

    public static async Task SaveJsonFile<T>(T obj, string filePath)
    {
        try
        {
            if (obj == null)
            {
                Console.WriteLine("Cannot save a null object.");
                return;
            }
            
            List<T> existingEntities = await ReadJsonFile<T>(filePath) ?? new List<T>();
            existingEntities.Add(obj);
            
            string serializedFile = JsonSerializer.Serialize(existingEntities, JsonSerializerOptions);
            await File.WriteAllTextAsync(filePath, serializedFile);
        }
        catch (Exception ex)
        {
            Console.WriteLine("Something went wrong during file serialization. " + ex.Message);
        }
    }

    public static async Task<List<T>?> ReadJsonFile<T>(string filePath)
    {
        try
        {
            if (!File.Exists(filePath))
            {
                return new List<T>();
            }
            
            string readFile = await File.ReadAllTextAsync(filePath);
            
            if (string.IsNullOrWhiteSpace(readFile))
            {
                return new List<T>();
            }
            
            return JsonSerializer.Deserialize<List<T>>(readFile);
        }
        catch (Exception ex)
        {
            Console.WriteLine("Something went wrong during file deserialization. " + ex.Message);
            return null;
        }
    }
}