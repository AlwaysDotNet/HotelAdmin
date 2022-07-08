using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel
{
    //Класс Client описивает Человека который проживает или прожил
    internal class Client:Person
    {
        private int _id;//Номер паспорта
        private string city;//Город с который прибыл
        int numRoom;//Номер который бронировал 
        Date dateOr;//Дата прибыли
        private string dateclose;
        public  string DateClose
        {
            get { return dateclose; }
            set { dateclose = value;}
        }//Дата который ушел если актвино то +
        public int NumPas
        {
            get { return _id; }
            set 
            {

                    string svalue =Convert.ToString(value);
                    foreach(char c in svalue)
                        if (!Char.IsDigit(c))
                            throw new Exception("Ошибка! значение не соответствует!");
                _id = value;
            }
        }
        public string City
        {
            get { return city; }
            set { city = value; }
        }
        public int NumRoom
        {
            get { return numRoom; } 
            set { numRoom = value; }    
        }
        public Date DateOr
        {
            get { return dateOr; }
            set { dateOr = value; }
        }
        
        public Client():base()
        {
            //По умолчание
            this.City = "-";
            this.NumRoom = 0;
            this._id = 0;
            this.dateOr = new Date();
            this.dateclose = "+";
             
        }
        public Client(string sname,string nm,string mnm,int id,string ct,int nmrm):base(sname,nm,mnm)
        {
            this._id = id;
            this.city = ct;
            this.numRoom = nmrm;
            this.dateOr=new Date();
            this.DateClose = "+";
        }
        public override string ToString()
        {
            return $"|{base.Surname,20}|{base.Name,20}|{base.Middle_Name,20}|{NumPas,8}|{city,15}|{numRoom,6}|{DateOr.ToString()}|{DateClose,10}|";
        }
        //обшая сумма который дал проживщый 
        public double getProfit(double pr)
        {
            //Сначало считаем количество дней
            if(this.DateClose=="+")
            {
                return (Date.DayToCurDate(DateOr)+1)*pr;
            }
            else
            {
                Date date = Date.parseDate(DateClose);
                return  (Date.PerioadDay(dateOr,date)+1) * pr;
            }
        }

    }
}
