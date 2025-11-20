
namespace TodoList;
internal class AddCommand : ICommand
{
	private bool _multiline;
	public string Task { get; private set; }

	public static string CommandAddEndMark = "!end";

	public AddCommand(bool multiline, string task)
	{
		_multiline = multiline;
		Task = task;
	}

	public void Execute()
	{
		TodoItem item = new TodoItem(Task);

		if (_multiline)
		{
			Task = "";

			for (var line = Console.ReadLine(); line != CommandAddEndMark; line = Console.ReadLine())
			{
				Task += line + "\n";
			}

			item.Text = Task;
			AppInfo.Todos.Add(item);
		}
		else
		{
			AppInfo.Todos.Add(item);
		}

		Console.WriteLine($"Добавлена новая задача {item.GetShortText()}");
	}
}
