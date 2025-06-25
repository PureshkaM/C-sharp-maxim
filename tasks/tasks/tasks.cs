/*Создать приложение, которое на вход будет получать строку.

Если строка будет иметь чётное количество символов, то программа должна разделить её на две подстроки, каждую подстроку перевернуть и соединять обратно обе подстроки в одну строку.

Если входная строка будет иметь нечётное количество символов, то программа должна перевернуть эту строку и к ней добавить изначальную строку, которую ввёл пользователь.

Далее вернуть пользователю обработанную строку.*/



/*Добавить в программу из «Задания 1» дополнительный функционал. Необходимо начать проверять входящую строку, чтобы в ней были только буквы из
 * английского алфавита в нижнем регистре «abcdefghijklmnopqrstuvwxyz». Результатом работы программы должна стать либо обработанная строка, как в «Задании 1» 
 * или сообщение о том, что были введены не подходящие символы, в сообщении об ошибке должны быть перечислены все неподходящие символы, которые были введены

Результат программы:

Если не подходящая строка:

Сообщение об ошибке с информацией

Если подходящая строка:

Обработанная строка*/



/*Добавить в программу из «Задания 2» дополнительный функционал. Помимо обработанной строки, необходимо также возвращать пользователю информацию о том, 
 * сколько раз повторялся каждый символ в обработанной строке.

Результат программы:

Если не подходящая строка:

Сообщение об ошибке с информацией

Если подходящая строка:

Обработанная строка

Информация о том, сколько раз входил в обработанную строку каждый символ*/



/*Добавить в программу из «Задания 3» дополнительный функционал. Помимо обработки строки и подсчёта количества вхождений 
 * каждого символа, необходимо найти в обработанной строке наибольшую подстроку, которая начинается и заканчивается на гласную букву из «aeiouy»

Результат программы:

Если не подходящая строка:

Сообщение об ошибке с информацией

Если подходящая строка:

Обработанная строка

Информация о том, сколько раз входил в обработанную строку каждый символ

Самая длинная подстрока начинающаяся и заканчивающаяся на гласную*/



public class StringProcessor
{
    public static void Main(string[] args)
    {
        Console.Write("Введите строку: ");
        string input = Console.ReadLine();

        var invalidChars = GetInvalidCharacters(input);

        if (invalidChars.Any())
        {
            Console.WriteLine("Ошибка: введены недопустимые символы: " + string.Join(", ", invalidChars));
        }
        else
        {
            string result = Process(input);
            Console.WriteLine("Результат: " + result);

            Console.WriteLine("Статистика повторов символов:");
            PrintCharacterFrequencies(result);

            string vowelSubstring = FindLongestVowelSubstring(result);
            Console.WriteLine("Самая длинная подстрока, начинающаяся и заканчивающаяся на гласную: " + vowelSubstring);
        }
    }

    private static List<char> GetInvalidCharacters(string input)
    {
        return input.Where(c => c < 'a' || c > 'z').Distinct().ToList();
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

    private static void PrintCharacterFrequencies(string text)
    {
        var frequencies = new Dictionary<char, int>();

        foreach (char c in text)
        {
            if (frequencies.ContainsKey(c))
                frequencies[c]++;
            else
                frequencies[c] = 1;
        }

        foreach (var pair in frequencies.OrderBy(p => p.Key))
        {
            Console.WriteLine($"'{pair.Key}': {pair.Value} раз(а)");
        }
    }

    private static string FindLongestVowelSubstring(string text)
    {
        string vowels = "aeiouy";
        int maxLen = 0;
        int startIdx = -1;

        for (int i = 0; i < text.Length; i++)
        {
            if (vowels.Contains(text[i]))
            {
                for (int j = text.Length - 1; j > i; j--)
                {
                    if (vowels.Contains(text[j]))
                    {
                        int len = j - i + 1;
                        if (len > maxLen)
                        {
                            maxLen = len;
                            startIdx = i;
                        }
                        break;
                    }
                }
            }
        }

        return maxLen > 0 ? text.Substring(startIdx, maxLen) : "(нет подходящей подстроки)";
    }
}
