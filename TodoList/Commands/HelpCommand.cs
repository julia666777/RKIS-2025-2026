
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
				read - просмотреть полный текст задачи
				delete - удалить задачу
				update - обновить задачу
				status - <idx> изменить статус задачи {TodoStatus.Completed}, {TodoStatus.Failed}, {TodoStatus.NotStarted}, {TodoStatus.InProgress}
				exit - выход
				"""); 
			}
	}
}
