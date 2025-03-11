using Health_tracker;
//
// JsonService.InitializeClasses();
//
// Meal meal = new Meal("fish", 250);
//
// JsonService.SaveJsonFile(meal, "resources/meals.json");
//
// Doctor doctor = new Doctor("Dr. Smith", new DateTime(1980, 3, 20), Specialization.Dietitian);
// JsonService.SaveJsonFile(doctor, "resources/doctors.json");
//
// WearableDevice device = new WearableDevice("Wearable Device", "My Wearable");
// MobileDevice mobileDevice = new MobileDevice("Mobile device", "My Mobile device");
// User user = new User("Johny", new DateTime(1990, 5, 12), 75.5f, 180.2f, "johny", "password123", device);
// JsonService.SaveJsonFile(user, "resources/users.json");
//
// JsonService.SaveJsonFile(device, "resources/wearableDevices.json");
//
//
// Diet diet = new Diet(2000, new List<Meal> { meal });
// JsonService.SaveJsonFile(diet, "resources/diets.json");
//
//
//
// // JsonService.SaveJsonFile(exercise, "resources/exercises.json");
//
//
// FitnessData fitnessData = new FitnessData(DateTime.Now, 10000, 500, 7.5f);
// JsonService.SaveJsonFile(fitnessData, "resources/fitnessData.json");
//
// HealthMetric healthMetric = new HealthMetric(120.5f, 75.2f);
// JsonService.SaveJsonFile(healthMetric, "resources/healthMetrics.json");
// Achievement achievement = new Achievement("10K Steps", "Walked 10,000 steps in a day", DateTime.Now);
// JsonService.SaveJsonFile(achievement, "resources/achievements.json");
//
// SleepTracker sleepTracker = new SleepTracker(DateTime.Now, 8, Quality.High);
// JsonService.SaveJsonFile(sleepTracker, "resources/sleepTrackers.json");
//
//
//
// JsonService.SaveJsonFile(workout, "resources/workouts.json");
//
//
//
// JsonService.ReadJsonFile<Workout>("resources/workouts.json");
//
//
// Workout.InitializeClass();
JsonService.InitializeClasses();

//  Exercise exercise = new Exercise("Push Ups", DifficultyLevel.Medium);
//  Workout workout = new Workout(60, 300, "Morning Workout", new List<Exercise> { exercise });
//  Coach coach = new Coach("Petya", DateTime.Now, new List<Workout> { workout }, ExperienceLevel.Expert);


// User user = new User("user", DateTime.Now, 2, 2, "username", "password", new MobileDevice());
//
// FitnessData fitnessData = new FitnessData(DateTime.Now, 10000, 500, 7.5f, user);
// await JsonService.SaveJsonFile(coach, "resources/coaches.json");
// await JsonService.SaveJsonFile(user, "resources/users.json");
//
// Doctor doctor = new Doctor("Dr. Smith", new DateTime(1980, 3, 20), Specialization.Dietitian);
// await JsonService.SaveJsonFile(doctor, "resources/doctors.json");
// foreach (var VARIABLE in Coach.GetAllInstances())
// {
//     Console.WriteLine(VARIABLE);
// }
//
// User user = new User("user", DateTime.Now, 2, 2, "username", "password", new MobileDevice());
// User user2 = new User("user2", DateTime.Now, 2, 2, "username", "password", new MobileDevice());
// List<User> users = new List<User>() { user, user2 };
// Achievement achievement = new Achievement("10K Steps", "Walked 10,000 steps in a day", users);
// Achievement achievement2 = new Achievement("20K Steps", "Walked 20,000 steps in a day");
// user.AddAchievement(achievement);
// user.AddAchievement(achievement2);
// user2.AddAchievement(achievement);
// user2.EditAchievement(achievement, achievement2);
// user.GetAchievements().ForEach(a => Console.WriteLine(a.GetUser().Name + ": " + a.GetAchievement().Title));
// achievement.GetUsers().ForEach(a => Console.WriteLine("All users in ach " + a.GetUser().Name + ": " + a.GetAchievement().Title));
// achievement2.GetUsers().ForEach(a => Console.WriteLine("All users in ach2 " +a.GetUser().Name + ": " + a.GetAchievement().Title));

//
// user.GetAchievements().ForEach(a => Console.WriteLine("All ach in user: " + a.GetAchievement().Title));
// user2.GetAchievements().ForEach(a => Console.WriteLine("All ach in user2: " + a.GetAchievement().Title));

