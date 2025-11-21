
namespace TodoList;
internal class ProfileCommand : ICommand
{
		public void Execute()
		{
			Console.WriteLine($"Пользователь {AppInfo.CurrentProfile.GetInfo()}");
		}
	public void Unexecute() { }
}


