

namespace TodoList;
internal class ReadCommand : IndexedCommand
{
	public ReadCommand(TodoList todoList, int index) : base(todoList, index) { }

	protected override void SubExecute(TodoItem item)
	{
		Console.WriteLine(item.GetFullInfo());
	}
}
