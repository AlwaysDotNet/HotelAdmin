using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel
{
    //Класс описиваюшый номер гостиницы
    internal class Room
    {
        public int Id { get; set; }//Номер 
        public bool IsActive { get; set; }//Свободно ли нет
        public double price { get; set; }//Цена за сутки
        public long Phone { get; set; } //Телефон   
        public enum RoomType
        {
            OnePlace,//Одноместный 
            TwoPlace,//Двухместный 
            ThreePlace,//Трехместный
        }
        public static string RoomTypeToStr(RoomType r)
        {
            switch (r)
            {
                case RoomType.OnePlace:
                    return "одноместный";
                case RoomType.TwoPlace:
                    return "двухместный";
                case RoomType.ThreePlace:
                    return "трехместный";
                    default: return string.Empty;
            }
        }
        public RoomType Type { get; set;}
        public Room()
        {
            this.price = 100;
            this.Type = RoomType.OnePlace;
            this.IsActive = true;
            this.Id = 0;
            this.Phone = 72232563263;
        }
        public Room(int id,double pr,string tp,long ph= 72232563263,bool ac=true)
        {
            this.Id=id;
            this.Type=parseRoomType(tp);
            this.IsActive = ac;
            this.price=pr;
            this.Phone = ph;
        }
        public static RoomType parseRoomType(string tp)
        {
            switch (tp.ToLower())
            {
                case "одноместный":
                    return RoomType.OnePlace;
                case "двухместный":
                    return RoomType.TwoPlace;
                case "трехместный":
                    return RoomType.ThreePlace;
                    default : return RoomType.OnePlace;
            }
        }
        public override string ToString()
        {
            string sm = (IsActive)?"свободен":"занято";
            string type=RoomTypeToStr(Type);
            string phone=string.Format("{0:+# (###) ###-##-##}",Phone);
            string ans = $"|{Id,6}|{sm,-10}|{type,-12}|{price,8}|{phone}|";
            return ans;
        }
    }
}
