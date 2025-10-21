using CandidateTests.DateCalculator.Models;
using NLog;

namespace CandidateTests.DateCalculator;

public class BusinessDayCalculator : IBusinessDayCalculator
{

    private static readonly Logger _log = LogManager.GetCurrentClassLogger();
    public int BusinessDaysBetweenTwoDates(DateTime firstDate, DateTime secondDate, IList<IPublicHoliday> publicHolidays)
    {
        try
        {
            var businessDays = 0;
            var currentDate = firstDate.AddDays(1);
            var holidays = GetAllHolidays(firstDate, secondDate, publicHolidays);

            while (currentDate.Date < secondDate.Date)
            {
                if (!currentDate.IsWeekend()
                    && holidays.All(holiday => holiday != currentDate))
                {
                    businessDays++;
                }

                currentDate = currentDate.AddDays(1);
            }

            return businessDays;
        }
        catch (Exception ex)
        {
            _log.Error(ex, "Error calculating buisness days , Exception : " + ex.Message + " Inner Exception " + ex.InnerException);
            throw;
        }
    }

    private List<DateTime> GetAllHolidays(DateTime firstDate, DateTime secondDate, IList<IPublicHoliday> publicHolidays)
    {
        try
        {
            var holidays = new List<DateTime>();
            for (var currentYear = firstDate.Year; currentYear <= secondDate.Year; currentYear++)
            {
                foreach (var holiday in publicHolidays)
                {
                    holidays.Add(holiday.GetHolidayDate(currentYear));
                }
            }

            return holidays;
        }
        catch (Exception ex)
        {
            _log.Error(ex, "Error creating holiday list , Exception : " + ex.Message + " Inner Exception " + ex.InnerException);
            throw;

        }
    }
}