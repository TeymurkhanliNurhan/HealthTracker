namespace Health_tracker;

public class User_Achievement
{
    private DateTime _dateAchieved;
    public DateTime DateAchieved
    {
        get => _dateAchieved;
        private set
        {
            if (value > DateTime.Now)
            {
                throw new ArgumentException("Date Achieved cannot be in the future.");
            }
            _dateAchieved = value;
        }
    }
    
    private User _user;
    
    private Achievement _achievement;

    public User GetUser()
    {
        return _user;
    }

    public Achievement GetAchievement()
    {
        return _achievement;
    }

    public void SetUser(User user)
    {
        if (user == null) throw new ArgumentNullException(nameof(user));
        _user = user;
        if (!_user.GetAchievements().Contains(this)) _user.AddAchievement(_achievement);
    }

    public void RemoveUser()
    {
        _achievement.RemoveUser(_user);
        _user = null;
        _achievement = null;
    }

    public void SetAchievement(Achievement achievement)
    {
        if (achievement == null) throw new ArgumentNullException(nameof(achievement));
        _achievement = achievement;
        if (!_achievement.GetUsers().Contains(this)) _achievement.AddUser(_user);
    }

    public void RemoveAchievement()
    {
        _user.RemoveAchievement(_achievement);
        _achievement = null;
        _user = null;
    }

    protected User_Achievement()
    {
        DateAchieved = DateTime.Now;
    }
    
    public User_Achievement(DateTime dateAchieved)
    {
        this.DateAchieved = dateAchieved;
    }
}