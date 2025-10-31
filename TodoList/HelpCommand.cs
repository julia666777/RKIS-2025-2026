using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TodoList
{
    internal class HelpCommand : ICommand
	{
		public void Execute()
		{
			Console.WriteLine($"""
				help - список команд
				profile - данные пользователя
				add - добавить задачу
				add -m - добавить задачу в многострочном режиме
				view -i, -s, -d, -a - показать задачи
				read - просмотреть полный текст задачи
				done - отметить задачу выполненной
				delete - удалить задачу
				update - обновить задачу
				exit - выход
				"""); 
			}
	}
}
