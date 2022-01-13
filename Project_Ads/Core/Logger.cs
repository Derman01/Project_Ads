using System;
using System.IO;
using Project_Ads.MVVM.Model;

namespace Project_Ads.Core
{
    public static  class Logger
    {
        private static string PATH = AppDomain.CurrentDomain.BaseDirectory + @"\log.txt";

        public static void InitState()
        {
            var fileExists = File.Exists(PATH);
            if (!fileExists)
            {
                File.CreateText(PATH).Dispose();
                using (var writer = File.CreateText(PATH))
                {
                    writer.WriteLine("{0, 10}\t |{1, 10}\t |{2,10}\t |{3,15}\n",
                        "Пометка","Рег. Номер","Дата","Пользователь");
                }
            }
        }

        public static void Adding(string regNum)
        {
            AddData("Добавлено",regNum, Session.GetUser().UserName);
        }
        
        public static void Eding(string regNum)
        {
            AddData("Изменено",regNum, Session.GetUser().UserName);
        }
        
        public static void Deleting(string regNum)
        {
            AddData("Удалено", regNum, Session.GetUser().UserName);
        }


        private static void AddData(string type, string regNum, string user)
        {
            using (var writer = File.AppendText(PATH))
            {
                writer.WriteLine("{0, 10}\t|{1, 10}\t|{2,10}\t|{3,15}",
                    type, regNum, DateTime.Now.ToString("d"), user);
            }
        }
    }
}