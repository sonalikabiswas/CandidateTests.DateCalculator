using CandidateTests.DateCalculator.Models;

namespace CandidateTests.DateCalculator;

public interface IBusinessDayCalculator
{
    int BusinessDaysBetweenTwoDates(DateTime firstDate, DateTime secondDate, IList<IPublicHoliday> publicHolidays);
}