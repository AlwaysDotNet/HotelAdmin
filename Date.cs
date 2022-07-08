using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel
{
    internal class Date
    {
        public int day { get; set; }
        public int month { get; set; }
        public int year { get; set; }
        public Date()
        {

            day = DateTime.Now.Day;
            month = DateTime.Now.Month;
            year = DateTime.Now.Year;   
        }
        public Date(int d,int m,int y)
        {
            this.day = d;  
            this.month = m;
            this.year = y; 
        }
        //Функция будет возврашат ден недели Вс-0
        public int getDayOfWeek()
        {
            //используем стандарт
            Date date = new Date(year,month,day);
            int rez=date.getDayOfWeek();
            return  rez;
        }
        public  enum DayWeek
        {
            Sunday,
            Mon,
            Tue,
            Wed,
            Thue,
            Friday,
            Saturday,
        }
        public static string DayOfWeekToStr(DayWeek d)
        {
            switch(d)
            {
                case DayWeek.Sunday:
                    return "Вс.";
                case DayWeek.Mon:
                    return "Пн.";
                case DayWeek.Tue:
                    return "Вт.";
                case DayWeek.Wed:
                    return "Ср.";
                case DayWeek.Thue:
                    return "Чт.";
                case DayWeek.Friday:
                    return "Пт.";
                case DayWeek.Saturday:
                    return "Сб.";
                    default: return "-";
            }
        }
        public static string DayOfWeekToStr(int d)
        {
            DayWeek dayWeek = (DayWeek)d;
            switch (dayWeek)
            {
                case DayWeek.Sunday:
                    return "Вс.";
                case DayWeek.Mon:
                    return "Пн.";
                case DayWeek.Tue:
                    return "Вт.";
                case DayWeek.Wed:
                    return "Ср.";
                case DayWeek.Thue:
                    return "Чт.";
                case DayWeek.Friday:
                    return "Пт.";
                case DayWeek.Saturday:
                    return "Сб.";
                default: return "-";
            }
        }

        public static Date.DayWeek parseDayWeek(string d)
        {
            d=d.ToLower();
            d=d.Trim('.');
            if (d.CompareTo("вс") == 0)
                return DayWeek.Sunday;
            else if (d.CompareTo("пн") == 0)
                return DayWeek.Mon;
            else if (d.CompareTo("вт") == 0)
                return DayWeek.Tue;
            else if (d.CompareTo("ср") == 0)
                return DayWeek.Wed;
            else if (d.CompareTo("чт") == 0)
                return DayWeek.Thue;
            else if(d.CompareTo("пт")==0)
                return DayWeek.Friday;
            else return DayWeek.Saturday;
        }
        //Сегодня какой ден недели
        public static int CurDayOfWeek()
        {
            return (int)DateTime.Now.DayOfWeek;
        }

        //Перегрузка некоторых операторов
        public static bool operator <(Date a, Date b)
        {

            if (a.year < b.year)
                return true;
            else if (a.month < b.month)
            {
                return true;
            }
            else if (a.day < b.day)
                return true;
            return false;
        }
        public static bool operator>(Date a,Date b)
        {

            if (a.year > b.year)
                return true;
            else if (a.month > b.month)
            {
                return true;
            }
            else if (a.day > b.day)
                return true;
            return false;
        }
        public static bool operator==(Date a,Date b)
        {
            return (a.year==b.year&& a.month==b.month&& a.day==b.day);
        }
        public static bool operator!=(Date a,Date b)
        {
            return !(a == b);
        }
        public static bool operator<=(Date a,Date b)
        {
            return (a == b||a<b);
        }
        public static bool operator>=(Date a,Date b)
        {
            return (a==b||a>b);
        }

        //Реализуем функция который будет считат количество дней от заданного то текушый даты
        public static int DayToCurDate(Date a)
        {
            DateTime fr=new DateTime(a.year,a.month,a.day);
            int ds = (DateTime.Now - fr).Days;
            return ds;
        }
        //Количество дней между двумя датами
        public  static int PerioadDay(Date from,Date to)
        {
            DateTime fr = new DateTime(from.year,from.month,from.day);
            DateTime too = new DateTime(to.year, to.month, to.day);
            return (too - fr).Days;
        }
        public static Date parseDate(string d)
        {
            string []ob=d.Split('-');
            int day=Convert.ToInt32(ob[0]);
            int mon=Convert.ToInt32(ob[1]);
            int yr=Convert.ToInt32(ob[2]);
            return new Date(day, mon, yr);
        }


        public override string ToString()
        {
            return $"{day/10}{day%10}-{month/10}{month%10}-{year}";
        }
        public override bool Equals(object obj)
        {
           if(obj is Date)
                return (Date)this==(Date)obj;
           return false;
        }
        public override int GetHashCode()
        {
            return day.GetHashCode();
        }
    }
}
