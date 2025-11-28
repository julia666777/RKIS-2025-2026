
namespace TodoList;
internal class FileManager
{
	public static string DataDirPath => "data";

	public static string ProfileInfoName => "profile.cso";
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

	public static string ProfileToFormat()
	{
		return AppInfo.CurrentProfile.FirstName + "\n" + AppInfo.CurrentProfile.LastName + "\n" + AppInfo.CurrentProfile.BirthYear;
	}

	public static void SaveProfile(string filePath)
	{
		string textOfProfile = ProfileToFormat();
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

	public static bool LoadProfiles(string filePath, out List<Profile> profile)
	{
		profile = new List<Profile>();

		if (!File.Exists(filePath))
			return false;

		string[] lines = File.ReadAllLines(filePath);
		foreach (var i in lines)
		{
			var args = i.Split(';');
			Profile p = new Profile();
			Guid.TryParse(args[0], out var id);
			p.Id = id;
			p.Login = args[1];
			p.Password = args[2];
			p.FirstName = args[3];
			p.LastName = args[4];
			int.TryParse(args[5], out int year);
			p.BirthYear = year;
			profile.Add(p);
		}

		return true;
	}

	public static bool IsThereProfilesFile() => File.Exists(ProfileInfoPath);

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

	public static void SaveTodos(string filePath)
	{
		string lines = "";

		for (int i = 0; i < AppInfo.Todos.Length; i++)
		{
			var item = AppInfo.Todos.GetItem(i);
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

	public static void SaveData(string profilePath, string todoPath)
	{
		//SaveProfile(profilePath);
		//SaveTodos(todoPath);
	}

	public static void SaveProfiles(string pathToSaves)
	{
		List<string> text = new List<string>();
		foreach (var i in AppInfo.Profiles)
		{
			string s = $"{i.Id};{i.Login};{i.Password};{i.FirstName};{i.LastName};{i.BirthYear}";
			text.Add(s);
		}
		File.WriteAllLines(pathToSaves, text);
	}
}
