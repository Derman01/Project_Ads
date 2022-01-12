using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        
        public static void ExecuteCreateAdvertisement(
            string sql, int userId, string advType, string address, int idAnimal, string description,
            DateTime dateEvent, DateTime dateCreate)
        {
            App.Conn.Open();
            var command = new NpgsqlCommand(sql, App.Conn);
            command.Parameters.AddWithValue("id_user", userId);
            command.Parameters.AddWithValue("type", advType);
            command.Parameters.AddWithValue("address", address);
            command.Parameters.AddWithValue("id_animal", idAnimal);
            command.Parameters.AddWithValue("description", description);
            command.Parameters.AddWithValue("date_event", dateEvent);
            command.Parameters.AddWithValue("date_create", dateCreate);
            var rows = command.ExecuteNonQuery();
            App.Conn.Close();
        }
        
        public static int ExecuteCreateAnimal(
            string sql, int anType, string animalColor, string pic)
        {
            App.Conn.Open();
            var command = new NpgsqlCommand(sql, App.Conn);
            command.Parameters.AddWithValue("type_id", anType);
            command.Parameters.AddWithValue("description", animalColor);
            command.Parameters.AddWithValue("path", pic);
            var reader = command.ExecuteReader();
            int animal_id = -1;
            while (reader.Read())
                animal_id = (int) reader[0];
            App.Conn.Close();
            return animal_id;
        }
        
        public static void ExecuteEditAdvertisement(
            string sqlAdv, string sqlAnimal, string address,  string description,
            DateTime dateEvent, int regNum, string animalColor, string pic, int idAnimal)
        {
            App.Conn.Open();
            var command = new NpgsqlCommand(sqlAdv, App.Conn);
            command.Parameters.AddWithValue("address", address);
            command.Parameters.AddWithValue("description", description);
            command.Parameters.AddWithValue("date_event", dateEvent);
            command.Parameters.AddWithValue("reg_num", regNum);
            var rowsAdv = command.ExecuteNonQuery();
            command = new NpgsqlCommand(sqlAnimal, App.Conn);
            command.Parameters.AddWithValue("animalColor", animalColor);
            command.Parameters.AddWithValue("pic", pic);
            command.Parameters.AddWithValue("num", idAnimal);
            var rowsAnimal = command.ExecuteNonQuery();
            App.Conn.Close();
        }
        
        public static void ExecuteDeleteAdvertisement(string sql, int regNum)
        {
            App.Conn.Open();
            var command = new NpgsqlCommand(sql, App.Conn);
            command.Parameters.AddWithValue("reg_num", regNum);
            var rows = command.ExecuteNonQuery();
            App.Conn.Close();
        }

        public static int ExecuteGetLastRegNum(string sql)
        {
            App.Conn.Open();
            var command = new NpgsqlCommand(sql, App.Conn);
            var reader = command.ExecuteReader();
            int regNum = 0;
            while (reader.Read())
                regNum = Convert.ToInt32(reader[0]);
            App.Conn.Close();
            return regNum;
        }

        public static ObservableCollection<Advertisement> ExecuteGetAdvertisementList(string sql, List<Animal> animals) // ДОДЕЛАТЬ
        {
            App.Conn.Open();
            var command = new NpgsqlCommand(sql, App.Conn);
            var reader = command.ExecuteReader();
            var advs = new ObservableCollection<Advertisement>();
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
            App.Conn.Close();
            return advs;
        }

        public static List<Animal> ExecuteGetAnimalList(string sql)
        {
            App.Conn.Open();
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
            App.Conn.Close();
            return animals;
        }
        
        public static int ExecuteUserRegistration(
            string sql, string name, string login, string password, string phone, int role = 3)
        {
            App.Conn.Open();
            var command = new NpgsqlCommand(sql, App.Conn);
            command.Parameters.AddWithValue("name", name);
            command.Parameters.AddWithValue("login", login);
            command.Parameters.AddWithValue("password", password);
            command.Parameters.AddWithValue("phone", phone);
            command.Parameters.AddWithValue("role", role);
            var reader = command.ExecuteReader();
            int userId = -1;
            while (reader.Read())
                userId = (int)reader[0];
            App.Conn.Close();
            return userId;
        }

        public static User ExecuteUserAuthorization(string sql, string login, string password)
        {
            App.Conn.Open();
            var command = new NpgsqlCommand(sql, App.Conn);
            command.Parameters.AddWithValue("login", login);
            command.Parameters.AddWithValue("password", password);
            var reader = command.ExecuteReader();
            var user = new User();
            while (reader.Read())
            {
                var role = MVVM.Model.User.Role.NotAuthorizedUser;
                var rights = new Rights(true, true, true, true, false);
                if (reader[3].ToString() == "user")
                    role = MVVM.Model.User.Role.User;
                else if (reader[3].ToString() == "admin")
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
            App.Conn.Close();
            if (user.UserName is null)
                throw new Exception();
            return user;
        }
    }
}