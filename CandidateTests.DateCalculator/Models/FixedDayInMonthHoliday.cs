namespace CandidateTests.DateCalculator.Domain;

public class FixedDayInMonthHoliday : IPublicHoliday
{
    public string HolidayName { get; set; }
    
    public int Day { get; init; }

    public Month Month { get; init; }

    public DateTime GetHolidayDate(int year)
    {
        return new DateTime(year, (int) Month, Day);
    }
}