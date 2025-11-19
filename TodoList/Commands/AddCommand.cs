
namespace TodoList;
internal class AddCommand : ICommand
{
	private TodoList _todoList;
	private bool _multiline;
	public string Task { get; private set; }

	public static string CommandAddEndMark = "!end";

	public AddCommand(TodoList todoList, bool multiline, string task)
	{
		_todoList = todoList;
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
			_todoList.Add(item);
		}
		else
		{
			_todoList.Add(item);
		}

		Console.WriteLine($"Добавлена новая задача {item.GetShortText()}");
	}
}
