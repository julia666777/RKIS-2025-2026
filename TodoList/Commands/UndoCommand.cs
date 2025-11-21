

namespace TodoList.Commands;
internal class UndoCommand : ICommand
{
	public void Execute()
	{
		if (AppInfo.UndoStack.TryPeek(out var command))
		{
			command.Unexecute();
			AppInfo.RedoPush(command);
			AppInfo.UndoPop();
		}
	}

	public void Unexecute() { }
}
