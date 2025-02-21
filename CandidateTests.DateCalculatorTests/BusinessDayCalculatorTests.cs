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
    public void BusinessDaysBetweenTwoDates_ShouldReturnsZero_WhenDateAreSubsequentDays()
    {
        // Arrange
        var expectedResult = 0;
        var startDate = new DateTime(2025, 2, 3); // Monday
        var endDate = new DateTime(2025, 2, 4); // Tuesday
        var holidays = new List<FixedDayInMonthHoliday>();

        // Act
        var result = _calculator.BusinessDaysBetweenTwoDates(startDate, endDate, holidays);

        // Assert
        result.Should().Be(expectedResult);
    }

    [Fact]
    public void BusinessDaysBetweenTwoDates_ShouldReturnOne_WhenDateBetweenMondayToWednesdayAndNoHolidays()
    {
        // Arrange
        var expectedResult = 1;
        var startDate = new DateTime(2025, 2, 3); // Monday
        var endDate = new DateTime(2025, 2, 5); // Wednesday
        var holidays = new List<FixedDayInMonthHoliday>();

        // Act
        var result = _calculator.BusinessDaysBetweenTwoDates(startDate, endDate, holidays);

        // Assert
        result.Should().Be(expectedResult);
    }

    [Fact]
    public void BusinessDaysBetweenTwoDates_ShouldCalculateProperly_WhenPassingOneWeekWithNoHolidays()
    {
        // Arrange
        var expectedResult = 4;
        var startDate = new DateTime(2025, 2, 3); // Monday
        var endDate = new DateTime(2025, 2, 10); // Monday
        var holidays = new List<FixedDayInMonthHoliday>();

        // Act
        var result = _calculator.BusinessDaysBetweenTwoDates(startDate, endDate, holidays);

        // Assert
        result.Should().Be(expectedResult);
    }

    [Fact]
    public void BusinessDaysBetweenTwoDates_ShouldReturnZero_WhenDateBetweenMondayAndWednesdayWithTuesdayHoliday()
    {
        // Arrange
        var expectedResult = 0;
        var startDate = new DateTime(2025, 2, 3); // Monday
        var endDate = new DateTime(2025, 2, 5); // Wednesday
        var holidays = new List<FixedDayInMonthHoliday> { new() { HolidayName = "Test Holiday", Month = Month.February, Day = 4 } };

        // Act
        var result = _calculator.BusinessDaysBetweenTwoDates(startDate, endDate, holidays);

        // Assert
        result.Should().Be(expectedResult);
    }

    [Fact]
    public void BusinessDaysBetweenTwoDates_ShouldCalculateProperly_WhenOneWeekAndThereIsOneHolidayInBetween()
    {
        // Arrange
        var expectedResult = 3;
        var startDate = new DateTime(2025, 2, 3);
        var endDate = new DateTime(2025, 2, 10);
        var holidays = new List<FixedDayInMonthHoliday> { new() { HolidayName = "Test Holiday", Month = Month.February, Day = 4 } };

        // Act
        var result = _calculator.BusinessDaysBetweenTwoDates(startDate, endDate, holidays);

        // Assert
        result.Should().Be(expectedResult);
    }

    [Fact]
    public void BusinessDaysBetweenTwoDates_ShouldCalculateProperly_WhenPassingAMonthWithInAndOutHolidays()
    {
        // Arrange
        var expectedResult = 21;
        var startDate = new DateTime(2024, 1, 1);
        var endDate = new DateTime(2024, 1, 31);
        var holidays = new List<FixedDayInMonthHoliday>
        {
            new() { HolidayName = "New year's eve", Month = Month.January, Day = 1 },
            new() { HolidayName = "Christmas", Month = Month.December, Day = 25 },
        };

        // Act
        var result = _calculator.BusinessDaysBetweenTwoDates(startDate, endDate, holidays);

        // Assert
        result.Should().Be(expectedResult);
    }
    
    public static TheoryData<DateTime, DateTime, int> WeekDaysOnlyTests =
        new()
        {
            { new DateTime(2024, 10, 07), new DateTime(2024, 10, 09), 1 },
            { new DateTime(2024, 10, 05), new DateTime(2024, 10, 14), 5 },
            { new DateTime(2024, 10, 07), new DateTime(2025, 01, 01), 61 },
            { new DateTime(2024, 10, 07), new DateTime(2024, 10, 05), 0 }
        };

    [Theory]
    [MemberData(nameof(WeekDaysOnlyTests))]
    public void BusinessDaysBetweenTwoDates_ShouldReturnExpectedResult_WhenNotUsingHolidays(DateTime firstDate, DateTime secondDate,
        int expectedDays)
    {
        // Act
        var result = _calculator.BusinessDaysBetweenTwoDates(firstDate, secondDate, new List<FixedDayInMonthHoliday>());

        // Assert
        result.Should().Be(expectedDays);
    }

    public static TheoryData<DateTime, DateTime, int> WeekDaysWithHolidaysTests =
        new()
        {
            { new DateTime(2024, 10, 7), new DateTime(2024, 10, 09), 1 },
            { new DateTime(2024, 12, 24), new DateTime(2024, 12, 27), 0 },
            { new DateTime(2024, 10, 7), new DateTime(2025, 01, 01), 59 }
        };

    [Theory]
    [MemberData(nameof(WeekDaysWithHolidaysTests))]
    public void BusinessDaysBetweenTwoDates_ShouldReturnExpectedResult_WhenAddingHolidays(DateTime firstDate,
        DateTime secondDate, int expectedDays)
    {
        // Arrange
        var holidays = new List<FixedDayInMonthHoliday>
        {
            new() { HolidayName = "Christmas", Month = Month.December, Day = 25 },
            new() { HolidayName = "Boxing Day", Month = Month.December, Day = 26 },
            new() { HolidayName = "New Years", Month = Month.January, Day = 1 }
        };

        // Act
        var result = _calculator.BusinessDaysBetweenTwoDates(firstDate, secondDate, holidays);

        // Assert
        result.Should().Be(expectedDays);
    }
}