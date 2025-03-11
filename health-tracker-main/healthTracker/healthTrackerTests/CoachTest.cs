using Health_tracker;
using NUnit.Framework;

namespace health_tracker_tests;

[TestFixture]
public class CoachTest
{
    private const string ValidName1 = "name1";
    private const string ValidName2 = "name2";
    private const string InvalidName = null;
    private const string EmptyName = "";
    private static readonly DateTime ValidDateOfBirth1 = DateTime.Now.AddYears(-30);
    private static readonly DateTime ValidDateOfBirth2 = DateTime.Now.AddYears(-25);
    private const ExperienceLevel ValidExperienceLevel1 = ExperienceLevel.Expert;
    private const ExperienceLevel ValidExperienceLevel2 = ExperienceLevel.Medium;
    private static List<Workout> ValidWorkouts;
    private static Workout ValidWorkout1;
    private static Workout ValidWorkout2;


    [SetUp]
    public void SetUp()
    {
        ValidWorkout1 = new Workout(5, 5, "workout1", new Coach());
        ValidWorkout2 = new Workout(10, 10, "workout2", new Coach());
        ValidWorkouts = new List<Workout> { ValidWorkout1, ValidWorkout2 };
    }

    [Test]
    public void Should_Correctly_Create_Coach()
    {
        var coach = new Coach(ValidName1, ValidDateOfBirth1, ValidExperienceLevel1);

        Assert.That(coach.Name, Is.EqualTo(ValidName1));
        Assert.That(coach.DateOfBirth, Is.EqualTo(ValidDateOfBirth1));
    }

    [Test]
    public void Should_Throw_Exception_Name_Is_Null_Or_Empty()
    {
        Assert.Throws<ArgumentException>(() => new Coach(InvalidName, ValidDateOfBirth1, ValidExperienceLevel1));
        Assert.Throws<ArgumentException>(() => new Coach(EmptyName, ValidDateOfBirth1, ValidExperienceLevel1));
    }
    [Test]
    public void Should_Correctly_Create_Coach_With_Supervising_Coach()
    {
        var supervisingCoach = new Coach(ValidName1, ValidDateOfBirth1, ValidExperienceLevel1);
        var coach = new Coach(ValidName2, ValidDateOfBirth2, ValidWorkouts, ValidExperienceLevel2, supervisingCoach);
        Assert.That(coach.GetSupervisingCoach(), Is.EqualTo(supervisingCoach));
    }
    [Test]
    public void Should_Set_Supervising_Coach_Successfully()
    {
        var coach = new Coach(ValidName1, ValidDateOfBirth1, ValidExperienceLevel1);
        var supervisingCoach = new Coach(ValidName2, ValidDateOfBirth2, ValidExperienceLevel2);
        coach.SetSupervisingCoach(supervisingCoach);
        Assert.That(coach.GetSupervisingCoach(), Is.EqualTo(supervisingCoach));
    }
    [Test]
    public void Should_Remove_Supervising_Coach_Successfully()
    {
        var supervisingCoach = new Coach(ValidName1, ValidDateOfBirth1, ValidExperienceLevel1);
        var coach = new Coach(ValidName2, ValidDateOfBirth2, ValidWorkouts, ValidExperienceLevel2, supervisingCoach);
        coach.RemoveSupervisingCoach();
        Assert.That(coach.GetSupervisingCoach(), Is.Null);
    }
    [Test]
    public void Should_Get_Supervised_Coaches_Successfully()
    {
        var supervisingCoach = new Coach(ValidName1, ValidDateOfBirth1, ValidExperienceLevel1);
        var coach1 = new Coach(ValidName2, ValidDateOfBirth2, ValidWorkouts, ValidExperienceLevel2, supervisingCoach);
        var coach2 = new Coach(ValidName2, ValidDateOfBirth2, ValidWorkouts, ValidExperienceLevel2, supervisingCoach);
        Assert.That(supervisingCoach.GetSupervisedCoaches().Count, Is.EqualTo(2));
    }
    [Test]
    public void Should_Not_Add_Duplicate_Supervised_Coach()
    {
        var supervisingCoach = new Coach(ValidName1, ValidDateOfBirth1, ValidExperienceLevel1);
        var coach = new Coach(ValidName2, ValidDateOfBirth2, ValidWorkouts, ValidExperienceLevel2, supervisingCoach);
        supervisingCoach.AddSupervisedCoach(coach);
        Assert.That(supervisingCoach.GetSupervisedCoaches().Count, Is.EqualTo(1));
    }

    [Test]
    public void Should_Correctly_Return_All_Instances()
    {
        var coach1 = new Coach(ValidName1, ValidDateOfBirth1, ValidExperienceLevel1);
        var coach2 = new Coach(ValidName2, ValidDateOfBirth2, ValidExperienceLevel2);
        var allCoaches = Coach.GetAllInstances();

        Assert.Contains(coach1, allCoaches);
        Assert.Contains(coach2, allCoaches);
    }

    [Test]
    public void Should_Correctly_Initialize_And_Write_Data()
    {
        Coach.InitializeClass();
        Coach.WriteClass();

        Assert.NotNull(Coach.GetAllInstances());
    }

    [Test]
    public void Should_Not_Add_Workout_From_Another_Coach()
    {
        var coach1 = new Coach(ValidName1, ValidDateOfBirth1, ValidExperienceLevel1);
        var coach2 = new Coach(ValidName2, ValidDateOfBirth2, ValidExperienceLevel2);
        coach1.AddWorkout(ValidWorkout1);

        Assert.Throws<ArgumentException>(() => coach2.AddWorkout(ValidWorkout1));
    }
}