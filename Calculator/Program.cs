using System;
using System.Linq;

namespace Calculator
{
    public class Calculator
    {
        public static double Get(double first, string operation, double second, int count)
        {
            double num = 0;
            switch (operation)
            {
                case "+":
                    num = first + second;
                    break;
                case "-":
                    num = first - second;
                    break;
                case "*":
                    num = first * second;
                    break;
                case "/":
                    if (second == 0)
                    {
                        Console.WriteLine("На ноль делить нельзя");
                        var newNum = Number.Get($"{count}");
                        num = first / newNum;
                        break;
                    }
                    else
                    {
                        num = first / second;
                        break;
                    }

            }
            return num;
        }
    }
    public class Number
    {

        public static double Get(string name)
        {
            double number = 0;
            bool isSuccessfully = false;
            while (!isSuccessfully)
            {
                Console.WriteLine($"Введите {name} число");
                var value = Console.ReadLine();
                double num;
                isSuccessfully = Double.TryParse(value, out num);

                if (isSuccessfully == true && value.Any(x => x == ',') == false)
                {
                    number = num;
                }
                else
                {
                    Console.WriteLine("Введено некорректное число");
                    isSuccessfully = false;
                }
            }

            return number;
        }
    }
    public class Operation
    {
        public static string Get()
        {
            string answer = "";
            bool isSuccessfully = false;
            while (!isSuccessfully)
            {
                Console.WriteLine("Введите операцию");
                var symbol = Console.ReadLine();
                if (symbol.Count() == 1 && (symbol == "*" || symbol == "/" || symbol == "+" || symbol == "-"))
                {
                    answer = symbol;
                    isSuccessfully = true;
                }
                else
                {
                    Console.WriteLine("Введена некорректная операция");
                }
            }
            return answer;
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            var firstNum = Number.Get("1");
            var key = ConsoleKey.A;
            int count = 2;
            while (key != ConsoleKey.Escape)
            {

                var operation = Operation.Get();
                var secondNum = Number.Get($"{count}");
                var answer = Calculator.Get(firstNum, operation, secondNum, count);
                Console.WriteLine(answer);
                Console.WriteLine("продолжить вычисление?");
                key = Console.ReadKey().Key;
                firstNum = answer;
                count++;
            }

        }
    }
}
