

namespace TodoList;
internal class AppInfo
{
	public static TodoList Todos { get; set; }
	public static Profile CurrentProfile { get; set; }

	public static Stack<ICommand> UndoStack = new Stack<ICommand>();
	public static Stack<ICommand> RedoStack = new Stack<ICommand>();

}
