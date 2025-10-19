

namespace CandidateTests.DateCalculator.Models;

public class SubstituteDayInMonthHoliday : IPublicHoliday
{
    public string? HolidayName { get; set; }

    public int Day { get; init; }

    public Month Month { get; init; }


    /// <summary>
    /// Subsititutes the holiday date to the nearest available Working date
    /// </summary>
    /// <param name="year">The year to calculate the holiday date for</param>
    /// <returns>Substituted holiday date if weekend or the same day </returns>
    public DateTime GetHolidayDate(int year)
    { 
        DateTime fixedHoliday = new DateTime(year, (int) Month, Day);

        if (!fixedHoliday.IsWeekend())
            return fixedHoliday;

        // Search outward from the fixed date: check previous day and next day
        // at increasing distance; return the first non-weekend found.
        for (int offset = 1; ; offset++)
        {
            DateTime prev = fixedHoliday.AddDays(-offset);
            if (!prev.IsWeekend())
                return prev;

            DateTime next = fixedHoliday.AddDays(offset);
            if (!next.IsWeekend())
                return next;
        }
    }
}