using System;

public class UserInfo{

    struct UserData
    {
        public string firstName, lastName, birthYearString;
        public int currentYear, age, birthYear;
    }

    private static string[] todos;
    private static bool[] statuses;
    private static DateTime[] dates;

    private static int todosCount, todosLen;
    private static UserData userData = new UserData();
    private static bool isProgramRunning = true;
    private static int todosStartLen = 2;


    private const string COMMAND_ADD_NAME = "add";
    private const string COMMAND_PROFILE_NAME = "profile";
    private const string COMMAND_VIEW_NAME = "view";
    private const string COMMAND_EXIT_NAME = "exit";
    private const string COMMAND_HELP_NAME = "help";
    private const string COMMAND_DONE_NAME = "done";
    private const string COMMAND_DELETE_NAME = "delete";
    private const string COMMAND_UPDATE_NAME = "update";


    private static void DoHelp()
    {
        Console.WriteLine("****\tUserInfo Помощник\t****");
        Console.WriteLine($"{COMMAND_PROFILE_NAME} — выводит данные пользователя в формате: <Имя> <Фамилия>, <Год рождения> .");
        Console.WriteLine($"{COMMAND_ADD_NAME} — добавляет новую задачу. Формат ввода: add \"текст задачи\"");
        Console.WriteLine($"{COMMAND_VIEW_NAME} — выводит все задачи из массива (только непустые элементы).");
        Console.WriteLine($"{COMMAND_EXIT_NAME} — завершает цикл и останавливает выполнение программы.");
        Console.WriteLine($"{COMMAND_DONE_NAME} — отмечает задачу выполненной.");
        Console.WriteLine($"{COMMAND_DELETE_NAME} — <idx> — удаляет задачу по индексу.");
        Console.WriteLine($"{COMMAND_UPDATE_NAME} — <idx> \"new_text\" — обновляет текст задачи.");
    }

    private static void DoExit() => isProgramRunning = false;


    private static void InsertNewTask(string task)
    {
        int newTodosCount = todosCount + 1;

        // Расширение массива 'todos'
        if (newTodosCount > todosLen)
        {
            int newTodosLen = todosLen * 2;

            string[] newTodos = new string[newTodosLen];
            bool[] newStatuses = new bool[newTodosLen];
            DateTime[] newDates = new DateTime[newTodosLen];

            for (int i = 0; i < todosCount; i++)
            {
                newTodos[i] = todos[i];
                newStatuses[i] = statuses[i];
                newDates[i] = dates[i];
            }

            newTodos[todosCount] = task;
            newDates[todosCount] = DateTime.Now;
            newStatuses[todosCount] = false;

            todos = newTodos;
            statuses = newStatuses;
            dates = newDates;

            todosLen = newTodosLen;
        }
        else
        {
            todos[todosCount] = task;
            dates[todosCount] = DateTime.Now;
            statuses[todosCount] = false;
        }

        todosCount = newTodosCount;
    }

    private static void DoAdd(string command)
    {
        string newTask = "";
        var userEnteredTask = command.Split($"{COMMAND_ADD_NAME} ");

        foreach (var item in userEnteredTask)
        {
            newTask += item;
        }

        InsertNewTask(newTask);

        Console.WriteLine($"Added new task: \"{newTask}\".");
    }

    private static void DoProfile()
    {
        Console.WriteLine($"User Data: \"{userData.firstName}\" \"{userData.lastName}\", {userData.birthYear}");
    }

    private static void DoView()
    {
        Console.WriteLine("===========================================================");
        Console.WriteLine("****\tAll tasks list\t****");

        for (int i = 0;i < todosCount;i++)
        {
            string isDone = statuses[i] ? "сделано" : "не сделано";
            Console.WriteLine($"\t\"{i}\", \"{todos[i]}\", \"{isDone}\", \"{dates[i].ToString()}\"");
        }

        Console.WriteLine("===========================================================");
    }

    private static void DoDone(string command)
    {

    }

    private static void DoDelete(string command)
    {

    }

    private static void DoUpdate(string command)
    {

    }


    // Эта байда обрабатывает комманду введенную юзером
    private static void ProcessCommand(string command)
    {
        // Проверка комманд, если комманда опознана, то выполняется соответствующая процедупа,
        // а эта завершается

        if (command == COMMAND_HELP_NAME)
        { 
            DoHelp();
            return;
        }

        if (command == COMMAND_EXIT_NAME)
        {
            DoExit();
            return;
        }

        if (command.StartsWith(COMMAND_ADD_NAME))
        {
            DoAdd(command);
            return;
        }

        if (command == COMMAND_PROFILE_NAME)
        {
            DoProfile();
            return;
        }

        if (command == COMMAND_VIEW_NAME)
        {
            DoView();
            return;
        }

        if (command.StartsWith(COMMAND_DONE_NAME))
        {
            DoDone(command);
            return;
        }

        if (command.StartsWith(COMMAND_DELETE_NAME))
        {
            DoDelete(command);
            return;
        }

        if (command.StartsWith(COMMAND_UPDATE_NAME))
        {
            DoUpdate(command);
            return;
        }

        // Если ни одна комманда не распознана
        Console.WriteLine("Неизвестная комманда!");
    }

    // Получение данных пользователя и их обработка 
    private static void InitializeUserData()
    {
        Console.WriteLine("Работу выполнили Чернова Юлия и Соловьев Иван 3833");

        Console.WriteLine("Введите имя:");
        userData.firstName = Console.ReadLine();

        Console.WriteLine("Введите фамилию:");
        userData.lastName = Console.ReadLine();
        Console.WriteLine("Введите год рождения:");
        userData.birthYearString = Console.ReadLine();

        bool isBirthYearValid = int.TryParse(userData.birthYearString, out userData.birthYear);

        if (!isBirthYearValid)
        {
            Console.WriteLine("Ошибка: Некорректный год рождения. Пожалуйста, введите целое число.");
            return;
        }

        userData.currentYear = DateTime.Now.Year;
        userData.age = userData.currentYear - userData.birthYear;

        Console.WriteLine($"Добавлен пользователь {userData.firstName} {userData.lastName}, возраст - {userData.age}");
    }


    private static void InitializeTasksData()
    {
        todosCount = 0;
        todosLen = todosStartLen;

        todos = new string[todosStartLen];
        statuses = new bool[todosStartLen];
        dates = new DateTime[todosStartLen];
    }


    public static void Main(string[] args){
        InitializeUserData();
        InitializeTasksData();

        while (isProgramRunning)
        {
            var command = Console.ReadLine();

            ProcessCommand(command);
        }
    }
}