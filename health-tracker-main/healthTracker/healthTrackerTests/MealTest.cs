using Health_tracker;

namespace health_tracker_tests;

[TestFixture]
public class MealTest
{
    private const string ValidDishName1 = "naem1";
    private const string ValidDishName2 = "name2";
    private const int ValidCalories1 = 500;
    private const int ValidCalories2 = 300;
    private const string InvalidDishName = null;
    private const string EmptyDishName = "";
    private const int InvalidNegativeCalories = -1000;

    [Test]
    public void Should_Correctly_Create_Meal()
    {
        var meal = new Meal(ValidDishName1, ValidCalories1);

        Assert.That(meal.Dish, Is.EqualTo(ValidDishName1));
        Assert.That(meal.Calories, Is.EqualTo(ValidCalories1));
    }

    [Test]
    public void Should_Throw_Exception_Invalid_Or_Empty_Dish_Name()
    {
        Assert.Throws<ArgumentException>(() => new Meal(InvalidDishName, ValidCalories1));
        Assert.Throws<ArgumentException>(() => new Meal(EmptyDishName, ValidCalories1));
    }
    
    [Test]
    public void Should_Throw_Exception_Negative_Calories()
    {
        Assert.Throws<ArgumentException>(() => new Meal(ValidDishName1, InvalidNegativeCalories));
    }

    [Test]
    public void Should_Correctly_Return_All_Instances()
    {
        var meal1 = new Meal(ValidDishName1, ValidCalories1);
        var meal2 = new Meal(ValidDishName2, ValidCalories2);
        var allMeals = Meal.GetAllInstances();

        Assert.Contains(meal1, allMeals);
        Assert.Contains(meal2, allMeals);
    }

    [Test]
    public void Should_Correctly_Initialize_And_Write_Data()
    {
        Meal.InitializeClass();
        Meal.WriteClass();

        Assert.NotNull(Meal.GetAllInstances());
    }
}