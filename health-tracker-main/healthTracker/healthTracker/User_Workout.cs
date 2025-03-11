namespace Health_tracker;

public class User_Workout
{
    private DateTime _timeStarted;
    public DateTime TimeStarted
    {
        get => _timeStarted;
        private set
        {
            if (value > DateTime.Now)
            {
                throw new ArgumentException("Date Achieved cannot be in the future.");
            }
            _timeStarted = value;
        }
    }

    private Status _status;
    public Status Status
    {
        get => _status;
        set => _status = value;
    }
    
    private User _user;
    
    private Workout _workout;

    public User GetUser()
    {
        return _user;
    }

    public Workout GetWorkout()
    {
        return _workout;
    }

    public void SetUser(User user)
    {
        if (user == null) throw new ArgumentNullException(nameof(user));
        _user = user;
        if (!_user.GetWorkouts().Contains(this)) _user.AddWorkout(_workout);
    }

    public void RemoveUser()
    {
        _workout.RemoveUser(_user);
        _user = null;
        _workout = null;
    }

    public void SetWorkout(Workout workout)
    {
        if (workout == null) throw new ArgumentNullException(nameof(workout));
        _workout = workout;
        if (!_workout.GetUsers().Contains(this)) _workout.AddUser(_user);
    }

    public void RemoveWorkout()
    {
        _user.RemoveWorkout(_workout);
        _workout = null;
        _user = null;
    }
    
    public void PauseWorkout()
    {
        _status = Status.Paused;
    }

    public void ResumeWorkout()
    {
        _status = Status.Ongoing;
    }

    public void CancelWorkout()
    {
        _status = Status.Cancelled;
    }

    public void FinishWorkout()
    {
        _status = Status.Completed;
    }

    protected User_Workout()
    {
        _timeStarted = DateTime.Now;
        _status = Status.Ongoing;
    }

    public User_Workout(DateTime timeStarted)
    {
        _timeStarted = timeStarted;
        _status = Status.Ongoing;
    }
}