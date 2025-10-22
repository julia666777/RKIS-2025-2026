using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace TodoList;
internal class Todoitem
{
	public string Text { get; set; }
	public bool IsDone { get; set; }
	public DateTime LastUpdate { get; set; }

	public Todoitem(string text)
	{
		Text = text;
		IsDone = false;
		LastUpdate = DateTime.Now;
	}

	public void MarkDone()
	{
		IsDone = true;
		LastUpdate = DateTime.Now;
	}

	public void UpdateText(string newText)
	{
		Text = newText;
		LastUpdate = DateTime.Now;
	}

	public string GetShortInfo()
	{
		string status = IsDone ? "Done" : "Pending";
		string truncatedText = Text.Length > 30 ? Text.Substring(0, 30) + "..." : Text;
		return $"{truncatedText} ({status}, {LastUpdate.ToShortDateString()})";
	}

	public string GetFullInfo()
	{
		string status = IsDone ? "Выполнено" : "В процессе";
		return $"Задача: {Text}\nСтатус: {status}\nПоследнее обновление: {LastUpdate}";
	}

	public class TodoList
	{
		private Todoitem[] _items = new Todoitem[0];
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
			for (int i = 0; i < _items.Length; i++)
			{
				Console.WriteLine(GenerateTableRow(_items[i], i, showIndex, showDone, showDate));
			}
		}
		public Todoitem GetItem(int index)
		{
			if (index >= 0 && index < _items.Length)
			{
				return _items[index];
			}
			else
			{
				return null; 
			}
		}

		// увеличение размера массива при переполнении
		private Todoitem[] IncreaseArray(Todoitem[] items, Todoitem item)
		{
			Todoitem[] newArray = new Todoitem[items.Length + 1];
			Array.Copy(items, newArray, items.Length);
			newArray[items.Length] = item;
			return newArray;
		}

		private string GenerateTableHeader(bool showIndex, bool showDone, bool showDate)
		{
			string header = "";
			if (showIndex) header += "Indext";
			header += "Textt";
			if (showDone) header += "Donet";
			if (showDate) header += "Datet";
			return header;
		}

		private string GenerateTableRow(Todoitem item, int index, bool showIndex, bool showDone, bool showDate)
		{
			string row = "";
			if (showIndex) row += $"{index}t";
			row += $"{item.Text}t";
			if (showDone) row += $"{item.IsDone}t";
			if (showDate) row += $"{item.LastUpdate.ToShortDateString()}t";
			return row;
		}
	}
}
