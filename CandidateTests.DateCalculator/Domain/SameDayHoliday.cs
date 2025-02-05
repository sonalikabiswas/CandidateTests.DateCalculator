namespace CandidateTests.DateCalculator.Domain;

public class SameDayHoliday
{
    public string HolidayName { get; init; }
    public int Day { get; init; }
    public Month Month { get; init; }

    public DateTime GetHolidayDate(int year)
    {
        return new DateTime(year, (int)Month, Day);
    }
}