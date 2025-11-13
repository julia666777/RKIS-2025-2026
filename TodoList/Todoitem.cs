
namespace TodoList;
internal class TodoItem
{
	public string Text { get; set; }

	public TodoStatus Status { get; set; }
	public bool IsDone => Status == TodoStatus.Completed;

	public DateTime LastUpdate { get; set; }

	public TodoItem(string text)
	{
		Text = text;
		Status = TodoStatus.NotStarted;
		LastUpdate = DateTime.Now;
	}

	public void MarkDone()
	{
		Status = TodoStatus.Completed;
		LastUpdate = DateTime.Now;
	}

	public void UpdateText(string newText)
	{
		Text = newText;
		LastUpdate = DateTime.Now;
	}

	public string GetShortText()
	{
		return Text.Length > 30 ? Text.Substring(0, 30) + "..." : Text;
	}

	public string GetShortInfo()
	{
		string truncatedText = GetShortText();
		return $"{truncatedText} ({Status}, {LastUpdate.ToShortDateString()})";
	}

	public string GetFullInfo()
	{
		return $"Задача: {Text}\nСтатус: {Status}\nПоследнее обновление: {LastUpdate}";
	}
}
