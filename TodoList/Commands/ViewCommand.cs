
namespace TodoList;
internal class ViewCommand : ICommand
{
	private TodoList _todoList;
	private bool _showIndexes;
	private bool _showDates;
	private bool _showStatuses;

	public ViewCommand(TodoList todoList, bool indexes, bool statuses, bool dates)
	{
		_todoList = todoList;
		_showDates = dates;
		_showStatuses = statuses;
		_showIndexes = indexes;
	}

	public void Execute()
	{
		Console.WriteLine("===========================================================");
		Console.WriteLine("\tИнформация о задачах\t");
		_todoList.View(_showIndexes, _showStatuses, _showDates);
		Console.WriteLine("===========================================================");
	}
}
