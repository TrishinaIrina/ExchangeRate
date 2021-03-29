using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using ExchangeRate.Value;
using System.Threading;
using System.IO;
using LiteDB;

namespace ExchangeRate.OnlainApp
{
    class Program
    {
/* Приложение должно состоять из 2-х частей:
1.	Библиотека классов
2.	Консольное приложение
Цель.
Приложения состоит в том, чтобы получать актуальные курсы с интервалом, который укажет пользователь.
Логика приложения.
При запуске приложения должно происходить обращение по ссылке для получения курса валют в формате JSON. 
(вариант 1) После того как курс валют будет получен, его необходимо загрузить в БД.
На экран же вывести уведомление, красным цветом в том случае если новый полученный курс валют отличается от ранее загруженного курса валют.
(вариант 2) После того как курс валют будет получен, его необходимо отобразить на экране.
*/
        private static string pathForDB = @"C:\Users\irish\My documents\C#\ExchangeRate.OnlainApp\newBD.db";
        static void Main(string[] args)
        {
            while (true) //программа работает бесконечно, обновляется раз в час (по идее надо продумать решение переполнения БД)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;                
                Console.WriteLine("Данные по курсу валюты обновляются каждый час");
                Console.WriteLine("Курс выводится красным цветом если за час произошло его изменение");
                Console.ResetColor();
                string message = "";
                RateValue rateDOL = new RateValue("доллар");

                DB.Create<RateValue>(pathForDB, rateDOL, out message);

                var list = DB.GetCollection<RateValue>(pathForDB);

                DB.ShowLast(list);
                Thread.Sleep(3600000);
                Console.Clear();
            }
        }
    }
}

    
  
