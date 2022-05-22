using System;

namespace TicketSystemApi.Helpers.ImageFormat
{
    public static class TimeFormat
    {
        public static string datetimechange(string date)
        {
            DateTime dateTime = Convert.ToDateTime(date);
            return dateTime.ToString("hh:mm tt");
        }
    }
}
