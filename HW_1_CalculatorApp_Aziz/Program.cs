using System;
using System.Globalization;

class Program
{
    static void Main()
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;
        CultureInfo culture = CultureInfo.InvariantCulture;

        Console.WriteLine("Калькулятор запущен.");
        Console.WriteLine("Доступные операции: +  -  *  /");
        Console.WriteLine("Для выхода введите q в любом месте.");
        Console.WriteLine();

        while (true)
        {
            double firstNumber = ReadNumber("Введите первое число: ", culture);
            string operation = ReadOperation("Введите операцию (+, -, *, /): ");
            double secondNumber = ReadNumber("Введите второе число: ", culture);

            double result;

            switch (operation)
            {
                case "+":
                    result = firstNumber + secondNumber;
                    Console.WriteLine($"Результат: {firstNumber} + {secondNumber} = {result}");
                    break;

                case "-":
                    result = firstNumber - secondNumber;
                    Console.WriteLine($"Результат: {firstNumber} - {secondNumber} = {result}");
                    break;

                case "*":
                    result = firstNumber * secondNumber;
                    Console.WriteLine($"Результат: {firstNumber} * {secondNumber} = {result}");
                    break;

                case "/":
                    if (secondNumber == 0)
                    {
                        Console.WriteLine("Ошибка: деление на ноль невозможно.");
                    }
                    else
                    {
                        result = firstNumber / secondNumber;
                        Console.WriteLine($"Результат: {firstNumber} / {secondNumber} = {result}");
                    }
                    break;
            }

            Console.WriteLine();
        }
    }

    static double ReadNumber(string message, CultureInfo culture)
    {
        while (true)
        {
            Console.Write(message);
            string? input = Console.ReadLine();

            CheckExit(input);

            if (double.TryParse(input, NumberStyles.Float, culture, out double number))
            {
                return number;
            }

            if (double.TryParse(input, out number))
            {
                return number;
            }

            Console.WriteLine("Ошибка: введите корректное число.");
        }
    }

    static string ReadOperation(string message)
    {
        while (true)
        {
            Console.Write(message);
            string? input = Console.ReadLine();

            CheckExit(input);

            if (input == "+" || input == "-" || input == "*" || input == "/")
            {
                return input;
            }

            Console.WriteLine("Ошибка: допустимы только операции +, -, *, /");
        }
    }

    static void CheckExit(string? input)
    {
        if (input != null && input.Trim().ToLower() == "q")
        {
            Console.WriteLine("Программа завершена.");
            Environment.Exit(0);
        }
    }
}