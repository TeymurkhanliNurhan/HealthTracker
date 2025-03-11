using Health_tracker;

namespace health_tracker_tests;

[TestFixture]
public class ExerciseTest
{
    private const string ValidName1 = "name1";
    private const string ValidName2 = "name2";
    private const string InvalidName = null;
    private const string EmptyName = "";
    private const DifficultyLevel ValidDifficulty1 = DifficultyLevel.Easy;
    private const DifficultyLevel ValidDifficulty2 = DifficultyLevel.Hard;
    private Exercise ValidExercise;
    private Workout ValidWorkout1;
    private Workout ValidWorkout2;
    private Coach ValidCoach;

    [SetUp]
    public void Setup()
    {
        ValidCoach = new Coach("coach1", new DateTime(), ExperienceLevel.Novice);
        ValidExercise = new Exercise("Push-ups", DifficultyLevel.Medium);
        ValidWorkout1 = new Workout(30, 200, "Morning Routine", ValidCoach);
        ValidWorkout2 = new Workout(45, 350, "Evening Routine", ValidCoach);
    }
    
    [Test]
    public void Should_Correctly_Create_Exercise()
    {
        var exercise = new Exercise(ValidName1, ValidDifficulty1);

        Assert.That(exercise.Name, Is.EqualTo(ValidName1));
    }

    [Test]
    public void Should_Throw_Exception_Name_Is_Null_Or_Empty()
    {
        Assert.Throws<ArgumentException>(() => new Exercise(InvalidName, ValidDifficulty1));
        Assert.Throws<ArgumentException>(() => new Exercise(EmptyName, ValidDifficulty1));
    }

    [Test]
    public void Should_Correctly_Return_All_Instances()
    {
        var exercise1 = new Exercise(ValidName1, ValidDifficulty1);
        var exercise2 = new Exercise(ValidName2, ValidDifficulty2);

        var allExercises = Exercise.GetAllInstances();

        Assert.Contains(exercise1, allExercises);
        Assert.Contains(exercise2, allExercises);
    }

    [Test]
    public void Should_Correctly_Initialize_And_Write_Data()
    {
        Exercise.InitializeClass();
        Exercise.WriteClass();

        Assert.NotNull(Exercise.GetAllInstances());
    }
    
    [Test]
    public void Should_Correctly_Add_Workout_To_Exercise()
    { 
        ValidExercise.AddWorkout(ValidWorkout1);

        var workouts = ValidExercise.GetWorkouts();

        Assert.Contains(ValidWorkout1, workouts);
        Assert.Contains(ValidExercise, ValidWorkout1.GetExercises());
    }

    [Test]
    public void Should_Not_Add_Workout_To_Exercise_If_Workout_Already_Exists()
    {
        ValidExercise.AddWorkout(ValidWorkout1);
        ValidExercise.AddWorkout(ValidWorkout1);

        var workouts = ValidExercise.GetWorkouts();

        Assert.That(workouts.Count, Is.EqualTo(1));
    }

    [Test]
    public void Should_Correctly_Remove_Workout_From_Exercise()
    {
        ValidExercise.AddWorkout(ValidWorkout1);
        bool result = ValidExercise.RemoveWorkout(ValidWorkout1);

        var workouts = ValidExercise.GetWorkouts();

        Assert.IsTrue(result);
        Assert.IsFalse(workouts.Contains(ValidWorkout1));
        Assert.IsFalse(ValidWorkout1.GetExercises().Contains(ValidExercise));
    }

    [Test]
    public void Should_Correctly_Edit_Workout_From_Exercise()
    {
        ValidExercise.AddWorkout(ValidWorkout1);

        bool result = ValidExercise.EditWorkouts(ValidWorkout1, ValidWorkout2);

        var workouts = ValidExercise.GetWorkouts();

        Assert.IsTrue(result);
        Assert.Contains(ValidWorkout2, workouts);
        Assert.IsFalse(workouts.Contains(ValidWorkout1));
        Assert.IsTrue(ValidWorkout2.GetExercises().Contains(ValidExercise));
        Assert.IsFalse(ValidWorkout1.GetExercises().Contains(ValidExercise));
    }

    [Test]
    public void Should_Not_Remove_Workout_From_Exercise_If_Workout_Does_Not_Exist()
    {
        bool result = ValidExercise.EditWorkouts(ValidWorkout1, ValidWorkout2);

        Assert.IsFalse(result);
    }
}