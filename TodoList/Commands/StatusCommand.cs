
namespace TodoList.Commands;
internal class StatusCommand : ICommand
{
    private readonly TodoList _todoList;
    private readonly int _index;
    private readonly TodoStatus _status;

    public StatusCommand(TodoList todoList, int index, TodoStatus status)
    {
        _todoList = todoList;
        _index = index;
        _status = status;
    }

    public void Execute()
    {
        if (!_todoList.IsValidIndex(_index))
        {
            Console.WriteLine($"Ошибка: Задачи с индексом {_index} не существует.");
            return;
        }
        _todoList.SetStatus(_index, _status);
        Console.WriteLine($"Статус задачи {_index} изменен на {_status}.");
    }
}
