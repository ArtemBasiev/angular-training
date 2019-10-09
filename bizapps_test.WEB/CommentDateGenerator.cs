using System;


namespace bizapps_test.WEB
{
    public static class CommentDateGenerator
    {
        public static string GetDateString(DateTime inputDate)
        {
            string dateString="";
            TimeSpan deferenceDate = DateTime.Now - inputDate;
            if ((deferenceDate.TotalMinutes <= 1)&&(deferenceDate.TotalSeconds > 0))
            {
                dateString = "Now";
            }
       
            if ((deferenceDate.TotalMinutes > 1) && (deferenceDate.TotalMinutes < 60))
            {
                dateString = Math.Round(deferenceDate.TotalMinutes, 0, MidpointRounding.ToEven)+" minutes ago";
            }

            if ((deferenceDate.TotalHours >= 1) && (deferenceDate.TotalHours < 24))
            {
                if (Math.Round(deferenceDate.TotalHours, 0, MidpointRounding.ToEven) == 1)
                {
                    dateString = Math.Round(deferenceDate.TotalHours, 0, MidpointRounding.ToEven) + " hour ago";
                }
                else
                {
                    dateString = Math.Round(deferenceDate.TotalHours, 0, MidpointRounding.ToEven) + " hours ago";
                }
                
            }

            if ((deferenceDate.TotalDays >= 1) && (deferenceDate.TotalDays < 31))
            {
                if (Math.Round(deferenceDate.TotalDays, 0, MidpointRounding.ToEven) == 1)
                {
                    dateString = Math.Round(deferenceDate.TotalDays, 0, MidpointRounding.ToEven) + " day ago";
                }
                else
                {
                    dateString = Math.Round(deferenceDate.TotalDays, 0, MidpointRounding.ToEven) + " days ago";
                }
               
            }

            if ((deferenceDate.TotalDays >= 30) && (deferenceDate.TotalDays < 364))
            {
                if (Math.Round(deferenceDate.TotalDays, 0, MidpointRounding.ToEven) == 30)
                {
                    dateString = Math.Round(deferenceDate.TotalDays / 30, 0, MidpointRounding.ToEven) + " month ago";
                }
                else
                {
                    dateString = Math.Round(deferenceDate.TotalDays / 30, 0, MidpointRounding.ToEven) + " months ago";
                }
                   
            }

            if (deferenceDate.TotalDays > 364)
            {
                if (Math.Round(deferenceDate.TotalDays, 0, MidpointRounding.ToEven) == 364)
                {
                    dateString = Math.Round(deferenceDate.TotalDays / 364, 0, MidpointRounding.ToEven) + " year ago";
                }
                else
                {
                    dateString = Math.Round(deferenceDate.TotalDays / 364, 0, MidpointRounding.ToEven) + " years ago";
                }
                  
            }


            return dateString;
        }
    }
}