namespace TimeConverterCSharp
{
    public interface IConverter
    {
        bool IsValidDateTime(int year, int month, int day, int hours, int minutes, int seconds);
        string CurrentDateTime(int year, int month, int day, int hours, int minutes, int seconds);
        string FormatRelative(ref DateTime target);
    }
}
