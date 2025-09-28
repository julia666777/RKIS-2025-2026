using System;

public class UserInfo{
    public static void Main(string[] args){
        Console.WriteLine("Работу выполнили Чернова Юлия и Соловьев Иван 3833");

        Console.WriteLine("Введите имя:");
        string firstName = Console.ReadLine();

        Console.WriteLine("Введите фамилию:");
        string lastName = Console.ReadLine();
        Console.WriteLine("Введите год рождения:");
        string birthYearString = Console.ReadLine();

        int birthYear;
        bool isBirthYearValid = int.TryParse(birthYearString, out birthYear);

        if (!isBirthYearValid)
        {
            Console.WriteLine("Ошибка: Некорректный год рождения. Пожалуйста, введите целое число.");
            return;
        }

        int currentYear = DateTime.Now.Year;
        int age = currentYear - birthYear;

        Console.WriteLine($"Добавлен пользователь {firstName} {lastName}, возраст - {age}");
    }
}