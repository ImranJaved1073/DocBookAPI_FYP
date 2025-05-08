using System.Globalization;

namespace DocBookAPI.Helpers
{
    public static class TimeSlotHelper
    {
        public static List<string> BreakTimeRange(string rangeString, int interval = 30)
        {
            //first of all convert rangeString to a 12 hour format from 22:00 - 02:00 to 10PM - 2AM

            if (string.IsNullOrEmpty(rangeString))
                throw new ArgumentNullException(nameof(rangeString));

            string[] parts = rangeString.Split('-');
            if (parts.Length != 2)
                throw new ArgumentException("Invalid time range format. Expected 'StartTime-EndTime'");

            string startTimeString = parts[0].Trim().Replace(" ", "");
            string endTimeString = parts[1].Trim().Replace(" ", "");

            var startTime = ParseTime(startTimeString);
            var endTime = ParseTime(endTimeString);

            List<string> slots = new List<string>();
            var currentTime = startTime;

            // Adjust endTime if it's the next day
            if (startTime > endTime)
                endTime = endTime.AddDays(1);

            while (currentTime < endTime)
            {
                var nextTime = currentTime.AddMinutes(interval);
                if (nextTime > endTime)
                    nextTime = endTime;

                slots.Add($"{currentTime:hh:mm tt}-{nextTime:hh:mm tt}");
                currentTime = nextTime;
            }

            return slots;
        }

        private static DateTime ParseTime(string timeString)
        {
            if (DateTime.TryParseExact(timeString, new[] { "H:mm", "h:mmtt" }, CultureInfo.InvariantCulture, DateTimeStyles.None, out var result))
            {
                return result;
            }
            else
            {
                throw new ArgumentException("Invalid time format. Expected 'H:mm' or 'h:mmtt'");
            }
        }
    }
}