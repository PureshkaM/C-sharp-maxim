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



/*Добавить в программу из «Задания 4» дополнительный функционал. Помимо всех предыдущих результатов необходимо также возвращать отсортированную обработанную строку. 
 * Должно быть реализовано два алгоритма сортировки – Быстрая сортировка (Quicksort) и Сортировка деревом (Tree sort). Текущий алгоритм сортировки должен выбираться пользователем.

Результат программы:

Если не подходящая строка:

Сообщение об ошибке с информацией

Если подходящая строка:

Обработанная строка

Информация о том, сколько раз входил в обработанную строку каждый символ

Самая длинная подстрока начинающаяся и заканчивающаяся на гласную

Отсортированная обработанная строка*/



/*Добавить в программу из «Задания 5» дополнительный функционал. 
 * Помимо всех предыдущих результатов, программа также должна получать случайно сгенерированное число, которое будет меньше, чем число символов в обработанной строке, 
 * и удалять символ в той позиции, номер которой вернёт случайный генератор. Получать число необходимо через удалённый API 
 * (например http://www.randomnumberapi.com или любое со схожим функционалом). Если удалённое API в момент работы программы возвращает какую-то непредвиденную ошибку, 
 * то необходимо получать случайное число средствами .NET


Результат программы:

Если не подходящая строка

Сообщение об ошибке с информацией

Если подходящая строка

Обработанная строка

Информация о том, сколько раз входил в обработанную строку каждый символ

Самая длинная подстрока начинающаяся и заканчивающаяся на гласную

Отсортированная обработанная строка

«Урезанная» обработанная строка – обработанная строка без одного символа*/



public class StringProcessor
{
    public static async Task Main(string[] args)
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

            Console.Write("Выберите алгоритм сортировки (1 - Быстрая сортировка, 2 - Сортировка деревом): ");
            string choice = Console.ReadLine();
            string sorted = choice == "2" ? TreeSort(result) : QuickSort(result);

            Console.WriteLine("Отсортированная обработанная строка: " + sorted);

            int randomIndex = await GetRandomIndexAsync(result.Length);
            string shortened = RemoveCharAt(result, randomIndex);
            Console.WriteLine($"\"Урезанная\" обработанная строка (удалён символ по индексу {randomIndex}): {shortened}");
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

    private static string QuickSort(string text)
    {
        char[] array = text.ToCharArray();
        QuickSortRecursive(array, 0, array.Length - 1);
        return new string(array);
    }

    private static void QuickSortRecursive(char[] array, int left, int right)
    {
        if (left >= right) return;

        int pivotIndex = Partition(array, left, right);
        QuickSortRecursive(array, left, pivotIndex - 1);
        QuickSortRecursive(array, pivotIndex + 1, right);
    }

    private static int Partition(char[] array, int left, int right)
    {
        char pivot = array[right];
        int i = left - 1;

        for (int j = left; j < right; j++)
        {
            if (array[j] <= pivot)
            {
                i++;
                (array[i], array[j]) = (array[j], array[i]);
            }
        }

        (array[i + 1], array[right]) = (array[right], array[i + 1]);
        return i + 1;
    }

    private class TreeNode
    {
        public char Value;
        public TreeNode Left;
        public TreeNode Right;

        public TreeNode(char value)
        {
            Value = value;
        }

        public void Insert(char value)
        {
            if (value <= Value)
            {
                if (Left == null) Left = new TreeNode(value);
                else Left.Insert(value);
            }
            else
            {
                if (Right == null) Right = new TreeNode(value);
                else Right.Insert(value);
            }
        }

        public void InOrder(List<char> result)
        {
            Left?.InOrder(result);
            result.Add(Value);
            Right?.InOrder(result);
        }
    }

    private static string TreeSort(string text)
    {
        if (string.IsNullOrEmpty(text)) return "";

        TreeNode root = new TreeNode(text[0]);
        for (int i = 1; i < text.Length; i++)
        {
            root.Insert(text[i]);
        }

        List<char> sorted = new List<char>();
        root.InOrder(sorted);
        return new string(sorted.ToArray());
    }

    private static async Task<int> GetRandomIndexAsync(int max)
    {
        try
        {
            using HttpClient client = new HttpClient();
            string url = $"http://www.randomnumberapi.com/api/v1.0/random?min=0&max={max - 1}&count=1";
            var response = await client.GetAsync(url);
            response.EnsureSuccessStatusCode();

            string content = await response.Content.ReadAsStringAsync();

            var numbers = System.Text.Json.JsonSerializer.Deserialize<int[]>(content);
            if (numbers != null && numbers.Length > 0)
            {
                Console.WriteLine("Получено случайное число через API.");
                return numbers[0];
            }

            throw new Exception("Неверный формат ответа API");
        }
        catch
        {
            Console.WriteLine("Не удалось получить случайное число через API. Используется локальный генератор.");
            return new Random().Next(0, max);
        }
    }

    private static string RemoveCharAt(string text, int index)
    {
        return text.Remove(index, 1);
    }
}

