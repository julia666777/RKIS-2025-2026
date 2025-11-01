
namespace TodoList;
internal abstract class IndexedCommand : ICommand
{
	public TodoList TodoList { get; private set; }
	public int Index { get; private set; }

	public IndexedCommand(TodoList todoList, int index)
	{
		TodoList = todoList;
		Index = index;
	}

	public void Execute()
	{
		var item = TodoList.GetItem(Index);
		if (item != null)
			SubExecute(item);
	}

	// Don't check for NULL, this item is guaranteed not to be NULL
	protected abstract void SubExecute(TodoItem item);
}
