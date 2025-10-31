

namespace TodoList;
internal class TodoList
{
	private const int ItemsStartLen = 2;
	private Todoitem[] _items = new Todoitem[ItemsStartLen];

	// Количество задач в списке
	private int _realLenght = 0;
	public int Length { get => _realLenght; }
	public int LenghtAllocated { get => _items.Length; }

	//добавление задачи
	public void Add(Todoitem item)
	{
		_items = IncreaseArray(_items, item);
	}

	//удаление
	public void Delete(int index)
	{
		if (index >= 0 && index < _items.Length)
		{
			Todoitem[] newArray = new Todoitem[_items.Length - 1];
			Array.Copy(_items, 0, newArray, 0, index);
			Array.Copy(_items, index + 1, newArray, index, _items.Length - index - 1);
			_items = newArray;
		}
		else
		{
			Console.WriteLine("Неверный индекс");
		}
	}

	public void View(bool showIndex, bool showDone, bool showDate)
	{
		Console.WriteLine(GenerateTableHeader(showIndex, showDone, showDate));
		for (int i = 0; i < Length; i++)
		{
			Console.WriteLine(GenerateTableRow(_items[i], i, showIndex, showDone, showDate));
		}
	}

	public Todoitem GetItem(int index)
	{
		return IsValidIndex(index) ? _items[index] : null;
	}

	// увеличение размера массива при переполнении
	private Todoitem[] IncreaseArray(Todoitem[] items, Todoitem item)
	{
		int newLen = Length + 1;
		
		if (newLen >= LenghtAllocated)
		{
			Todoitem[] newArray = new Todoitem[LenghtAllocated * 2];
			Array.Copy(items, newArray, items.Length);
			newArray[Length] = item;
			_realLenght++;
			return newArray;
		}
		else
		{
			_items[Length] = item;
			_realLenght++;
			return _items;
		}
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

	private string GenerateTableRow(Todoitem item, int index, bool showIndex, bool showDone, bool showDate)
	{
		string row = "";
		if (showIndex) row += $"{index} ";
		row += $"\"{item.Text}\" ";
		if (showDone) row += $"{item.IsDone} ";
		if (showDate) row += $"{item.LastUpdate.ToShortDateString()} ";
		return row;
	}

	public bool IsValidIndex(int index) => index >= 0 && index < Length;
}
