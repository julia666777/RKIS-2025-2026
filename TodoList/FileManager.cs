
namespace TodoList;
internal class FileManager
{
	public static string DataDirPath { get => "data"; }

	public static string ProfileInfoName { get => "profile.txt"; }
	public static string ProfileInfoPath { get => Path.Combine(DataDirPath, ProfileInfoName); }

	public static string TodoListFilePath { get => "todo.csv"; }
	public static string TodolistPath { get => Path.Combine(DataDirPath, TodoListFilePath); }

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

	public static void SaveTodos(TodoList todos, string filePath)
	{
		string lines = "";

		for (int i = 0; i < todos.Length; i++)
		{
			lines += $"{todos.GetItem(i).Text};{todos.GetItem(i).IsDone};{todos.GetItem(i).LastUpdate}\n";
		}

		File.WriteAllText(TodolistPath, lines);
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
				var text = args[0];

				bool status = false;
				bool.TryParse(args[1], out status);

				var date = DateTime.Now;
				DateTime.TryParse(args[2], out date);

				TodoItem item = new TodoItem(text);
				item.IsDone = status;
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
