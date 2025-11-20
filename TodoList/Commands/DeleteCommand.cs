

namespace TodoList
{
	internal class DeleteCommand : IndexedCommand
	{
		public DeleteCommand(int index) : base(index)
		{
		}

		protected override void SubExecute(TodoItem item)
		{
			AppInfo.Todos.Delete(Index);
			Console.WriteLine("Задача успешно удалена");
		}
	}
}