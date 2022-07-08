using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel
{
    //Класс Person 
    internal class Person
    {
        //У каждого человека есть ФИО
        private string name;//Имя
        private string surname;//Фамилия
        private string middle_name;//Отчество 
        public Person()
        {
            //Конструктор по умолчание
            this.name = "";
            this.middle_name = "";
            this.surname = "";
        }
        //Реализуем Геттеры и Сеттеры свойства
        public string Name
        {
            get { return this.name; }
            set
            {
                    value = value.Trim();
                    foreach (char c in value)
                    {
                        if (!Char.IsLetter(c))
                    {
                        throw new Exception("Ошибка значение поля не можеть имет символов кроме букв!");
                    }
                    }
                    this.name = value;
            }
       
        }
        
        public string Surname
        {
            get { return this.surname; }
            set
            {
                    value = value.Trim();

                    foreach (char c in value)
                    {
                        if (!Char.IsLetter(c))
                            throw new Exception("Ошибка значение поля не можеть имет символов кроме букв!");
                    }
                    this.surname = value;
                
                
            }
        }
       public string Middle_Name
        {
            get { return this.middle_name; }
            set
            {
                
                    value = value.Trim();
                    foreach (char c in value)
                    {
                        if (!Char.IsLetter(c))
                            throw new Exception("Ошибка значение поля не можеть имет символов кроме букв!");
                    }
                    this.middle_name = value;
            }
        }
        public Person(string snm,string nm,string mnm )
        {
            this.Name=nm;
            this.Middle_Name=mnm;
            this.Surname=snm;
        }
        public override string ToString()
        {
            return $"Фамилия={surname,-20}  Имя={name,-20}  Отчество={middle_name,-20}";
        }
    }
}
