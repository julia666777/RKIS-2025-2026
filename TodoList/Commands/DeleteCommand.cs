

namespace TodoList
{
	internal class DeleteCommand : IndexedCommand
	{
		private TodoItem _deletedItem;

		public DeleteCommand(int index) : base(index)
		{
			_deletedItem = null;
		}

		protected override void SubExecute(TodoItem item)
		{
			_deletedItem = item;
			AppInfo.Todos.Delete(Index);
			Console.WriteLine("Задача успешно удалена");
		}

		// Not need
		protected override void SubUnExecute(TodoItem item) { }

		public new void Unexecute()
		{
			AppInfo.Todos.Add(_deletedItem);
		}
	}
}