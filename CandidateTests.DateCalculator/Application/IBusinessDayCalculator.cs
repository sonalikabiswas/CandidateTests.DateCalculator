using CandidateTests.DateCalculator.Domain;

namespace CandidateTests.DateCalculator.Application;

public interface IBusinessDayCalculator
{
    int BusinessDaysBetweenTwoDates(DateTime firstDate, DateTime secondDate, IList<SameDayHoliday> publicHolidays);
}