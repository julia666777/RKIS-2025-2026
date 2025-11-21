
namespace TodoList.Commands;
internal class StatusCommand : ICommand
{
    private readonly int _index;
    private readonly TodoStatus _status;

    public StatusCommand(int index, TodoStatus status)
    {
        _index = index;
        _status = status;
    }

    public void Execute()
    {
        if (!AppInfo.Todos.IsValidIndex(_index))
        {
            Console.WriteLine($"Ошибка: Задачи с индексом {_index} не существует.");
            return;
        }
		AppInfo.Todos.SetStatus(_index, _status);
        Console.WriteLine($"Статус задачи {_index} изменен на {_status}.");
    }

	public void Unexecute()
	{

	}

	public bool PossibleToUndo() => true;

}
