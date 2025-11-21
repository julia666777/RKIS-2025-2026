

namespace TodoList;
internal class AppInfo
{
	public static TodoList Todos { get; set; }
	public static Profile CurrentProfile { get; set; }

	public static Stack<ICommand> UndoStack = new Stack<ICommand>();
	public static Stack<ICommand> RedoStack = new Stack<ICommand>();

	public static void UndoPush(ICommand command) => UndoStack.Push(command);
	public static void UndoPop() => UndoStack.Pop();
	public static void RedoPush(ICommand command) => RedoStack.Push(command);
	public static void RedoPop() => RedoStack.Pop();

}
