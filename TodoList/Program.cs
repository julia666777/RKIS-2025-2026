
namespace TodoList;
public class Program 
{

	public static void Main(string[] args)
    {
		Console.WriteLine("Работу выполнили Чернова Юлия и Соловьев Иван 3833");

		InitializeUserData();
		InitializeTasks();

		while (true)
        {
            var commandLine = Console.ReadLine();
			var command = CommandParser.Parse(commandLine);
			command.Execute();
			FileManager.SaveData(FileManager.ProfileInfoPath, FileManager.TodolistPath);
		}
    }

	private static void GetUserAge()
	{
		Console.WriteLine("Введите год рождения:");
		string birthYearString = Console.ReadLine();

		bool isBirthYearValid = int.TryParse(birthYearString, out int birthYear);

		if (isBirthYearValid)
		{
			AppInfo.CurrentProfile.BirthYear = birthYear;
			return; 
		}
		else
		{
			Console.WriteLine("Ошибка: Некорректный год рождения, пожалуйста, введите целое число.");
			GetUserAge();
		}
	}

    // Получение данных пользователя и их обработка 
    private static void InitializeUserData()
    {
		AppInfo.CurrentProfile = FileManager.LoadProfile(FileManager.ProfileInfoPath);

		if (AppInfo.CurrentProfile == null)
		{
			AppInfo.CurrentProfile = new Profile();

			Console.WriteLine("Введите имя:");
			AppInfo.CurrentProfile.FirstName = Console.ReadLine();

			Console.WriteLine("Введите фамилию:");
			AppInfo.CurrentProfile.LastName = Console.ReadLine();

			GetUserAge();

			int currentYear = DateTime.Now.Year;
			int age = currentYear - AppInfo.CurrentProfile.BirthYear;

			FileManager.SaveProfile(FileManager.ProfileInfoPath);

			Console.WriteLine($"Добавлен пользователь {AppInfo.CurrentProfile.FirstName} {AppInfo.CurrentProfile.LastName}, возраст - {age}");
		}
		else
		{
			int currentYear = DateTime.Now.Year;
			int age = currentYear - AppInfo.CurrentProfile.BirthYear;
			Console.WriteLine($"Загружен пользователь {AppInfo.CurrentProfile.FirstName} {AppInfo.CurrentProfile.LastName}, возраст - {age}");
		}
	}

	private static void InitializeTasks()
	{
		AppInfo.Todos = FileManager.LoadTodos(FileManager.TodolistPath);
		if (AppInfo.Todos == null)
			AppInfo.Todos = new TodoList();
	}

}