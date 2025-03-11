using Health_tracker;

namespace health_tracker_tests;

[TestFixture]
public class SleepTrackerTest
{

    private const int ValidSleepHours1 = 8;
    private const int ValidSleepHours2 = 9;
    private const int InvalidSleepHours = -100;
    private static readonly Quality ValidQuality1 = Quality.Low;
    private static readonly Quality ValidQuality2 = Quality.Medium;
    private static readonly DateTime ValidDate1 = DateTime.Now;
    private static readonly DateTime ValidDate2 = DateTime.Now.AddDays(-1);

    [Test]
    public void Should_Correctly_Create_SleepTracker()
    {
        var sleepTracker = new SleepTracker(ValidDate1, ValidSleepHours1, ValidQuality1);

        Assert.That(sleepTracker.SleepHours, Is.EqualTo(ValidSleepHours1));
    }

    [Test]
    public void Should_Throw_Exception_Negative_SleepHours()
    {
        Assert.Throws<ArgumentException>(() => new SleepTracker(ValidDate1, InvalidSleepHours, ValidQuality1));
    }

    [Test]
    public void Should_Correctly_Return_All_Instances()
    {
        var sleepTracker1 = new SleepTracker(ValidDate1, ValidSleepHours1, ValidQuality1);
        var sleepTracker2 = new SleepTracker(ValidDate2, ValidSleepHours2, ValidQuality2);
        var allSleepTrackers = SleepTracker.GetAllInstances();

        Assert.Contains(sleepTracker1, allSleepTrackers);
        Assert.Contains(sleepTracker2, allSleepTrackers);
    }

    [Test]
    public void Should_Correctly_Initialize_And_Write_Data()
    {
        SleepTracker.InitializeClass();
        SleepTracker.WriteClass();

        Assert.NotNull(SleepTracker.GetAllInstances());
    }
}