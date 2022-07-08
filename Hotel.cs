using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
namespace Hotel
{
    internal class Hotel
    {
        private List<Worker> lW;//Служащие
        private List<Client> lC;//Проживающие 
        const int cntEt = 5;//Количестов этажов
        private List<Room> lR;//Комнаты
        private string nameHotel;//Название 
        private const string Login = "Admin";
        private const string Passwor = "Admin";
        public Hotel()
        {
            lW = new List<Worker>();
            lC = new List<Client>();
            lR = new List<Room>();
            this.nameHotel = "";
            //5 этаж 
            //каждый этаж 10 комнат
            
        }
        public Hotel(string nm)
        {
            this.nameHotel = nm;
            lW = new List<Worker>();
            lC = new List<Client>();
            lR = new List<Room>();
        }
        public string Name 
        {
            get { return this.nameHotel; }
            set { this.nameHotel = value; }
        }
        public void LoadDB(string login,string pass)
        {
            if (login.CompareTo(Login) == 0 && pass.CompareTo(Passwor) == 0)
            {
                using (StreamReader sr=new StreamReader(@"Rooms.txt",System.Text.Encoding.UTF8))
                {
                    string line;
                    while((line=sr.ReadLine())!=null)
                    {
                        if (line == "")
                            continue;
                        line = line.Trim('|');
                        string[] split = line.Split('|');
                        for(int i = 0; i < split.Length; i++) 
                        { 
                          split[i] = split[i].Trim();
                        }
                        int id=Convert.ToInt32(split[0]);
                        bool isAc = true;
                        if(split[1].Trim().ToLower().CompareTo("занято")==0)
                            isAc = false;
                        string nums = split[4].Trim();
                        string snums = "";
                        foreach (char ch in nums)
                            if (Char.IsDigit(ch))
                                snums += ch;
                        long num = long.Parse(snums);
                        double pr=double.Parse(split[3].Trim());
                        Room room = new Room(id,pr,split[2],num,isAc);
                        lR.Add(room);
                    }
                    sr.Close();
                }
                using (StreamReader sr = new StreamReader(@"Clients.txt", System.Text.Encoding.UTF8))
                {
                    string line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        if(line=="")
                            continue ;
                        line = line.Trim('|');
                        string[] split = line.Split('|');
                        for (int i = 0; i < split.Length; i++)
                        {
                            split[i] = split[i].Trim();
                        }
                        string snm=split[0];
                        string nm=split[1];
                        string mnm=split[2];
                        int id=int.Parse(split[3]);
                        string ct=split[4];
                        int nmrm=int.Parse(split[5]);
                        string dC=split[7];
                        Client client=new Client(snm,nm,mnm,id,ct,nmrm);
                        client.DateClose = dC;
                        client.DateOr=Date.parseDate(split[6]);
                        lC.Add(client);
                    }
                    sr.Close();
                }
                using (StreamReader sr = new StreamReader(@"Workers.txt", System.Text.Encoding.UTF8))
                {
                    string line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        if (line == "")
                            continue;
                        line = line.Trim('|');
                        string[] split = line.Split('|');
                        for (int i = 0; i < split.Length; i++)
                        {
                            split[i] = split[i].Trim();
                        }
                        string snm = split[0];
                        string nm = split[1];
                        string mnm = split[2];
                        int et=int.Parse(split[3]);
                        int len = split.Length - 4;
                        Date.DayWeek[]days=new Date.DayWeek[len];
                        for (int i = 0;i < len;i++)
                            days[i] = Date.parseDayWeek(split[4+i]);
                        Worker worker = new Worker(snm, nm, mnm, et, days);
                        lW.Add(worker);

                    }
                    sr.Close();
                }

            }
            else
                throw new Exception("Не правильный парол или логин Система закрыт!");
        }
        //Клиенты проживающых в заданном номере 
        public List<Client>getClientsCurNomer(int nm)
        {
            List<Client> clients = new List<Client>();
            foreach(Client cl in lC)
                if(cl.NumRoom==nm&& cl.DateClose=="+")//ТО ест те клиенты который живут 
                    clients.Add(cl);
            return clients;
        }
        //Клиенты прибывщих из заданного Города
        public List<Client>getClientFromCity(string city)
        {
            List<Client>cl=new List<Client>();
            foreach (Client c in lC)
                if (c.City== city)
                    cl.Add(c);
            return cl;
        }
        //Кто из служащых убрал
        public Worker getCleanByNum(int num,string day)
        {
            Date.DayWeek dayWeek = Date.parseDayWeek(day);
            //Сначало найдем номер этажа
            int finEt = num/10+1;
            //Теперь найдем
            foreach(Worker w in lW)
            {
                if((w.Etaj==finEt))
                {
                    //Теперь проверим дни
                    foreach(Date.DayWeek i in w.Days)
                    {
                        if(i==Date.parseDayWeek(day))
                        {
                            return w;
                        }
                    }
                    Console.WriteLine(w.ToString());
                }
            }
        return null;
        }
        //Отчет о свободных номера
        public void FreePlace()
        {
            int freePl = 0;
            int freRom = 0;
            foreach(Room rm in lR)
            {
                //Найдем количество 
                List<Client>clients =getClientsCurNomer(rm.Id);
                int cur = (int)rm.Type+1;
                freePl = freePl+clients.Count - cur;
                Console.WriteLine($"Номер : {rm.Id}  {clients.Count - cur}-Свободных мест");
                freRom++;
            }
            Console.WriteLine(freePl+" -Свободных мест "+freRom+" -Свободных номера");
        }
        //Вывод всех клиентов
        public void PrintClient()
        {
            Console.WriteLine($"|{"Фамилия",20}|{"Имя ",20}|{"Отчество",20}|{"Ном.Пас",8}|{" Город ",15}|{"Брон",6}|{"ДатаПр",10}|{"ДатаВых",10}|");
            Console.WriteLine("|++++++++++++++++++++|++++++++++++++++++++|++++++++++++++++++++|++++++++|+++++++++++++++|++++++|++++++++++|++++++++++|");
            foreach (Client cl in lC)
                Console.WriteLine(cl);
            Console.WriteLine("|++++++++++++++++++++|++++++++++++++++++++|++++++++++++++++++++|++++++++|+++++++++++++++|++++++|++++++++++|++++++++++|");
        }
        //Вывод клиентов перегрузка
        public void PrintClientBy(List<Client>cl)
        {
            Console.WriteLine($"|{"Фамилия",20}|{"Имя ",20}|{"Отчество",20}|{"Ном.Пас",8}|{" Город ",15}|{"Брон",6}|{"ДатаПр",10}|{"ДатаВых",10}|");
            Console.WriteLine("|++++++++++++++++++++|++++++++++++++++++++|++++++++++++++++++++|++++++++|+++++++++++++++|++++++|++++++++++|++++++++++|");
            foreach (Client l in cl)
                Console.WriteLine(l);
            Console.WriteLine("|++++++++++++++++++++|++++++++++++++++++++|++++++++++++++++++++|++++++++|+++++++++++++++|++++++|++++++++++|++++++++++|");
        }
        //Вывод всех служащых
        public void PrintWorker()
        {
            Console.WriteLine($"|{"  Фамилия  ",20}|{"  Имя  ",20}|{" Отчество ",20}|{"Э.",2}|{"   График    ",20}|");
            Console.WriteLine("|++++++++++++++++++++|++++++++++++++++++++|++++++++++++++++++++|++|++++++++++++++++++++|");
            foreach(Worker w in lW)
                Console.WriteLine(w);
            Console.WriteLine("|++++++++++++++++++++|++++++++++++++++++++|++++++++++++++++++++|++|++++++++++++++++++++|");
        }
        //Перегружим вывод служашего
        public void PrintWorkerBy(Worker w)
        {
                Console.WriteLine($"|{"  Фамилия  ",20}|{"  Имя  ",20}|{" Отчество ",20}|{"Э.",2}|{"   График    ",20}|");
                Console.WriteLine("|++++++++++++++++++++|++++++++++++++++++++|++++++++++++++++++++|++|++++++++++++++++++++|");
                    Console.WriteLine(w);
                Console.WriteLine("|++++++++++++++++++++|++++++++++++++++++++|++++++++++++++++++++|++|++++++++++++++++++++|");
        }
        public void PrintRooms()
        {
            Console.WriteLine($"|{"Номер",6}|{"Состояние",-10}|{"   Тип   ",-12}|{"Стоимос.",8}|{"   Номер   ",19}|");
            Console.WriteLine("|++++++|++++++++++|++++++++++++|++++++++|+++++++++++++++++++|");
            foreach(Room r in lR)
                Console.WriteLine(r);
            Console.WriteLine("|++++++|++++++++++|++++++++++++|++++++++|+++++++++++++++++++|");
        }
        //Действия администратора
        //Поселит клиента
     public void AddKlient(Client cl)
        {
            lC.Add(cl);
            Console.WriteLine("Успешно добавлено!!");
        }
        //Высилить клиента
        public void RemoveClient(int ind)
        {
            lC.RemoveAt(ind);
            Console.WriteLine("Успешно удалено!!");
        }
        //Принять на работу
       public void AddWorker(Worker w)
        {
            lW.Add(w);
            Console.WriteLine("Успешно принят на работу!!");
        }
        //Увалнение 
        public void RemoveWorker(int ind)
        {
            lW.RemoveAt(ind);
            Console.WriteLine("Успешно уволнено!!");
        }

        //Проверка на логин и пароля 
        public bool isCheckAdmin(string log,string pas)
        {
            if(log==Login&& pas==Passwor)
                return true;
            return false;
        }
        //Печать номеров который свободный
        public void PrintFreeRoom()
        {
            Console.WriteLine($"|{"Номер",6}|{"Состояние",-10}|{"   Тип   ",-12}|{"Стоимос.",8}|{"   Номер   ",19}|");
            Console.WriteLine("|++++++|++++++++++|++++++++++++|++++++++|+++++++++++++++++++|");
            int freenum = 0;
            foreach (Room r in lR)
                if(r.IsActive)
                {
                    Console.WriteLine(r);
                    freenum = r.Id;
                }
            Console.WriteLine("|++++++|++++++++++|++++++++++++|++++++++|+++++++++++++++++++|");
        }

        //Вывод текущых клиентов
        public void PrintCurClient()
        {
            Console.WriteLine($"|{"Фамилия",20}|{"Имя ",20}|{"Отчество",20}|{"Ном.Пас",8}|{" Город ",15}|{"Брон",6}|{"ДатаПр",10}|{"ДатаВых",10}|");
            Console.WriteLine("|++++++++++++++++++++|++++++++++++++++++++|++++++++++++++++++++|++++++++|+++++++++++++++|++++++|++++++++++|++++++++++|");
            foreach (Client cl in lC)
            {
                if(cl.DateClose.CompareTo("+")==0)
                    Console.WriteLine(cl);
            }
            Console.WriteLine("|++++++++++++++++++++|++++++++++++++++++++|++++++++++++++++++++|++++++++|+++++++++++++++|++++++|++++++++++|++++++++++|");
        }
        //Количество место в заданом номере 
        public int cntPlaceInRoom(int num)
        {
            foreach(Room r in lR)
            {
                if ((int)r.Id == num)
                    return (int)r.Type;
            }
            return -1;
        }
        //Дизактив комнаты
        public void DisActiveRoom(int num)
        {
            foreach (Room r in lR)
            {
                if ((int)r.Id == num)
                    r.IsActive = false;
            }
        }
        //Дизактивация клиента
        public void DisActivClient(int kod)
        {
            foreach(Client r in lC)
            {
                if(r.NumPas == kod)
                {
                    Date d=new Date();
                    r.DateClose = d.ToString();
                }
            }

        }
        //Изменинеи Графики
        public Date.DayWeek[] newGrafik()
        {
            Console.WriteLine("Сколько дней в недели: ");
            int size=Convert.ToInt32(Console.ReadLine());  
            Date.DayWeek[] days=new Date.DayWeek[size];
            
            for(int i=0; i<days.Length; i++)
            {
                Console.WriteLine("Введите краткый название (Например Вс или Вс.): ");
                string d=Console.ReadLine();
                d=d.Trim('.');
                days[i]=Date.parseDayWeek(d);
            }
            return days;
        }
        //Изменит График служашего по ФИО
        public void ChangeGrafik(string s,string n,string m)
        {
            foreach(Worker w in lW)
            {
                if(w.Name == n&&w.Surname==s&&w.Middle_Name==m)
                {
                    Date.DayWeek[] days = newGrafik();
                    w.Days = days;
                }
            }
        }
        //Сохранение данных
        public void SaveData()
        {
            using(StreamWriter win=new StreamWriter(@"Clients.txt",false,System.Text.Encoding.UTF8))
            {
                foreach(Client cl in lC)
                {
                    win.WriteLine(cl);
                }
                win.Close();
            }
            using (StreamWriter win = new StreamWriter(@"Workers.txt", false, System.Text.Encoding.UTF8))
            {
                foreach (Worker cl in lW)
                {
                    win.WriteLine(cl);
                }
                win.Close();
            }
            using (StreamWriter win = new StreamWriter(@"Rooms.txt", false, System.Text.Encoding.UTF8))
            {
                foreach (Room r in lR)
                {
                    win.WriteLine(r);
                }
                win.Close();
            }

        }

        //отчет
        double getPriceRoom(int num)
        {
            foreach (Room room in lR)
            {
                if (room.Id == num)
                    return room.price;
            }
            return 0;
        }
        //Получение доходности за квартал
        public double getSumPeriod(Date from, Date to)
        {
            double sum = 0;
            foreach (Client cl in lC)
            {
                if(cl.DateOr>=from)
                {
                    Date d;
                    if(cl.DateClose.CompareTo("+")==0)
                        d=new Date();
                    else d=Date.parseDate(cl.DateClose);
                    if (to <= d)
                        sum += Date.PerioadDay(from, to) * getPriceRoom(cl.NumRoom);
                    else
                        sum += Date.PerioadDay(from, d) * getPriceRoom(cl.NumRoom);
                }

            }
            return sum;
        }
        public void Itogo(Date from,Date to)
        {
            Console.WriteLine($"Доходность за квартал={getSumPeriod(from, to)}");
            int cntCl = 0;
            foreach (Client cl in lC)
            {
                if (cl.DateOr >= from)
                {
                    cntCl++;
                }

            }
            Console.WriteLine("Число клиентов за указанный период: " + cntCl);

        }
    }
}