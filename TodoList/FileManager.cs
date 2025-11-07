

using System.Text.Json;

namespace TodoList;
internal class FileManager
{

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
		throw new NotImplementedException();
	}

	public static TodoList LoadTodos(string filePath)
	{
		throw new NotImplementedException();
	}

	public static void SaveData(Profile profile, TodoList todoList, string profilePath, string todoPath)
	{
		SaveProfile(profile, profilePath);
		SaveTodos(todoList, todoPath);

	}
}
