using CandidateTests.DateCalculator;
using CandidateTests.DateCalculator.Models;

namespace CandidateTests.DateCalculatorTests
{
    public class SubstituteBusinessDateCalculatorTests
    {
        private readonly BusinessDayCalculator _businessDayCalculator;

        public SubstituteBusinessDateCalculatorTests()
        {
            _businessDayCalculator = new BusinessDayCalculator();
        }
        [Fact]
        public void SubstituteBusinessDateCalculator_ShouldMoveWeekendHoliday_WhenUsingSubstitute()
        {
            //Arrange
            int expectedResult = 4;
            DateTime StartDate = new DateTime(2025, 10, 20);
            DateTime EndDate = new DateTime(2025, 10, 28);

            var holidays = new List<IPublicHoliday> { new SubstituteDayInMonthHoliday { HolidayName = "Diwali", Day = 25, Month = Month.October } };

            //Act
            var result = _businessDayCalculator.BusinessDaysBetweenTwoDates(StartDate, EndDate, holidays);

            //Assert
            Assert.Equal(expectedResult, result);

        }


        [Fact]
        public void SubstituteBusinessDateCalculator_ShouldNotMoveWeekendHoliday_WhenNotUsingSubstitute()
        {
            //Arrange
            int expectedResult = 5;
            DateTime StartDate = new DateTime(2025, 10, 20);
            DateTime EndDate = new DateTime(2025, 10, 28);

            var holidays = new List<IPublicHoliday> { new FixedDayInMonthHoliday { HolidayName = "Diwali", Day = 25, Month = Month.October } };

            //Act
            var result = _businessDayCalculator.BusinessDaysBetweenTwoDates(StartDate, EndDate, holidays);

            //Assert
            Assert.Equal(expectedResult, result);

        }


        [Fact]
        public void SubstituteBusinessDateCalculator_ShouldThrowException_WhenDayandMonthNotSet()
        {
            //Arrange
            int expectedResult = 5;
            DateTime StartDate = new DateTime(2025, 10, 20);
            DateTime EndDate = new DateTime(2025, 10, 28);

            var holidays = new List<IPublicHoliday> { new SubstituteDayInMonthHoliday { HolidayName = "Diwali" } };

            //Act and Assert
            Assert.Throws<System.ArgumentOutOfRangeException>(() => _businessDayCalculator.BusinessDaysBetweenTwoDates(StartDate, EndDate, holidays));

        }
    }
}
