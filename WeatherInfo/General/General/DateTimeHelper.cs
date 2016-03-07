using System;

namespace Common
{
    public class DateTimeHelper
    {
        public string GetTimeText(DateTime postedTime, DateTime currentTime)
        {
            double roundedDifference;
            TimeSpan timeSpan = currentTime - postedTime;
            if (timeSpan.TotalMinutes < 1)
            {
                double totalSeconds = timeSpan.TotalSeconds;
                roundedDifference = GetRoundedDifference(totalSeconds, 1);
                return roundedDifference == 1 ? "1 second ago" : roundedDifference.ToString() + " seconds ago";
            }
            if (timeSpan.TotalHours < 1)
            {
                double totalMinutes = timeSpan.TotalMinutes;
                roundedDifference = GetRoundedDifference(totalMinutes, 1);
                return roundedDifference == 1 ? "1 minute ago" : roundedDifference.ToString() + " minutes ago";
            }
            if (timeSpan.TotalDays < 1)
            {
                double totalHours = timeSpan.TotalHours;
                roundedDifference = GetRoundedDifference(totalHours, 1);
                return roundedDifference == 1 ? "1 hour ago" : roundedDifference.ToString() + " hours ago";
            }
            double totalDays = timeSpan.TotalDays;
            if (totalDays < 30)
            {
                roundedDifference = GetRoundedDifference(totalDays, 1);
                return roundedDifference == 1 ? "1 day ago" : roundedDifference.ToString() + " days ago";
            }
            if (totalDays < 365)
            {
                roundedDifference = GetRoundedDifference(totalDays, 30);
                return roundedDifference == 1 ? "1 month ago" : roundedDifference.ToString() + " months ago";
            }

            roundedDifference = GetRoundedDifference(timeSpan.TotalDays, 365);
            return roundedDifference == 1 ? "1 year ago" : roundedDifference.ToString() + " years ago";
        }

        private double GetRoundedDifference(double totalTimeUnits, int basicUnit)
        {
            if (totalTimeUnits <= 0)
                totalTimeUnits = 1;

            return Math.Round(totalTimeUnits / basicUnit, MidpointRounding.AwayFromZero);
        }
    }
}

