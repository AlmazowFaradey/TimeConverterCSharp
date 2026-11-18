using System.Globalization;
using TimeConverterCSharp;

IConverter converter = new Converter();

while (true)
{
    Console.WriteLine("Введите дату и время в формате 'YYYY-MM-DD HH:MM:SS' или 'exit' для выхода:\n");
    string input = Console.ReadLine()!;

    if (input.ToLower() == "exit")
        break;

    if (!DateTime.TryParseExact(input, "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime dt))
    {
        Console.WriteLine("ERROR: Incorrect format. Expected YYYY-MM-DD HH:MM:SS.\n");
        continue;
    }

    int year = dt.Year;
    int month = dt.Month;
    int day = dt.Day;
    int hour = dt.Hour;
    int minute = dt.Minute;
    int second = dt.Second;

    if (!converter.IsValidDateTime(year, month, day, hour, minute, second))
    {
        Console.WriteLine("ERROR: Current date/time is not exists\n");
        continue;
    }

    Console.WriteLine(converter.CurrentDateTime(year, month, day, hour, minute, second));
    Console.WriteLine(converter.FormatRelative(ref dt));
}

Console.WriteLine("Bye!");
return 0;
