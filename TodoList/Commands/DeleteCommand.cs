

namespace TodoList
{
	internal class DeleteCommand : IndexedCommand
	{
		public DeleteCommand(TodoList todoList, int index) : base(todoList, index)
		{
		}
		protected override void SubExecute(TodoItem item)
		{
			TodoList.Delete(Index);
			Console.WriteLine("Задача успешно удалена");
		}
	}
}