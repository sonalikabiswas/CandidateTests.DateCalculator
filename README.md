
# Business Day Calculator

The **Business Day Calculator** is an application designed to calculate the number of business days between two given dates. It takes into account weekends and public holidays, ensuring the result only includes weekdays that are not holidays.

### Features
- **Business Day Calculation**: Calculates business days between two dates.
- **Weekend Exclusion**: Excludes weekends (Saturday and Sunday) from the count.
- **Public Holiday Handling**: Excludes public holidays from the business day count.
- **Flexible Holiday List**: Easily configurable to add different public holidays.

### Key Classes

- **`BusinessDayCalculator`**: The main class responsible for calculating business days between two dates.
- **`IBusinessDayCalculator`**: The interface that defines the method to calculate business days between two dates.
- **`SameDayHoliday`**: Represents a public holiday that occurs on the same day every year.

### Installation

To use this application, simply add the necessary project references, or clone the repository and build the project.

### Example Usage

Here’s how to use the `BusinessDayCalculator` to compute business days between two dates:

```csharp
using CandidateTests.DateCalculator.Application;
using CandidateTests.DateCalculator.Domain;
using System;
using System.Collections.Generic;

class Program
{
    static void Main()
    {
        // List of public holidays (e.g., New Year's Day and Christmas Day)
        var holidays = new List<SameDayHoliday>
        {
            new SameDayHoliday(1, Month.January),   // New Year's Day
            new SameDayHoliday(25, Month.December)  // Christmas Day
        };

        // Instantiate the BusinessDayCalculator
        var businessDayCalculator = new BusinessDayCalculator();

        // Define the date range
        DateTime startDate = new DateTime(2023, 12, 1);
        DateTime endDate = new DateTime(2023, 12, 31);

        // Calculate business days
        int businessDays = businessDayCalculator.BusinessDaysBetweenTwoDates(startDate, endDate, holidays);

        // Output the result
        Console.WriteLine($"Business days between {startDate.ToShortDateString()} and {endDate.ToShortDateString()}: {businessDays}");
    }
}
```

### Input
- **`firstDate`**: The starting date of the range.
- **`secondDate`**: The ending date of the range.
- **`publicHolidays`**: A list of `SameDayHoliday` objects, each containing a `Day` (int) and `Month` (enum).

### Output
The method returns an integer representing the number of business days between the two provided dates, excluding weekends and the specified public holidays.

### Example Output
```
Business days between 12/1/2023 and 12/31/2023: 22
```

### How It Works

1. **Excludes Weekends**: The function excludes Saturdays and Sundays by checking if the current date is a weekend.
2. **Excludes Public Holidays**: Public holidays are retrieved for the specified date range, and any date matching a holiday is excluded from the count.
3. **Iterates Over Date Range**: The function loops through all dates between `firstDate` and `secondDate` and checks each date for weekends or holidays.

### Testing

The project includes various unit tests that validate the behavior of the `BusinessDayCalculator`, ensuring that:

- Business days are correctly calculated.
- Weekends are properly excluded.
- Public holidays are properly handled.
- Edge cases like empty holiday lists or single-day ranges are managed.

You can run the tests using a test framework like NUnit or XUnit to ensure the application is working as expected.

### Contributing

If you'd like to contribute to the development of this application, follow these steps:

1. Fork the repository.
2. Clone your forked repository to your local machine.
3. Create a new branch for your feature or bug fix.
4. Make your changes and write tests if applicable.
5. Push the changes to your forked repository.
6. Submit a pull request with a clear explanation of the changes you made.

### License

This project is licensed under the MIT License. See the [LICENSE](LICENSE) file for more details.
