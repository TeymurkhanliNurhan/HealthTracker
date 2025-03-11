using Health_tracker;

namespace health_tracker_tests;

[TestFixture]
public class HealthMetricTest
{
    private const float ValidBloodPressure1 = 120.7f;
    private const float ValidBloodPressure2 = 110.3f;
    private const float ValidHeartRate1 = 75.0f;
    private const float ValidHeartRate2 = 65.0f;
    private const float InvalidNegativeBloodPressure = -1000f;
    private const float InvalidNegativeHeartRate = -1000f;

    [Test]
    public void Should_Correctly_Create_HealthMetric()
    {
        var healthMetric = new HealthMetric(ValidBloodPressure1, ValidHeartRate1);

        Assert.That(healthMetric.BloodPressure, Is.EqualTo(ValidBloodPressure1));
        Assert.That(healthMetric.HeartRate, Is.EqualTo(ValidHeartRate1));
    }
    
    [Test]
    public void Should_Throw_Exception_Negative_BloodPressure()
    {
        Assert.Throws<ArgumentException>(() => new HealthMetric(InvalidNegativeBloodPressure, ValidHeartRate1));
    }

    [Test]
    public void Should_Throw_Exception_Negative_HeartRate()
    {
        Assert.Throws<ArgumentException>(() => new HealthMetric(ValidBloodPressure1, InvalidNegativeHeartRate));
    }

    [Test]
    public void Should_Correctly_Return_All_Instances()
    {
        var healthMetric1 = new HealthMetric(ValidBloodPressure1, ValidHeartRate1);
        var healthMetric2 = new HealthMetric(ValidBloodPressure2, ValidHeartRate2);

        var allMetrics = HealthMetric.GetAllInstances();

        Assert.Contains(healthMetric1, allMetrics);
        Assert.Contains(healthMetric2, allMetrics);
    }

    [Test]
    public void Should_Correctly_Initialize_And_Write_Data()
    {
        HealthMetric.InitializeClass();
        HealthMetric.WriteClass();

        Assert.NotNull(HealthMetric.GetAllInstances());
    }
}