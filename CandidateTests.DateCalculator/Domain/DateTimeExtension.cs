namespace CandidateTests.DateCalculator.Domain;

public static class DateTimeExtension
{
    public static bool IsWeekend(this DateTime date)
    {
        return date.DayOfWeek is DayOfWeek.Saturday or DayOfWeek.Sunday;
    }
}