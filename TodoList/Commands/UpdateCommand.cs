
namespace TodoList;
internal class UpdateCommand : IndexedCommand
{
	private string _task;

	public UpdateCommand(TodoList todoList, int index, string task) : base(todoList, index)
	{
		_task = task;
	}

	protected override void SubExecute(TodoItem item)
	{
		item.UpdateText(_task);
		Console.WriteLine($"Задача под номером {Index} изменена на \"{_task}\".");
	}

}
