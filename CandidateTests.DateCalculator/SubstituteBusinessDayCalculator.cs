using System.Security.Cryptography.X509Certificates;
using CandidateTests.DateCalculator.Models;

namespace CandidateTests.DateCalculator;

public class SubstituteBusinessDayCalculator : IBusinessDayCalculator
{
    public readonly IBusinessDayCalculator _businessDayCalculator;
    public SubstituteBusinessDayCalculator(IBusinessDayCalculator businessDayCalculator)
    {
        _businessDayCalculator = businessDayCalculator;
    }
    public int BusinessDaysBetweenTwoDates(DateTime firstDate, DateTime secondDate, IList<IPublicHoliday> publicHolidays)
    {
        //IList<IPublicHoliday> substitutePublicHolidays = GetSubstitutePulicHoliday(firstDate, secondDate, publicHolidays);
        return _businessDayCalculator.BusinessDaysBetweenTwoDates(firstDate, secondDate, publicHolidays);


    }

}