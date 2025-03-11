using Health_tracker;

namespace health_tracker_tests
{
    public class JsonServiceTests
    {
        // private const string TestFilePath = "test.json";
        //
        // [SetUp]
        // public void Setup()
        // {
        //     if (File.Exists(TestFilePath))
        //     {
        //         File.Delete(TestFilePath);
        //     }
        // }
        //
        // [Test]
        // public async Task SaveJsonFile_ShouldSaveObjectToFile()
        // {
        //     var testObject = new User("Test User", DateTime.Now, 2, 2, "user", "null", new MobileDevice());
        //     await JsonService.SaveJsonFile(testObject, TestFilePath);
        //
        //     var result = await JsonService.ReadJsonFile<User>(TestFilePath);
        //     Assert.IsNotNull(result);
        //     Assert.That(result.Count, Is.EqualTo(1));
        //     Assert.That(result[0].Name, Is.EqualTo("Test User"));
        // }
        //
        // [Test]
        // public async Task ReadJsonFile_ShouldReturnEmptyListWhenFileIsEmpty()
        // {
        //     var result = await JsonService.ReadJsonFile<User>(TestFilePath);
        //     Assert.IsNotNull(result);
        //     Assert.That(result.Count, Is.EqualTo(0));
        // }
        //
        // [Test]
        // public async Task ReadJsonFile_ShouldReturnEmptyListWhenFileDoesNotExist()
        // {
        //     var result = await JsonService.ReadJsonFile<User>("non_existent_file.json");
        //     Assert.IsNotNull(result);
        //     Assert.That(result.Count, Is.EqualTo(0));
        // }
        //
        // [Test]
        // public async Task SaveJsonFile_ShouldHandleNullObjectGracefully()
        // {
        //     await JsonService.SaveJsonFile<User>(null, TestFilePath);
        //     var result = await JsonService.ReadJsonFile<User>(TestFilePath);
        //     Assert.IsNotNull(result);
        //     Assert.That(result.Count, Is.EqualTo(0));
        // }
        //
        // [Test]
        // public async Task ReadJsonFile_ShouldHandleMalformedJsonGracefully()
        // {
        //     await File.WriteAllTextAsync(TestFilePath, "{malformed json}");
        //     var result = await JsonService.ReadJsonFile<User>(TestFilePath);
        //     Assert.IsNull(result);
        // }
        //
        // [Test]
        // public async Task SaveJsonFile_ShouldSaveAndReadMultipleObjects()
        // {
        //     var testObject1 = new User("User A", DateTime.Now, 2, 2, "user", "null", new MobileDevice());
        //     var testObject2 = new User("User B", DateTime.Now, 2, 2, "user", "null", new MobileDevice());
        //
        //     await JsonService.SaveJsonFile(testObject1, TestFilePath);
        //     await JsonService.SaveJsonFile(testObject2, TestFilePath);
        //
        //     var result = await JsonService.ReadJsonFile<User>(TestFilePath);
        //     Assert.IsNotNull(result);
        //     Assert.That(result.Count, Is.EqualTo(2));
        // }
        //
        // [Test]
        // public async Task ReadJsonFile_ShouldReturnCorrectNumberOfObjects()
        // {
        //     var testObject1 = new User("User A", DateTime.Now, 2, 2, "user", "null", new MobileDevice());
        //     var testObject2 = new User("User B", DateTime.Now, 2, 2, "user", "null", new MobileDevice());
        //
        //     await JsonService.SaveJsonFile(testObject1, TestFilePath);
        //     await JsonService.SaveJsonFile(testObject2, TestFilePath);
        //
        //     var result = await JsonService.ReadJsonFile<User>(TestFilePath);
        //     Assert.IsNotNull(result);
        //     Assert.That(result.Count, Is.EqualTo(2));
        // }
        //
        // [Test]
        // public async Task SaveJsonFile_ShouldOverwriteFileContentWhenSameObjectIsSaved()
        // {
        //     var testObject = new User("User C", DateTime.Now, 2, 2, "user", "null", new MobileDevice());
        //
        //     await JsonService.SaveJsonFile(testObject, TestFilePath);
        //     await JsonService.SaveJsonFile(testObject, TestFilePath);
        //
        //     var result = await JsonService.ReadJsonFile<User>(TestFilePath);
        //     Assert.IsNotNull(result);
        //     Assert.That(result.Count, Is.EqualTo(2));
        // }
        //
        // [Test]
        // public async Task ReadJsonFile_ShouldReturnCorrectObjectWhenSaved()
        // {
        //     var testObject = new User("User D", DateTime.Now, 2, 2, "user", "null", new MobileDevice());
        //
        //     await JsonService.SaveJsonFile(testObject, TestFilePath);
        //     var result = await JsonService.ReadJsonFile<User>(TestFilePath);
        //
        //     Assert.IsNotNull(result);
        //     Assert.That(result.Count, Is.EqualTo(1));
        //     Assert.That(result[0].Name, Is.EqualTo("User D"));
        // }
        //
        // [Test]
        // public async Task SaveAndReadJsonFile_ShouldReturnCorrectInformation()
        // {
        //     var testObject = new User("Test User", DateTime.Now, 2, 2, "user", "null", new MobileDevice());
        //     await JsonService.SaveJsonFile(testObject, TestFilePath);
        //
        //     var result = await JsonService.ReadJsonFile<User>(TestFilePath);
        //
        //     Assert.IsNotNull(result);
        //     Assert.That(result.Count, Is.EqualTo(1));
        //     Assert.That(result[0].Name, Is.EqualTo("Test User"));
        // }
        //
        // [Test]
        // public void ShouldThrowExceptionOnNullOrEmptyString()
        // {
        //     Assert.Throws<ArgumentException>(() => new User(null, DateTime.Now, 2, 2, "", null, new MobileDevice()));
        // }
        //
        // [Test]
        // public void ShouldThrowExceptionOnNegativeNumber()
        // {
        //     Assert.Throws<ArgumentException>(() => new User("null", DateTime.Now, -1, -1, "null", "null", new MobileDevice()));
        // }
        //
        // [Test]
        // public async Task Encapsulation_PrincipleShouldBeMaintained()
        // {
        //     var originalUser = new User("Original Name", DateTime.Now, 2, 2, "user", "null", new MobileDevice());
        //
        //     await JsonService.SaveJsonFile(originalUser, TestFilePath);
        //
        //     // originalUser.Name = "Modified Name";
        //
        //     var result = await JsonService.ReadJsonFile<User>(TestFilePath);
        //     Assert.That(result[0].Name, Is.EqualTo("Original Name"));
        // }
        //
        // [Test]
        // public async Task Extent_PersistencyAcrossRuns()
        // {
        //     var testObject = new User("Persistent User", DateTime.Now, 2, 2, "user", "null", new MobileDevice());
        //     await JsonService.SaveJsonFile(testObject, TestFilePath);
        //
        //     var resultAfterFirstRun = await JsonService.ReadJsonFile<User>(TestFilePath);
        //     Assert.IsNotNull(resultAfterFirstRun);
        //     Assert.That(resultAfterFirstRun.Count, Is.EqualTo(1));
        //
        //     var resultAfterSecondRun = await JsonService.ReadJsonFile<User>(TestFilePath);
        //     Assert.IsNotNull(resultAfterSecondRun);
        //     Assert.That(resultAfterSecondRun.Count, Is.EqualTo(1));
        //     Assert.That(resultAfterSecondRun[0].Name, Is.EqualTo("Persistent User"));
        // }
        //
        // [TearDown]
        // public void Cleanup()
        // {
        //     if (File.Exists(TestFilePath))
        //     {
        //         File.Delete(TestFilePath);
        //     }
        // }
        
        
    }
}