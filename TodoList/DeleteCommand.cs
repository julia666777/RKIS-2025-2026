using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TodoList
{
	internal class DeleteCommand : IndexedCommand
	{
		public DeleteCommand(TodoList todoList, int index) : base(todoList, index)
		{
		}
		protected override void SubExecute(Todoitem item)
		{
			TodoList.Delete(Index);
			Console.WriteLine("Задача успешно удалена");
		}
	}
}