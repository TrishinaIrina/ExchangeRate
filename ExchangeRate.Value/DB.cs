using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using ExchangeRate.Value;
using System.IO;
using LiteDB;

namespace ExchangeRate.Value
{
    public static class DB
    {
        public static List<T> GetCollection<T>(string pathForDB)
        {
            try
            {
                using (var db = new LiteDatabase(pathForDB))
                {
                    return db.GetCollection<T>(typeof(T).Name)
                        .FindAll()
                        .ToList();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static bool Create<T>(string pathForDB, T data, out string message)
        {
            try
            {
                using (var db = new LiteDatabase(pathForDB))
                {
                    var collecection = db.GetCollection<T>(typeof(T).Name);
                    collecection.Insert(data);
                }
                message = "success";
                return true;
            }
            catch (Exception ex)
            {
                message = ex.Message;
                return false;
            }
        }

        public static void ShowAll(List<RateValue> list)
        {
            Console.WriteLine(list[0]);
            for (int i = 1; i < list.Count; i++)
            {
                if (list[i].curValue != list[i - 1].curValue)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(list[i]);
                    Console.ResetColor();
                }
                else Console.WriteLine(list[i]);
            }
        }

        public static void ShowLast(List<RateValue> list)
        {
            Console.WriteLine("-----------------------------------------------------------------------------");
            Console.WriteLine("Курсы валют ЦБ на " + list[list.Count - 1].createDate);
            Console.WriteLine("-----------------------------------------------------------------------------");
            if (list.Count == 1) Console.WriteLine(list[0]);
            else if (list[list.Count - 1].curValue != list[list.Count - 2].curValue)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(list[list.Count - 1]);
                Console.ResetColor();
            }
            else Console.WriteLine(list[list.Count - 1]);
            Console.WriteLine("-----------------------------------------------------------------------------");
        }

    }
}
