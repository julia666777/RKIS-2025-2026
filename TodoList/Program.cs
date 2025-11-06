

namespace TodoList;
public class Program 
{

	private static Profile profile = new Profile();
	private static TodoList todoList = new TodoList();

	private const string ProfileInfoPath = "profile.txt";
	private const string DataDirPath = "data";


	public static void Main(string[] args)
    {
		Console.WriteLine("Работу выполнили Чернова Юлия и Соловьев Иван 3833");

		InitializeUserData();

        while (true)
        {
            var commandLine = Console.ReadLine();
			var command = CommandParser.Parse(commandLine, todoList, profile);
			command.Execute();
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
		Console.WriteLine("Введите имя:");
		profile.FirstName = Console.ReadLine();

        Console.WriteLine("Введите фамилию:");
		profile.LastName = Console.ReadLine();

		GetUserAge();

		int currentYear = DateTime.Now.Year;
		int age = currentYear - profile.BirthYear;

        Console.WriteLine($"Добавлен пользователь {profile.FirstName} {profile.LastName}, возраст - {age}");

		
    }

}