
namespace TodoList;
internal abstract class IndexedCommand : ICommand
{
	public int Index { get; private set; }

	public IndexedCommand(int index) => Index = index;

	public void Execute()
	{
		var item = AppInfo.Todos.GetItem(Index);
		if (item != null)
			SubExecute(item);
	}

	// Don't check for NULL, this item is guaranteed not to be NULL
	protected abstract void SubExecute(TodoItem item);
}
