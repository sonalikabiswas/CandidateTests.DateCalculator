using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CandidateTests.DateCalculator.Models;

namespace CandidateTests.DateCalculatorTests
{
    public class SubstituteBusinessDayInMonthHolidayTests
    {

        [Fact]
        public void SubstituteBusinessDayInMonthHoliday_ShiftsHolidaytoPrevDay_WhenHolidayOnSaturday()
        {
            //Arrange
            DateTime expectedHolidayDate = new DateTime(2025, 10, 24);
    

            // Act
            IPublicHoliday pH = new SubstituteDayInMonthHoliday { Day =25, Month=Month.October, HolidayName ="Haloween" };
            var actualHolidayDate = pH.GetHolidayDate(2025);
            
            //Assert
            Assert.Equal(expectedHolidayDate, actualHolidayDate);

        }



        [Fact]
        public void SubstituteBusinessDayInMonthHoliday_ShiftsHolidaytoNextDay_WhenHolidayOnSunday()
        {
            //Arrange
            DateTime expectedHolidayDate = new DateTime(2025, 10, 27);


            // Act
            IPublicHoliday pH = new SubstituteDayInMonthHoliday { Day = 26, Month = Month.October, HolidayName = "Halloween" };
            var actualHolidayDate = pH.GetHolidayDate(2025);

            //Assert
            Assert.Equal(expectedHolidayDate, actualHolidayDate);

        }

        [Fact]
        public void SubstituteBusinessDayInMonthHoliday_ShiftsHolidaytoNextYear_WhenHolidayOnYearEndandSunday()
        {
            //Arrange
            DateTime expectedHolidayDate = new DateTime(2018, 01, 01);


            // Act
            IPublicHoliday pH = new SubstituteDayInMonthHoliday { Day = 31, Month = Month.December, HolidayName = "New Year" };
            var actualHolidayDate = pH.GetHolidayDate(2017);

            //Assert
            Assert.Equal(expectedHolidayDate, actualHolidayDate);

        }
    }
}
