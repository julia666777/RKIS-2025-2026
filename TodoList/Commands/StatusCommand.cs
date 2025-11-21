
namespace TodoList.Commands;
internal class StatusCommand : ICommand
{
    private readonly int _index;
    private TodoStatus _status, _prevStatus;

    public StatusCommand(int index, TodoStatus status)
    {
        _index = index;
        _status = status;
		_prevStatus = TodoStatus.NotStarted;
	}

    public void Execute()
    {
        if (!AppInfo.Todos.IsValidIndex(_index))
        {
            Console.WriteLine($"Ошибка: Задачи с индексом {_index} не существует.");
            return;
        }
		_prevStatus = AppInfo.Todos.GetItem(_index).Status;
		AppInfo.Todos.SetStatus(_index, _status);
        Console.WriteLine($"Статус задачи {_index} изменен на {_status}.");
    }

	public void Unexecute()
	{
		if (!AppInfo.Todos.IsValidIndex(_index))
		{
			Console.WriteLine($"Ошибка: Задачи с индексом {_index} не существует.");
			return;
		}
		AppInfo.Todos.SetStatus(_index, _prevStatus);
		Console.WriteLine("Изменение статуса отменено.");
	}

	public bool PossibleToUndo() => true;

}
