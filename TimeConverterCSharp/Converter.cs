namespace TimeConverterCSharp
{
    public class Converter : IConverter
    {
        public bool IsValidDateTime(int year, int month, int day, int hours, int minutes, int seconds)
        {
            if (year < 1900 || year > 9999) return false;
            if (month < 1 || month > 12) return false;
            if (day < 1 || day > DateTime.DaysInMonth(year, month)) return false;
            if (hours < 0 || hours > 23) return false;
            if (minutes < 0 || minutes > 59) return false;
            if (seconds < 0 || seconds > 59) return false;
            return true;
        }

        public string CurrentDateTime(int year, int month, int day, int hours, int minutes, int seconds)
        {
            return $"{day:D2}.{month:D2}.{year} {hours:D2}:{minutes:D2}:{seconds:D2}";
        }

        public string FormatRelative(ref DateTime target)
        {
            DateTime targetLocal = DateTime.SpecifyKind(target, DateTimeKind.Local);
            DateTime nowLocal = DateTime.Now;

            DateTimeOffset targetOffset = new DateTimeOffset(targetLocal);
            DateTimeOffset nowOffset = new DateTimeOffset(nowLocal);

            long totalDiff = (long)(targetOffset - nowOffset).TotalSeconds;
            if (totalDiff == 0) return "сейчас";

            bool future = totalDiff > 0;
            DateTime earlier = future ? nowLocal : targetLocal;
            DateTimeOffset laterOffset = future ? targetOffset : nowOffset;

            int years = 0;
            while (new DateTimeOffset(earlier.AddYears(1)) <= laterOffset)
            {
                earlier = earlier.AddYears(1);
                years++;
            }

            int months = 0;
            while (new DateTimeOffset(earlier.AddMonths(1)) <= laterOffset)
            {
                earlier = earlier.AddMonths(1);
                months++;
            }

            TimeSpan rem = laterOffset - new DateTimeOffset(earlier);
            int days = rem.Days;
            int hours = rem.Hours;
            int minutes = rem.Minutes;
            int seconds = rem.Seconds;

            var parts = new List<string>();
            if (years > 0) parts.Add($"{years} {Plural(years, "год", "года", "лет")}");
            if (months > 0) parts.Add($"{months} {Plural(months, "месяц", "месяца", "месяцев")}");
            if (days > 0) parts.Add($"{days} {Plural(days, "день", "дня", "дней")}");
            if (hours > 0) parts.Add($"{hours} {Plural(hours, "час", "часа", "часов")}");
            if (minutes > 0) parts.Add($"{minutes} {Plural(minutes, "минута", "минуты", "минут")}");
            if (seconds > 0) parts.Add($"{seconds} {Plural(seconds, "секунда", "секунды", "секунд")}");

            if (parts.Count == 0) parts.Add("0 секунд");

            string result = string.Join(" ", parts);
            return future ? $"будет через {result}" : $"было {result} назад";
        }

        private string Plural(long n, string one, string few, string many)
        {
            long m100 = n % 100;
            if (m100 >= 11 && m100 <= 14) return many;
            long m10 = n % 10;
            if (m10 == 1) return one;
            if (m10 >= 2 && m10 <= 4) return few;
            return many;
        }
    }
}
