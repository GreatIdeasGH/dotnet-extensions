using System;

namespace GreatIdeas.Extensions
{
    public static class DateTimeOffsetExtensions
    {
        public static int GetCurrentAge(this DateTimeOffset dateTimeOffset)
        {
            var currentDate = DateTime.UtcNow;
            int age = currentDate.Year - dateTimeOffset.Year;

            if (currentDate < dateTimeOffset.AddYears(age))
            {
                age--;
            }

            return age;
        }

        
        /// <summary>  
        /// For calculating age  
        /// </summary>  
        /// <param name="dob">Enter Date of Birth to Calculate the age</param>  
        /// <returns> years, months,days, hours...</returns>  
        public static int CalculateAge(this DateTimeOffset dob)
        {
            DateTimeOffset now = DateTimeOffset.UtcNow; ;
            int years = new DateTime(DateTimeOffset.Now.Subtract(dob).Ticks).Year - 1;
            DateTimeOffset pastYearDate = dob.AddYears(years);
            int months = 0;
            for (int i = 1; i <= 12; i++)
            {
                if (pastYearDate.AddMonths(i) == now)
                {
                    months = i;
                    break;
                }
                else if (pastYearDate.AddMonths(i) >= now)
                {
                    months = i - 1;
                    break;
                }
            }
            int Days = now.Subtract(pastYearDate.AddMonths(months)).Days;
            int Hours = now.Subtract(pastYearDate).Hours;
            int Minutes = now.Subtract(pastYearDate).Minutes;
            int Seconds = now.Subtract(pastYearDate).Seconds;
            // return String.Format("Age: {0} Year(s) {1} Month(s) {2} Day(s) {3} Hour(s) {4} Second(s)",
            //     Years, Months, Days, Hours, Seconds);
            return years;
        }

    }
}
