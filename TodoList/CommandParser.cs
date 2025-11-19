using System.Diagnostics;
using TodoList.Commands;

namespace TodoList;
internal class CommandParser
{
	//==========================================================================
	//	COMMANDS STRINGS
	//==========================================================================
	private const string CommandAddName = "add";
	private const string CommandProfileName = "profile";
	private const string CommandExitName = "exit";
	private const string CommandHelpName = "help";
	private const string CommandDeleteName = "delete";
	private const string CommandUpdateName = "update";
	private const string CommandReadName = "read";
	private const string CommandStatusName = "status";

	private static string[] CommandAddMultilineFlags = new string[]
	{
		"--multiline",
		"-m"
	};

	private static string[] CommandViewIndexFlags = new string[]
	{
		"--index", "-i"
	};
	private static string[] CommandViewStatusFlags = new string[]
	{
		"--status", "-s"
	};
	private static string[] CommandViewUpdateFlags = new string[]
	{
		"--update-date", "-d"
	};
	private static string[] CommandViewAllFlags = new string[]
	{
		"--all", "-a"
	};
	//==========================================================================


	public static ICommand Parse(string inputString, TodoList todoList, Profile profile)
	{
		if (CompareCommand(inputString, CommandExitName)) return new ExitCommand();
		else if (CompareCommand(inputString, CommandHelpName)) return new HelpCommand();
		else if (CompareCommand(inputString, CommandAddName)) return GetAddCommand(inputString, todoList, profile);
		else if (CompareCommand(inputString, CommandStatusName)) return GetStatusCommand(inputString, todoList, profile);
		else if (CompareCommand(inputString, CommandProfileName)) return new ProfileCommand(profile);
		else if (CompareCommand(inputString, CommandDeleteName)) return GetDeleteCommand(inputString, todoList, profile);
		else if (CompareCommand(inputString, CommandUpdateName)) return GetUpdateCommand(inputString, todoList, profile);
		else if (CompareCommand(inputString, CommandReadName)) return GetReadCommand(inputString, todoList, profile);

		return new NoneCommand();
	}

	private static bool CompareCommand(string inputString, string commandName) => inputString.StartsWith(commandName);

	private static ICommand GetUncorrect() => new UncorrectCommand();

	private static bool IsTaskValidToAdd(string task)
	{
		return task.Length > 0;
	}

	private static ICommand GetAddCommand(string inputString, TodoList todoList, Profile profile)
	{
		bool multiline = false;
		var userEnteredTask = inputString.Split(' ', 2);

		// checking for flags
		foreach (var i in CommandAddMultilineFlags)
		{
			if (userEnteredTask.Contains(i))
			{
				multiline = true;
			}
		}

		// execution
		if (!multiline)
		{
			if (userEnteredTask.Length > 1 && IsTaskValidToAdd(userEnteredTask[1]))
			{
				return new AddCommand(todoList, multiline, userEnteredTask[1]);
			}
			else return GetUncorrect();
		}
		else
		{
			return new AddCommand(todoList, multiline, "");
		}
	}

	private static int ReadIndexFromCommand(TodoList todoList, string commandName, string command, bool checkForNumOfTasks = true)
	{
		var items = command.Split(commandName);
		Debug.Assert(items.Length >= 2);

		bool indexValid = int.TryParse(items[1], out int index);
		Debug.Assert(indexValid);

		if (checkForNumOfTasks && (index < 0 || index >= todoList.Length))
		{
			throw new Exception($"ReadIndexFromCommand: Uncorrected index {index}!");
		}

		return index;
	}

	private static ICommand GetDeleteCommand(string inputString, TodoList todoList, Profile profile) => new DeleteCommand(todoList, ReadIndexFromCommand(todoList, CommandDeleteName, inputString, false));
	private static ICommand GetReadCommand(string inputString, TodoList todoList, Profile profile) => new ReadCommand(todoList, ReadIndexFromCommand(todoList, CommandReadName, inputString, false));

	private static ICommand GetUpdateCommand(string inputString, TodoList todoList, Profile profile)
	{
		var args = inputString.Split(' ', 3);

		bool indexValid = int.TryParse(args[1], out int index);
		if (!indexValid)
		{
			Console.WriteLine($"{CommandUpdateName}: не верный индекс задачи.");
			return GetUncorrect();
		}

		if (index < 0 && index > todoList.Length)
		{
			Console.WriteLine($"{CommandUpdateName}: задачи под номером {index} не существует.");
			return GetUncorrect();
		}

		if (args.Length > 2 && todoList.IsValidIndex(index))
		{
			return new UpdateCommand(todoList, index, args[2]);
		}
		else
			return GetUncorrect();
	}

	// TODO: make status command
	private static ICommand GetStatusCommand(string inputString, TodoList todoList, Profile profile)
	{
		var args = inputString.Split(' ', 3); 
		if (args.Length < 3)
		{
			Console.WriteLine("Ошибка: Команда требует индекс задачи и новый статус. Пример: status 0 InProgress");
			return GetUncorrect();
		}
		if (!int.TryParse(args[1], out int index))
		{
			Console.WriteLine($"Ошибка: '{args[1]}' не является корректным индексом задачи.");
			return GetUncorrect();
		}
		if (!Enum.TryParse(args[2], true, out TodoStatus newStatus))
		{
			Console.WriteLine($"Ошибка: '{args[2]}' не является корректным статусом. Доступные статусы: {string.Join(", ", Enum.GetNames(typeof(TodoStatus)))}");
			return GetUncorrect();
		}
		return new StatusCommand(todoList, index, newStatus);
	}
}
