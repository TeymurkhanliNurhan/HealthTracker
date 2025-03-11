using Health_tracker;

namespace health_tracker_tests;

[TestFixture]
public class WorkoutTest
{
    private const int ValidDuration1 = 60;
    private const int ValidDuration2 = 80;
    private const int InvalidDuration = -60;
    private const float ValidCalories1 = 500f;
    private const float ValidCalories2 = 5000f;
    private const float InvalidCalories = -500f;
    private const string ValidWorkoutName1 = "name1";
    private const string ValidWorkoutName2 = "name2";
    private const string EmptyWorkoutName = "";
    private const string InvalidWorkoutName = null;
    private Coach ValidCoach;
    private Exercise ValidExercise1;
    private Exercise ValidExercise2;
    private User ValidUser1;
    private User ValidUser2;
    private List<User> ValidUsers;

    [SetUp]
    public void Setup()
    {
        ValidCoach = new Coach("coach1", new DateTime(), ExperienceLevel.Novice);
        ValidExercise1 = new Exercise("name1", DifficultyLevel.Easy);
        ValidExercise2 = new Exercise("name2", DifficultyLevel.Easy);
        ValidUser1 = new User("user1", new DateTime(), 20, 20, "username1", "password1");
        ValidUser2 = new User("user2", new DateTime(), 10, 10, "username2", "password2");
        ValidUsers = new List<User> { ValidUser1, ValidUser2 };
    }
    
    [Test]
    public void Should_Correctly_Create_Workout()
    {
        var workout = new Workout(ValidDuration1, ValidCalories1, ValidWorkoutName1, ValidCoach);
    
        Assert.That(workout.Duration, Is.EqualTo(ValidDuration1));
        Assert.That(workout.Calories, Is.EqualTo(ValidCalories1));
        Assert.That(workout.WorkoutName, Is.EqualTo(ValidWorkoutName1));
    }
    
    [Test]
    public void Should_Throw_Exception_Null_Or_Invalid_WorkoutName()
    {
        Assert.Throws<ArgumentException>(() => new Workout(ValidDuration1, ValidCalories1, InvalidWorkoutName, ValidCoach));
        Assert.Throws<ArgumentException>(() => new Workout(ValidDuration1, ValidCalories1, EmptyWorkoutName, ValidCoach));
    }
    
    [Test]
    public void Should_Throw_Exception_Negative_Duration()
    {
        Assert.Throws<ArgumentException>(() => new Workout(InvalidDuration, ValidCalories1, ValidWorkoutName1, ValidCoach));
    }
    
    [Test]
    public void Should_Throw_Exception_Negative_Calories()
    {
        Assert.Throws<ArgumentException>(() => new Workout(ValidDuration1, InvalidCalories, ValidWorkoutName1, ValidCoach));
    }
    
    [Test]
    public void Should_Correctly_Return_All_Instances()
    {
        var workout1 = new Workout(ValidDuration1, ValidCalories1, ValidWorkoutName1, ValidCoach);
        var workout2 = new Workout(ValidDuration2, ValidCalories2, ValidWorkoutName2, ValidCoach);
        var allWorkouts = Workout.GetAllInstances();
    
        Assert.Contains(workout1, allWorkouts);
        Assert.Contains(workout2, allWorkouts);
    }
    
    [Test]
    public void Should_Correctly_Initialize_And_Write_Data()
    {
        Workout.InitializeClass();
        Workout.WriteClass();
    
        Assert.NotNull(Workout.GetAllInstances());
    }
    
    [Test]
    public void Should_Correctly_Create_Workout_With_Exercises()
    {
        Workout workout = new Workout(ValidDuration1, ValidCalories1, ValidWorkoutName1, ValidCoach, new List<User>(), new List<Exercise>() {ValidExercise1, ValidExercise2});
        
        Assert.Contains(ValidExercise1, workout.GetExercises());
        Assert.Contains(ValidExercise2, workout.GetExercises());
    }

    [Test]
    public void Should_Correctly_Delete_Exercise_From_Workout()
    {
        Workout workout = new Workout(ValidDuration1, ValidCalories1, ValidWorkoutName1, ValidCoach, new List<User>(), new List<Exercise>() {ValidExercise1, ValidExercise2});
        workout.RemoveExercise(ValidExercise1);
        
        Assert.IsFalse(workout.GetExercises().Contains(ValidExercise1));
        Assert.Contains(ValidExercise2, workout.GetExercises());
    }

    [Test]
    public void Should_Correctly_Edit_Exercise_From_Workout()
    {
        Workout workout = new Workout(ValidDuration1, ValidCalories1, ValidWorkoutName1, ValidCoach, new List<User>(), new List<Exercise>() {ValidExercise2});
        workout.EditExercises(ValidExercise2, ValidExercise1);
        
        Assert.IsFalse(workout.GetExercises().Contains(ValidExercise2));
        Assert.Contains(ValidExercise1, workout.GetExercises());
    }

    [Test]
    public void Should_Not_Edit_Exercise_From_Workout_If_Exercise_Does_Not_Exist()
    {
        Workout workout = new Workout(ValidDuration1, ValidCalories1, ValidWorkoutName1, ValidCoach, new List<User>(), new List<Exercise>());
        workout.EditExercises(ValidExercise2, ValidExercise1);
        
        Assert.IsFalse(workout.GetExercises().Contains(ValidExercise1));
        Assert.IsFalse(workout.GetExercises().Contains(ValidExercise1));    
    }
    
    [Test]
    public void Should_Correctly_Create_Workout_With_Users()
    {
        Workout workout = new Workout(5, 5, "workout1", ValidCoach, ValidUsers, new List<Exercise>());
        
        List<User> workoutUsers = new List<User>();
        foreach (User_Workout userWorkout in workout.GetUsers())
        {
            workoutUsers.Add(userWorkout.GetUser());
        }
        
        Assert.Contains(ValidUser1, workoutUsers);
        Assert.Contains(ValidUser2, workoutUsers);
    }

    [Test]
    public void Should_Correctly_Delete_User_From_Workout()
    {
        Workout workout = new Workout(5, 5, "workout1", ValidCoach, ValidUsers, new List<Exercise>());
        workout.RemoveUser(ValidUser1);
        
        List<User> workoutUsers = new List<User>();
        foreach (User_Workout userWorkout in workout.GetUsers())
        {
            workoutUsers.Add(userWorkout.GetUser());
        }
        
        Assert.IsFalse(workoutUsers.Contains(ValidUser1));
        Assert.Contains(ValidUser2, workoutUsers);
    }

    [Test]
    public void Should_Correctly_Edit_User_From_Workout()
    {
        Workout workout = new Workout(5, 5, "workout1", ValidCoach, ValidUsers, new List<Exercise>());
        workout.RemoveUser(ValidUser1);
        workout.EditUser(ValidUser2, ValidUser1);
        
        List<User> workoutUsers = new List<User>();
        foreach (User_Workout userWorkout in workout.GetUsers())
        {
            workoutUsers.Add(userWorkout.GetUser());
        }
        
        Assert.IsFalse(workoutUsers.Contains(ValidUser2));
        Assert.Contains(ValidUser1, workoutUsers);
    }

    [Test]
    public void Should_Not_Edit_User_From_Workout_If_User_Does_Not_Exist()
    {
        Workout workout = new Workout(5, 5, "workout1", ValidCoach, ValidUsers, new List<Exercise>());
        workout.RemoveUser(ValidUser1);
        workout.RemoveUser(ValidUser2);
        workout.EditUser(ValidUser2, ValidUser1);
        
        List<User> workoutUsers = new List<User>();
        foreach (User_Workout userWorkout in workout.GetUsers())
        {
            workoutUsers.Add(userWorkout.GetUser());
        }
        
        Assert.IsFalse(workoutUsers.Contains(ValidUser1));
        Assert.IsFalse(workoutUsers.Contains(ValidUser2));    
    }
}