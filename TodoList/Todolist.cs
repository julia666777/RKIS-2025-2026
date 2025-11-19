
namespace TodoList;
internal class TodoList
{
	private List<TodoItem> _items = new List<TodoItem>();

	// Количество задач в списке
	public int Length { get => _items.Count; }
	public int LenghtAllocated { get => _items.Count; }

	//добавление задачи
	public void Add(TodoItem item)
	{
		_items.Add(item);
	}

	//удаление
	public void Delete(int index)
	{
		_items.RemoveAt(index);
	}

	public void View(bool showIndex, bool showDone, bool showDate)
	{
		Console.WriteLine(GenerateTableHeader(showIndex, showDone, showDate));
		for (int i = 0; i < Length; i++)
		{
			Console.WriteLine(GenerateTableRow(_items[i], i, showIndex, showDone, showDate));
		}
	}

	public IEnumerator<TodoItem> GetEnumerator()
	{
		foreach (TodoItem item in _items)
			yield return item;
	}

	public TodoItem GetItem(int index)
	{
		return IsValidIndex(index) ? _items[index] : null;
	}

	private string GenerateTableHeader(bool showIndex, bool showDone, bool showDate)
	{
		string header = "";
		if (showIndex) header += "Index ";
		header += "Text ";
		if (showDone) header += "Done ";
		if (showDate) header += "Date ";
		return header;
	}

	private string GenerateTableRow(TodoItem item, int index, bool showIndex, bool showDone, bool showDate)
	{
		string row = "";
		if (showIndex) row += $"{index} ";
		row += $"\"{item.GetShortText()}\" ";
		if (showDone) row += $"{item.Status} ";
		if (showDate) row += $"{item.LastUpdate.ToShortDateString()} ";
		return row;
	}

	public bool IsValidIndex(int index) => index >= 0 && index < Length;

	public void SetStatus(int index, TodoStatus status)
	{
		if (IsValidIndex(index))
		{
			_items[index].SetStatus(status);
		}
		else
		{
			Console.WriteLine($"Ошибка: Некорректный индекс для установки статуса: {index}");
		}
	}
}
