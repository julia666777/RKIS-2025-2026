
namespace TodoList;
public class Program 
{

	private static Profile profile;
	private static TodoList todoList;


	public static void Main(string[] args)
    {
		Console.WriteLine("Работу выполнили Чернова Юлия и Соловьев Иван 3833");

		InitializeUserData();
		InitializeTasks();


		while (true)
        {
            var commandLine = Console.ReadLine();
			var command = CommandParser.Parse(commandLine, todoList, profile);
			command.Execute();
			FileManager.SaveData(profile, todoList, FileManager.ProfileInfoPath, FileManager.TodolistPath);
		}
    }

	private static void GetUserAge()
	{
		Console.WriteLine("Введите год рождения:");
		string birthYearString = Console.ReadLine();

		bool isBirthYearValid = int.TryParse(birthYearString, out int birthYear);

		if (isBirthYearValid)
		{ 
			profile.BirthYear = birthYear;	
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
		profile = FileManager.LoadProfile(FileManager.ProfileInfoPath);

		if (profile == null)
		{
			profile = new Profile();

			Console.WriteLine("Введите имя:");
			profile.FirstName = Console.ReadLine();

			Console.WriteLine("Введите фамилию:");
			profile.LastName = Console.ReadLine();

			GetUserAge();

			int currentYear = DateTime.Now.Year;
			int age = currentYear - profile.BirthYear;

			FileManager.SaveProfile(profile, FileManager.ProfileInfoPath);

			Console.WriteLine($"Добавлен пользователь {profile.FirstName} {profile.LastName}, возраст - {age}");
		}
		else
		{
			int currentYear = DateTime.Now.Year;
			int age = currentYear - profile.BirthYear;
			Console.WriteLine($"Загружен пользователь {profile.FirstName} {profile.LastName}, возраст - {age}");
		}
	}

	private static void InitializeTasks()
	{
		todoList = FileManager.LoadTodos(FileManager.TodolistPath);
		if (todoList == null)
			todoList = new TodoList();
	}

}