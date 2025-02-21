namespace CandidateTests.DateCalculator.Domain;

public class FixedDayInMonthHoliday
{
    public string HolidayName { get; init; } = "Fixed day in month holiday";

    public int Day { get; init; }

    public Month Month { get; init; }

    public DateTime GetHolidayDate(int year)
    {
        return new DateTime(year, (int) Month, Day);
    }
}