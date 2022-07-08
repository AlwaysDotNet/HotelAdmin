using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel
{
    //Служащ
    internal class Worker:Person
    {
        private int etaj;//Номер этажа 
        private Date.DayWeek[] days;//Какие дны недели
        public int Etaj
        {
            get { return etaj; }    
            set { etaj = value; }   
        }
        public Date.DayWeek[] Days
        {
            get { return days; }
            set { days = value; }
        }
        public Worker():base()
        {
            this.etaj = 0;
        }
        public Worker(string sn,string nm,string mnm,int et,Date.DayWeek[]dy):base(sn,nm,mnm)
        {
            this.etaj = et;
            this.days = dy;
        }

        public override string ToString()
        {
            string line=$"|{base.Surname,20}|{base.Name,20}|{base.Middle_Name,20}|{Etaj,2}|"; 
            foreach(Date.DayWeek i in this.Days)
            {
                line += $"{Date.DayOfWeekToStr(i),2}|";
            }
            return line;
        }
    }
}
