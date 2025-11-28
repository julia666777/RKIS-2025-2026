
namespace TodoList;
internal class ProfileCommand : ICommand
{

	public void Execute()
	{
		int currentYear = DateTime.Now.Year;
		string info = $"{AppInfo.CurrentProfile.FirstName} {AppInfo.CurrentProfile.LastName}, возраст {currentYear - AppInfo.CurrentProfile.BirthYear}";
		Console.WriteLine($"Пользователь {info}");
	}

	public void Unexecute() { }
}


