using CandidateTests.DateCalculator.Domain;

namespace CandidateTests.DateCalculator.Application;

public class BusinessDayCalculator : IBusinessDayCalculator
{
    public int BusinessDaysBetweenTwoDates(DateTime firstDate, DateTime secondDate,
        IList<SameDayHoliday> publicHolidays)
    {
        int businessDays = 0;
        DateTime currentDate = firstDate.AddDays(1);
        DateTime[] sameDayHolidaysForPeriod = GetSameDayHolidaysForDate(firstDate, secondDate, publicHolidays);

        while (currentDate.Date < secondDate.Date)
        {
            if (!currentDate.IsWeekend()
                && !sameDayHolidaysForPeriod.Any(sameDayHoliday => sameDayHoliday == currentDate))
            {
                businessDays++;
            }

            currentDate = currentDate.AddDays(1);
        }

        return businessDays;
    }

    private static DateTime[] GetSameDayHolidaysForDate(DateTime firstDate, DateTime secondDate,
        IList<SameDayHoliday> publicHolidays)
    {
        var sameDayHolidays = new List<DateTime>();
        for (var currentYear = firstDate.Year; currentYear <= secondDate.Year; currentYear++)
        {
            foreach (var publicHoliday in publicHolidays)
            {
                sameDayHolidays.Add(publicHoliday.GetHolidayDate(currentYear));
            }
        }

        return sameDayHolidays.ToArray();
    }
}