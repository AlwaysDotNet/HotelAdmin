using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
namespace Hotel
{
    internal class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Hotel my=new Hotel("TransilVan");//название Гостиницы
                my.LoadDB("Admin", "Admin");//Загрузка данных
                
                bool quit = false;
                while(!quit)
                {
                    Console.WriteLine($"*************************Добро пожаловвать в {my.Name}**************************"); 
                    Console.WriteLine("1 -Таблица Клиентов");
                    Console.WriteLine("2 -Таблицы Номеров");
                    Console.WriteLine("3 -Таблица Служащых");
                    Console.WriteLine("4 -Внести изменение");
                    Console.WriteLine("5 -Дополнительные возможности");
                    Console.WriteLine("6 -Выход");
                    int cmd=Convert.ToInt32(Console.ReadLine());
                    switch(cmd)
                    {
                        case 1:
                            {
                                my.PrintClient();
                                break;
                            }
                        case 2:
                            {
                                my.PrintRooms();
                                break;
                            }
                        case 3:
                            {
                                my.PrintWorker();
                                break;
                            }
                        case 4:
                            {

                                string adminLog;//Логи Админа
                                string adminPar;//парол админа
                                Console.WriteLine("Введите Логин: ");
                                adminLog = Console.ReadLine();
                                Console.WriteLine("Введите парол: ");
                                adminPar = Console.ReadLine();
                                Hotel ht = new Hotel("MyHotel");
                                if (!ht.isCheckAdmin(adminLog, adminPar))
                                    throw new Exception("Пароль или Логин не правильно вы заблокированы!");
                                bool dsq = false;
                                while(!dsq)
                                {
                                    Console.WriteLine("1 -Поселить клиента");
                                    Console.WriteLine("2 -Выселить клиента");
                                    Console.WriteLine("3 -Принят на работу");
                                    Console.WriteLine("4 -Уволить служащего");
                                    Console.WriteLine("5 -Изменить график работы служашего");
                                    Console.WriteLine("6 -Назад");
                                    int cm=Convert.ToInt32(Console.ReadLine());
                                    switch(cm)
                                    {
                                        case 1:
                                            {
                                                my.PrintFreeRoom();
                                                Console.WriteLine("Выбирайте номер : ");
                                                int num=Convert.ToInt32(Console.ReadLine());
                                                Console.WriteLine("Имя: ");
                                                string name=Console.ReadLine();
                                                Console.WriteLine("Фамилия");
                                                string surname=Console.ReadLine();
                                                Console.WriteLine("Отчество ");
                                                string ot=Console.ReadLine();
                                                Console.WriteLine("Город: ");
                                                string ct=Console.ReadLine();
                                                Console.WriteLine("Код паспорта: ");
                                                int id=Convert.ToInt32(Console.ReadLine());
                                                Client cl=new Client(surname,name,ot,id,ct,num);
                                                //Добавим в базу
                                                my.AddKlient(cl);
                                                int cnt = my.getClientsCurNomer(num).Count;
                                                if(cnt==(int)my.cntPlaceInRoom(num)+1)
                                                    my.DisActiveRoom(num);
                                                break;
                                            }
                                        case 2:
                                            {
                                                my.PrintCurClient();
                                                Console.WriteLine("Ввдите код паспорта для выселение : ");
                                                int kod=Convert.ToInt32(Console.ReadLine());
                                                my.DisActivClient(kod);
                                                break;
                                            }
                                        case 3:
                                            {
                                                Console.WriteLine("Имя: ");
                                                string name = Console.ReadLine();
                                                Console.WriteLine("Фамилия");
                                                string surname = Console.ReadLine();
                                                Console.WriteLine("Отчество ");
                                                string ot = Console.ReadLine();
                                                Console.WriteLine("Номер этажа для уборки: ");
                                                int et=Convert.ToInt32(Console.ReadLine());
                                                Date.DayWeek[] days = my.newGrafik();
                                                my.AddWorker(new Worker(surname, name, ot, et, days));
                                                Console.WriteLine("Успешно добавлено");
                                                break;
                                            }
                                        case 4:
                                            {
                                                my.PrintWorker();
                                                Console.WriteLine("Выбирайте индекс для увалнение: ");
                                                int ind=Convert.ToInt32(Console.ReadLine());
                                                my.RemoveWorker(ind);
                                                break;
                                            }
                                        case 5:
                                            {
                                                my.PrintWorker();
                                                Console.WriteLine("Введите Имя Фамилия и Отчество для изменения Графика:");
                                                Console.WriteLine("Имя:");
                                                string name = Console.ReadLine();
                                                Console.WriteLine("Фамилия:");
                                                string surname = Console.ReadLine();
                                                Console.WriteLine("Отчество:");
                                                string ot = Console.ReadLine();
                                                my.ChangeGrafik(surname,name, ot);
                                                break;
                                            }
                                        case 6:
                                            {
                                                dsq = true;//Выход
                                                break;
                                            }
                                    }
                                  

                                }
                                
                                //Нам нужно сохранить данные
                                my.SaveData();
                                break;//4
                            }
                        case 5:
                            {
                                bool isq = false;
                                while(!isq)
                                {
                                    Console.WriteLine("************Дополнительные возможности*************");
                                    Console.WriteLine("1 -Отчест за квартал");
                                    Console.WriteLine("2 -Клиенты который проживают в заданный номер");
                                    Console.WriteLine("3 -Клиенты с..");
                                    Console.WriteLine("4 -Сведение о то кто убрал заданный номер");
                                    Console.WriteLine("5 -Свободные номера");
                                    Console.WriteLine("6 -Назад");
                                    int icm=Convert.ToInt32(Console.ReadLine());
                                    switch(icm)
                                    {
                                        case 1:
                                            {
                                                Console.WriteLine("Введите дата начало(dd-mm-yyyy):");
                                                Date from=Date.parseDate(Console.ReadLine().Trim());
                                                Date to=Date.parseDate(Console.ReadLine().Trim());
                                                my.Itogo(from,to);
                                                break;//Отчет за квартал
                                            }
                                        case 2:
                                            {
                                                //выводим те клиенты который проживають в данный момент
                                                Console.WriteLine("Введите номер: ");
                                                int num=Convert.ToInt32(Console.ReadLine());
                                                List<Client> cl = my.getClientsCurNomer(num);
                                                my.PrintClientBy(cl);
                                                break;
                                            }
                                        case 3:
                                            {
                                                //Клиенты который прибыли с города Город
                                                Console.WriteLine("Введите Город: ");
                                                string cit=Console.ReadLine();
                                                List<Client>cls=my.getClientFromCity(cit);
                                                my.PrintClientBy(cls);
                                                break;
                                            }
                                        case 4:
                                            {
                                                Console.WriteLine("Введите номер комнаты: ");
                                                int num=Convert.ToInt32((string)Console.ReadLine());
                                                Console.WriteLine("Введите ден (Используйте краткый запись Вс или Вс.)");
                                                string tm=Console.ReadLine().Trim('.');
                                                my.PrintWorkerBy(my.getCleanByNum(num,tm));
                                                break;
                                            }
                                        case 5:
                                            {
                                                my.FreePlace();
                                                break;
                                            }
                                        case 6:
                                            {
                                                isq=true;
                                                break;
                                            }
                                    }//Конец switch
                                }
                                break;//5
                            }
                        case 6:
                            {
                                Console.WriteLine("Вы точно хотите вийты ?(yes/no): ");
                                string nm=Console.ReadLine();
                                nm.Trim();
                                if(nm.ToLower().CompareTo("yes")==0)
                                {
                                    quit = true;
                                    my.SaveData();//Сохраним данные
                                }
                                break;
                            }

                    }
                }
                Console.ReadLine();

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message); 
                Console.ReadLine();
            }
        }
    }
}
