namespace CandidateTests.DateCalculator.Models;

public static class DateTimeExtension
{
    public static bool IsWeekend(this DateTime date)
    {
        return date.DayOfWeek is DayOfWeek.Saturday or DayOfWeek.Sunday;
    }
}