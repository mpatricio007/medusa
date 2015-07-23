using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Security.Cryptography;
using System.IO;
using System.Text;

namespace Medusa.DAL
{
    public static class DateTimeExtensionMethods
    {
        public static DateTime AddBusinessDays(this DateTime date,int days)
        {
            try
            {
                DateTime tmpDate = date;
                while (days > 0)
                {
                    tmpDate = tmpDate.AddDays(1);
                    if (tmpDate.DayOfWeek < DayOfWeek.Saturday &&
                        tmpDate.DayOfWeek > DayOfWeek.Sunday &&
                        !tmpDate.IsHoliday())
                        days--;
                }
                return tmpDate;
            }
            catch (Exception)
            {
                return date;
            } 
        }

        public static int GetBusinessDays(DateTime start, DateTime end)
        {
            if (start.DayOfWeek == DayOfWeek.Saturday)
            {
                start = start.AddDays(2);
            }
            else if (start.DayOfWeek == DayOfWeek.Sunday)
            {
                start = start.AddDays(1);
            }

            if (end.DayOfWeek == DayOfWeek.Saturday)
            {
                end = end.AddDays(-1);
            }
            else if (end.DayOfWeek == DayOfWeek.Sunday)
            {
                end = end.AddDays(-2);
            }

            int diff = (int)end.Subtract(start).TotalDays;

            int result = diff / 7 * 5 + diff % 7;

            if (end.DayOfWeek < start.DayOfWeek)
            {
                return result - 2;
            }
            else
            {
                return result;
            }
        }

        public static bool IsHoliday(this DateTime date)
        {
            var ctx = new Contexto();
            return ctx.Recessos.SingleOrDefault(it => it.data == date) != null;
        }
    }
}