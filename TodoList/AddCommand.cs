
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
		if (_multiline)
		{
			Task = "";

			for (var line = Console.ReadLine(); line != CommandAddEndMark; line = Console.ReadLine())
			{
				Task += line + "\n";
			}

			_todoList.Add(new Todoitem(Task));
		}
		else
		{
			_todoList.Add(new Todoitem(Task));
		}
	}
}
