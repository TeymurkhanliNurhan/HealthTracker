using System.Text.Json;
using System.Text.Json.Serialization;

namespace Health_tracker;

public class Achievement
{
    private static int IDCounter = 0;
    private static List<Achievement> _achievements = new List<Achievement>();
    [JsonInclude]
    private int AchievementID { get; set; }
    [JsonInclude]
    private string _title;
    [JsonIgnore]
    public string Title
    {
        get => _title;
        private set
        {
            if (string.IsNullOrEmpty(value))
            {
                throw new ArgumentException("Title cannot be null or empty.");
            }
            _title = value;
        }
    }
    [JsonInclude]
    private string _description;
    
    [JsonIgnore]
    public string Description
    {
        get => _description;
        private set
        {
            if (string.IsNullOrEmpty(value))
            {
                throw new ArgumentException("Description cannot be null or empty.");
            }
            _description = value;
        }
    }
    
    
    [JsonInclude]
    private List<User_Achievement> _users = new List<User_Achievement>();

    public List<User_Achievement> GetUsers()
    {
        return new List<User_Achievement>(_users);
    }
    
    public void AddUser(User user)
    {
        if (user == null) throw new ArgumentNullException();
        if (user.GetAchievements().Any(a => a.GetAchievement() == this))
        {
            if (!_users.Any(u => u.GetUser() == user))
            {
                _users.Add(user.GetAchievements().First(a => a.GetAchievement() == this));
            }
        }
        else
        {
            User_Achievement userAchievement = new User_Achievement(DateTime.Now);
            _users.Add(userAchievement);
            userAchievement.SetAchievement(this);
            userAchievement.SetUser(user);
        }
    }

    public bool RemoveUser(User user)
    {
        User_Achievement userAchievement = _users.FirstOrDefault(a => a.GetUser() == user);
        bool res = _users.Remove(userAchievement);
        if (userAchievement != null && userAchievement.GetAchievement() != null && userAchievement.GetAchievement().Equals(this)) userAchievement.RemoveAchievement();
        return res;
    }

    public bool EditUser(User oldUser, User newUser)
    {
        User_Achievement oldUserAchievement = _users.FirstOrDefault(a => a.GetUser() == oldUser);
        if (!_users.Remove(oldUserAchievement))
        {
            return false;
        }
        oldUserAchievement.RemoveUser();
        
        User_Achievement newUserAchievement = new User_Achievement(DateTime.Now);
        _users.Add(newUserAchievement);
        newUserAchievement.SetAchievement(this);
        newUserAchievement.SetUser(newUser);
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
    public Achievement()
    {
        AchievementID = IDCounter++;
    }

    public Achievement(string title, string description)
    {
        AchievementID = IDCounter++;
        Title = title;
        Description = description;
        
        AddAchievement(this);
    }

    public Achievement(string title, string description, List<User> users)
    {
        AchievementID = IDCounter++;
        Title = title;
        Description = description;
        AssociateUsers(users);
        
        AddAchievement(this);
    }
    
    private void AssociateUsers(List<User> users)
    {
        foreach (User user in users)
        {
            AddUser(user);
        }
    }

    public static List<Achievement> GetAllInstances()
    {
        return new List<Achievement>(_achievements);
    }

    public override string ToString()
    {
        return AchievementID + " - " + Title + " - " + Description;
    }
    private static void ReadAllData()
    {
        string jsonFilePath = "resources/achievements.json";
        
        if (!File.Exists(jsonFilePath) || string.IsNullOrWhiteSpace(File.ReadAllText(jsonFilePath)))
        {
            Console.WriteLine("The " + jsonFilePath + " file is empty");
            _achievements = new List<Achievement>();
            return;
        }

        
        _achievements = JsonSerializer
            .Deserialize<List<Achievement>>(File.ReadAllText("resources/achievements.json"));
    }

    private static void WriteAllData()
    {
        foreach (var VARIABLE in _achievements)
        {
            JsonService.SaveJsonFile(VARIABLE, "resources/achievements.json");
        }
    }
    

    private void AddAchievement(Achievement achievement)
    {
        _achievements.Add(achievement);
    }
}
