using CandidateTests.DateCalculator.Domain;

namespace CandidateTests.DateCalculator.Application;

public class BusinessDayCalculator : IBusinessDayCalculator
{
    public int BusinessDaysBetweenTwoDates(DateTime firstDate, DateTime secondDate, IList<FixedDayInMonthHoliday> publicHolidays)
    {
        var businessDays = 0;
        var currentDate = firstDate.AddDays(1);
        var holidays = GetAllHolidays(firstDate, secondDate, publicHolidays);

        while (currentDate.Date < secondDate.Date)
        {
            if (!currentDate.IsWeekend()
                && holidays.All(sameDayHoliday => sameDayHoliday != currentDate))
            {
                businessDays++;
            }

            currentDate = currentDate.AddDays(1);
        }

        return businessDays;
    }

    private List<DateTime> GetAllHolidays(DateTime firstDate, DateTime secondDate, IList<FixedDayInMonthHoliday> publicHolidays)
    {
        var sameDayHolidays = new List<DateTime>();
        for (var currentYear = firstDate.Year; currentYear <= secondDate.Year; currentYear++)
        {
            foreach (var holiday in publicHolidays)
            {
                sameDayHolidays.Add(holiday.GetHolidayDate(currentYear));
            }
        }

        return sameDayHolidays;
    }
}