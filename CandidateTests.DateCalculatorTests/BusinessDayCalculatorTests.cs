using CandidateTests.DateCalculator.Application;
using CandidateTests.DateCalculator.Domain;
using FluentAssertions;

namespace CandidateTests.DateCalculatorTests;

public class BusinessDayCalculatorTests
{
    private readonly BusinessDayCalculator _calculator;

    public BusinessDayCalculatorTests()
    {
        _calculator = new BusinessDayCalculator();
    }

    [Fact]
    public void BusinessDaysBetweenTwoDates_ShowReturnsZero_WhenDateAreSubsequentDays()
    {
        // Arrange
        var expectedResult = 0;
        var startDate = new DateTime(2025, 2, 3); // Monday
        var endDate = new DateTime(2025, 2, 4); // Tuesday
        var holidays = new List<SameDayHoliday>();


        // Act
        int result = _calculator.BusinessDaysBetweenTwoDates(startDate, endDate, holidays);

        // Assert
        result.Should().Be(expectedResult);
    }

    [Fact]
    public void BusinessDaysBetweenTwoDates_ShowReturnsOne_WhenDateBetweenMondayToWednesdayAndNoHolidays()
    {
        // Arrange
        var expectedResult = 1;
        var startDate = new DateTime(2025, 2, 3); // Monday
        var endDate = new DateTime(2025, 2, 5); // Wednesday
        var holidays = new List<SameDayHoliday>();


        // Act
        int result = _calculator.BusinessDaysBetweenTwoDates(startDate, endDate, holidays);

        // Assert
        result.Should().Be(expectedResult);
    }

    [Fact]
    public void BusinessDaysBetweenTwoDates_ShowReturns4_WhenDateBetweenMondayToMondayWithNoHolidays()
    {
        // Arrange
        var expectedResult = 4;
        var startDate = new DateTime(2025, 2, 3); // Monday
        var endDate = new DateTime(2025, 2, 10); // Monday
        var holidays = new List<SameDayHoliday>();

        // Act
        int result = _calculator.BusinessDaysBetweenTwoDates(startDate, endDate, holidays);

        // Assert
        result.Should().Be(expectedResult);
    }

    [Fact]
    public void BusinessDaysBetweenTwoDates_ShowReturnsZero_WhenDateBetweenMondayAndWednesdayAndHolidaysFallOnTuesday()
    {
        // Arrange
        var expectedResult = 0;
        var startDate = new DateTime(2025, 2, 3); // Monday
        var endDate = new DateTime(2025, 2, 5); // Wednesday
        var holidays = new List<SameDayHoliday>
        {
            new() { HolidayName = "Test Holiday", Month = Month.February, Day = 4 }
        };


        // Act
        int result = _calculator.BusinessDaysBetweenTwoDates(startDate, endDate, holidays);

        // Assert
        result.Should().Be(expectedResult);
    }

    [Fact]
    public void BusinessDaysBetweenTwoDates_ShowReturns3_WhenDateBetweenMondayToMondayAndThereIsOneHolidayInBetween()
    {
        // Arrange
        var expectedResult = 3;
        var startDate = new DateTime(2025, 2, 3); // Monday
        var endDate = new DateTime(2025, 2, 10); // Monday
        var holidays = new List<SameDayHoliday>
        {
            new() { HolidayName = "Test Holiday", Month = Month.February, Day = 4 }
        };

        // Act
        int result = _calculator.BusinessDaysBetweenTwoDates(startDate, endDate, holidays);

        // Assert
        result.Should().Be(expectedResult);
    }

    public static TheoryData<DateTime, DateTime, int> WeekDaysCaseTests =
        new()
        {
            { new DateTime(2013, 10, 07), new DateTime(2013, 10, 09), 1 },
            { new DateTime(2013, 10, 05), new DateTime(2013, 10, 14), 5 },
            { new DateTime(2013, 10, 07), new DateTime(2014, 01, 01), 61 },
            { new DateTime(2013, 10, 07), new DateTime(2013, 10, 05), 0 }
        };

    [Theory, MemberData(nameof(WeekDaysCaseTests))]
    public void BusinessDaysBetweenTwoDates_ShouldReturnDaysAsExpected(DateTime firstDate, DateTime secondDate,
        int expectedDays)
    {
        // Act
        int result = _calculator.BusinessDaysBetweenTwoDates(firstDate, secondDate, new List<SameDayHoliday>());

        // Assert
        result.Should().Be(expectedDays);
    }

    public static TheoryData<DateTime, DateTime, int> WeekDaysWithHolidaysCaseTests =
        new()
        {
            { new DateTime(2013, 10, 7), new DateTime(2013, 10, 09), 1 },
            { new DateTime(2013, 12, 24), new DateTime(2013, 12, 27), 0 },
            { new DateTime(2013, 10, 7), new DateTime(2014, 01, 01), 59 },
        };

    [Theory, MemberData(nameof(WeekDaysWithHolidaysCaseTests))]
    public void BusinessDaysBetweenTwoDatesWithHolidays_ShouldReturnDaysAsExpected(DateTime firstDate,
        DateTime secondDate, int expectedDays)
    {
        // Arrange
        var holidays = new List<SameDayHoliday>
        {
            new() { HolidayName = "Christmas", Month = Month.December, Day = 25 },
            new() { HolidayName = "Boxing Day", Month = Month.December, Day = 26 },
            new() { HolidayName = "New Years", Month = Month.January, Day = 1 }
        };

        // Act
        int result = _calculator.BusinessDaysBetweenTwoDates(firstDate, secondDate, holidays);

        // Assert
        result.Should().Be(expectedDays);
    }
}