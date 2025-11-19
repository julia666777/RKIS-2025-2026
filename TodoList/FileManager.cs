
namespace TodoList;
internal class FileManager
{
	public static string DataDirPath => "data";

	public static string ProfileInfoName => "profile.txt";
	public static string ProfileInfoPath => Path.Combine(DataDirPath, ProfileInfoName);

	public static string TodoListFilePath => "todo.csv";
	public static string TodolistPath => Path.Combine(DataDirPath, TodoListFilePath);
	private static string _multilineToken = "0xf4afff01fab670c";

	public static void EnsureDataDirectory(string dirPath)
	{
		if (!Directory.Exists(dirPath))
		{
			Directory.CreateDirectory(dirPath);
		}
	}

	public static void SaveProfile(Profile profile, string filePath)
	{
		string textOfProfile = profile.ConvertToFileFormat();
		EnsureDataDirectory(Path.GetDirectoryName(filePath).ToString());
		File.WriteAllText(filePath, textOfProfile);
	}

	public static Profile LoadProfile(string filePath)
	{
		if (!File.Exists(filePath))
			return null;

		string[] lines = File.ReadAllLines(filePath);
		if (lines.Length < 3)
			return null;

		Profile profile = new Profile();
		profile.FirstName = lines[0];
		profile.LastName = lines[1];

		if (!int.TryParse(lines[2], out int year))
			return null;

		profile.BirthYear = year;

		return profile;
	}

	private static string TextStringConvert(string text)
	{
		var args = text.Split('\n');
		string output = "";
		if (args.Length > 1)
		{
			foreach (var i in args)
			{
				output += i + _multilineToken;
			}
		}
		else
			output = text;
		return output;
	}

	public static void SaveTodos(TodoList todos, string filePath)
	{
		string lines = "";

		for (int i = 0; i < todos.Length; i++)
		{
			var item = todos.GetItem(i);
			lines += $"{item.Status.ToString()};{item.LastUpdate};{TextStringConvert(item.Text)}\n";
		}

		File.WriteAllText(TodolistPath, lines);
	}

	private static string ParseFileText(string text)
	{
		if (text.Contains(_multilineToken))
		{
			var args = text.Split(_multilineToken, StringSplitOptions.RemoveEmptyEntries);
			string output = "";
			foreach (var i in args)
				output += i + "\n";
			return output;
		}
		else return text;
	}

	public static TodoList LoadTodos(string filePath)
	{
		if (!File.Exists(filePath))
			return null;

		var lines = File.ReadAllLines(filePath);
		TodoList list = new TodoList();

		foreach (var line in lines)
		{
			var args = line.Split(';', 3);
			if (args.Length > 2)
			{
				TodoStatus status;
				if (!Enum.TryParse(args[0], true, out status))
				{
					Console.WriteLine($"Предупреждение: Не удалось разобрать статус '{args[0]}'. Использование NotStarted.");
					status = TodoStatus.NotStarted; // Дефолт. статус в случае ошибки
				}

				var date = DateTime.Now;
				DateTime.TryParse(args[1], out date);

				var text = args[2];

				TodoItem item = new TodoItem(ParseFileText(text));
				item.Status = status;
				item.LastUpdate = date;
				list.Add(item);
			}
		}
		return list;
	}

	public static void SaveData(Profile profile, TodoList todoList, string profilePath, string todoPath)
	{
		SaveProfile(profile, profilePath);
		SaveTodos(todoList, todoPath);
	}
}
