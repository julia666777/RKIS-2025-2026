

namespace TodoList;
internal class DoneCommand : IndexedCommand
{
	public DoneCommand(TodoList todoList, int index) : base(todoList, index) { }

	protected override void SubExecute(Todoitem item)
	{
		item.MarkDone();
		Console.WriteLine($"Задача под номером {Index} завершена.");
	}
}
