using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using Npgsql;
using Project_Ads.MVVM.Model;

namespace Project_Ads.Core
{
    public class Connection
    {
        private const string Host = "server.evgfilim1.me";
        private const string User = "pisya";
        private const string DBname = "pis";
        private const string Password = "pisya";
        private const string Port = "5432";

        public static string ConnString =>
            $"Server={Host};Username={User};Database={DBname};Port={Port};Password={Password};SSLMode=Prefer";

        public static void ExecuteNonQuery(string sql, Tuple<string, object>[] data)
        {
            var command = new NpgsqlCommand(sql, App.Conn);
            for (int i = 0; i < data.Length; i++)
                command.Parameters.AddWithValue(data[i].Item1, data[i].Item2);
            var rows = command.ExecuteNonQuery();
        }

        public static int ExecuteGetLastRegNum(string sql)
        {
            var command = new NpgsqlCommand(sql, App.Conn);
            var reader = command.ExecuteReader();
            int regNum = 0;
            while (reader.Read())
                regNum = Convert.ToInt32(reader[0]);
            return regNum;
        }

        public static List<Advertisement> ExecuteGetAdvertisementList(string sql, List<Animal> animals) // ДОДЕЛАТЬ
        {
            var command = new NpgsqlCommand(sql, App.Conn);
            var reader = command.ExecuteReader();
            var advs = new List<Advertisement>();
            while (reader.Read())
            {
                advs.Add(Advertisement.CreateAdv(
                    new User()
                    {
                        UserId = (int) reader[1],
                        UserPhone = reader[8].ToString()
                    },
                    reader[3].ToString(),
                    reader[4].ToString(),
                    Convert.ToDateTime(reader[6]),
                    Convert.ToDateTime(reader[5]),
                    reader[2].ToString() == "Find"
                        ? Advertisement.AdvertisementType.Find
                        : Advertisement.AdvertisementType.Lose,
                    (int) reader[0],
                    animals.FirstOrDefault(animal => animal.Num == (int) reader[7])
                ));
            }
            return advs;
        }

        public static List<Animal> ExecuteGetAnimalList(string sql)
        {
            var command = new NpgsqlCommand(sql, App.Conn);
            var reader = command.ExecuteReader();
            var animals = new List<Animal>();
            while (reader.Read())
            {
                animals.Add(new Animal()
                {
                    Num = (int) reader[0],
                    Type = reader[1].ToString() == "Cat" ? Animal.Types.Cat : Animal.Types.Dog,
                    Color = reader[2].ToString(),
                    Pic = reader[3].ToString()
                });
            }
            return animals;
        }
        
        public static int ExecuteUserRegistration(string sql, Tuple<string, object>[] data)
        {
            var command = new NpgsqlCommand(sql, App.Conn);
            for (int i = 0; i < data.Length; i++)
                command.Parameters.AddWithValue(data[i].Item1, data[i].Item2);
            var reader = command.ExecuteReader();
            int userId = -1;
            while (reader.Read())
                userId = (int)reader[0];
            return userId;
        }

        public static User ExecuteUserAuthorization(string sql, Tuple<string, object>[] data)
        {
            var command = new NpgsqlCommand(sql, App.Conn);
            for (int i = 0; i < data.Length; i++)
                command.Parameters.AddWithValue(data[i].Item1, data[i].Item2);
            var reader = command.ExecuteReader();
            var user = new User();
            while (reader.Read())
            {
                var role = MVVM.Model.User.Role.NotAuthorizedUser;
                var rights = new Rights(true, true, true, true, false);
                if (reader[4].ToString() == "user")
                    role = MVVM.Model.User.Role.User;
                else if (reader[4].ToString() == "admin")
                {
                    role = MVVM.Model.User.Role.Admin;
                    rights.IsAdmin = true;
                }
                // возможно все что выше надо переместить куда то в другое место
                user = new User()
                {
                    UserId = (int) reader[0],
                    UserName = reader[1].ToString(),
                    UserPhone = reader[2].ToString(),
                    UserRole = role,
                    UserRights = rights
                };
            }
            return user;
        }
    }
}