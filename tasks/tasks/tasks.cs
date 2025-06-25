/*Создать приложение, которое на вход будет получать строку.

Если строка будет иметь чётное количество символов, то программа должна разделить её на две подстроки, каждую подстроку перевернуть и соединять обратно обе подстроки в одну строку.

Если входная строка будет иметь нечётное количество символов, то программа должна перевернуть эту строку и к ней добавить изначальную строку, которую ввёл пользователь.

Далее вернуть пользователю обработанную строку.*/

public class StringProcessor
{
    public static void Main(string[] args)
    {
        Console.Write("Введите строку: ");
        string input = Console.ReadLine();
        string result = Process(input);
        Console.WriteLine("Результат: " + result);
    }

    private static string Process(string input)
    {
        if (string.IsNullOrEmpty(input))
            return string.Empty;

        int length = input.Length;

        if (length % 2 == 0)
        {
            int center = length / 2;
            string firstHalf = Reverse(input.Substring(0, center));
            string secondHalf = Reverse(input.Substring(center));
            return firstHalf + secondHalf;
        }
        else
        {
            string reversed = Reverse(input);
            return reversed + input;
        }
    }

    private static string Reverse(string text)
    {
        char[] chars = text.ToCharArray();
        Array.Reverse(chars);
        return new string(chars);
    }
}
