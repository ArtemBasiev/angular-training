using System;
using bizapps_test.MVC.Interfaces;

namespace bizapps_test.MVC.Util
{
    public static class CommentDateGenerator
    {
        public static string GetDateString(DateTime inputDate)
        {
            TimeSpan dateDeference = DateTime.Now - inputDate;
            if ((dateDeference.TotalMinutes <= 1) && (dateDeference.TotalSeconds > 0))
            {
               return "Now";
            }

            if ((dateDeference.TotalMinutes > 1) && (dateDeference.TotalMinutes < 60))
            {
                return SeveralMinutesDateString(dateDeference.TotalMinutes);
            }

            if ((dateDeference.TotalHours >= 1) && (dateDeference.TotalHours < 24))
            {
                if (Math.Round(dateDeference.TotalHours, 0, MidpointRounding.ToEven) == 1)
                {
                   return OneHourDateString(dateDeference.TotalHours);
                }
                else
                {
                    return SeveralHoursDateString(dateDeference.TotalHours);
                }
            }

            if ((dateDeference.TotalDays >= 1) && (dateDeference.TotalDays < 31))
            {
                if (Math.Round(dateDeference.TotalDays, 0, MidpointRounding.ToEven) == 1)
                {
                    return OneDayDateString(dateDeference.TotalDays);
                }
                else
                {
                    return SeveralDaysDateString(dateDeference.TotalDays);
                }
            }

            if ((dateDeference.TotalDays >= 30) && (dateDeference.TotalDays < 364))
            {
                if (Math.Round(dateDeference.TotalDays, 0, MidpointRounding.ToEven) == 30)
                {
                   return OneMonthDateString(dateDeference.TotalDays);
                }
                else
                {
                    return SeveralMonthsDateString(dateDeference.TotalDays);
                }
            }

            if (dateDeference.TotalDays > 364)
            {
                if (Math.Round(dateDeference.TotalDays, 0, MidpointRounding.ToEven) == 364)
                {
                    return OneYearDateString(dateDeference.TotalDays);
                }
                else
                {
                    return SeveralYearsDateString(dateDeference.TotalDays);
                }
            }

            return string.Empty;
        }


        private static string SeveralMinutesDateString(double totalMinutes)
        {
            return Math.Round(totalMinutes, 0, MidpointRounding.ToEven) + " minutes ago";
        }


        private static string OneHourDateString(double totalHours)
        {
            return  Math.Round(totalHours, 0, MidpointRounding.ToEven) + " hour ago";
        }


        private static string SeveralHoursDateString(double totalHours)
        {
            return Math.Round(totalHours, 0, MidpointRounding.ToEven) + " hours ago";
        }


        private static string OneDayDateString(double totalDays)
        {
            return Math.Round(totalDays, 0, MidpointRounding.ToEven) + " day ago";
        }


        private static string SeveralDaysDateString(double totalDays)
        {
            return Math.Round(totalDays, 0, MidpointRounding.ToEven) + " days ago";
        }


        private static string OneMonthDateString(double totalDays)
        {
            return Math.Round(totalDays / 30, 0, MidpointRounding.ToEven) + " month ago";
        }


        private static string SeveralMonthsDateString(double totalDays)
        {
            return Math.Round(totalDays / 30, 0, MidpointRounding.ToEven) + " months ago";
        }


        private static string OneYearDateString(double totalDays)
        {
            return Math.Round(totalDays / 364, 0, MidpointRounding.ToEven) + " year ago";
        }


        private static string SeveralYearsDateString(double totalDays)
        {
            return Math.Round(totalDays / 364, 0, MidpointRounding.ToEven) + " years ago";
        }
    }
}