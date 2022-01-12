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

        private static string ConnString =>
            $"Server={Host};Username={User};Database={DBname};Port={Port};Password={Password};SSLMode=Prefer";
        
        private static NpgsqlConnection _conn = new NpgsqlConnection(ConnString);

        public static void ExecuteCreateAdvertisement(int userId, string advType, string address, 
            int idAnimal, string description, DateTime dateEvent, DateTime dateCreate)
        {
            _conn.Open();
            var sql =
                "INSERT INTO advertisement (id_user, type, address, id_animal, description, date_event, date_create) VALUES (@id_user, @type, @address, @id_animal, @description, @date_event, @date_create)";
            var command = new NpgsqlCommand(sql, _conn);
            command.Parameters.AddWithValue("id_user", userId);
            command.Parameters.AddWithValue("type", advType);
            command.Parameters.AddWithValue("address", address);
            command.Parameters.AddWithValue("id_animal", idAnimal);
            command.Parameters.AddWithValue("description", description);
            command.Parameters.AddWithValue("date_event", dateEvent);
            command.Parameters.AddWithValue("date_create", dateCreate);
            var rows = command.ExecuteNonQuery();
            _conn.Close();
        }
        
        public static int ExecuteCreateAnimal(int anType, string animalColor, string pic)
        {
            _conn.Open();
            var sql =
                "INSERT INTO animal (type_id, description, path) VALUES (@type_id, @description, @path) RETURNING id";
            var command = new NpgsqlCommand(sql, _conn);
            command.Parameters.AddWithValue("type_id", anType);
            command.Parameters.AddWithValue("description", animalColor);
            command.Parameters.AddWithValue("path", pic);
            var reader = command.ExecuteReader();
            int animal_id = -1;
            while (reader.Read())
                animal_id = (int) reader[0];
            _conn.Close();
            return animal_id;
        }
        
        public static void ExecuteEditAdvertisement(string address,  string description,
            DateTime dateEvent, int regNum, string animalColor, string pic, int idAnimal)
        {
            _conn.Open();
            var sqlAdv =
                "UPDATE advertisement SET address = @address, description = @description, date_event = @date_event WHERE reg_num = @reg_num";
            var command = new NpgsqlCommand(sqlAdv, _conn);
            command.Parameters.AddWithValue("address", address);
            command.Parameters.AddWithValue("description", description);
            command.Parameters.AddWithValue("date_event", dateEvent);
            command.Parameters.AddWithValue("reg_num", regNum);
            var rowsAdv = command.ExecuteNonQuery();
            var sqlAnimal = "UPDATE animal SET description = @animalColor, path = @pic WHERE id = @num";
            command = new NpgsqlCommand(sqlAnimal, _conn);
            command.Parameters.AddWithValue("animalColor", animalColor);
            command.Parameters.AddWithValue("pic", pic);
            command.Parameters.AddWithValue("num", idAnimal);
            var rowsAnimal = command.ExecuteNonQuery();
            _conn.Close();
        }
        
        public static void ExecuteDeleteAdvertisement(int regNum, DateTime dateRemove)
        {
            _conn.Open();
            var sql = "UPDATE advertisement SET date_remove = @date_remove WHERE reg_num = @regNum";
            var command = new NpgsqlCommand(sql, _conn);
            command.Parameters.AddWithValue("regNum", regNum);
            command.Parameters.AddWithValue("date_remove", dateRemove);
            var rows = command.ExecuteNonQuery();
            _conn.Close();
        }

        public static int ExecuteGetLastRegNum()
        {
            _conn.Open();
            var sql = "SELECT reg_num FROM advertisement ORDER BY reg_num DESC LIMIT 1";
            var command = new NpgsqlCommand(sql, _conn);
            var reader = command.ExecuteReader();
            int regNum = 0;
            while (reader.Read())
                regNum = Convert.ToInt32(reader[0]);
            _conn.Close();
            return regNum;
        }

        public static List<Advertisement> ExecuteGetAdvertisementList(List<Animal> animals) // ДОДЕЛАТЬ
        {
            _conn.Open();
            var sql =
                "SELECT a.reg_num, a.id_user, a.type, a.address, a.description, a.date_event, a.date_create, a.id_animal, u.phone FROM advertisement a INNER JOIN \"user\" u on u.id = a.id_user WHERE a.date_remove IS NULL";
            var command = new NpgsqlCommand(sql, _conn);
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
                    reader[2].ToString() == "Find" ? 1 : 0,
                    (int) reader[0],
                    animals.FirstOrDefault(animal => animal.Num == (int) reader[7])
                ));
            }
            _conn.Close();
            return advs;
        }

        public static List<Animal> ExecuteGetAnimalList()
        {
            _conn.Open();
            var sql =
                "SELECT a.id, a2.type, a.description, a.path FROM animal a INNER JOIN animal_type a2 on a2.id = a.type_id";
            var command = new NpgsqlCommand(sql, _conn);
            var reader = command.ExecuteReader();
            var animals = new List<Animal>();
            while (reader.Read())
            {
                animals.Add(Animal.CreateAnimal(
                    reader[2].ToString(),
                    reader[3].ToString(),
                    reader[1].ToString() == "Cat" ? 0 : 1,
                    (int) reader[0]
                ));
            }
            _conn.Close();
            return animals;
        }
        
        public static int ExecuteUserRegistration(string name, string login, string password, string phone, int role = 3)
        {
            _conn.Open();
            var sql =
                "INSERT INTO \"user\" (name, login, password, phone, id_role) VALUES (@name, @login, @password, @phone, @role) RETURNING id";
            var command = new NpgsqlCommand(sql, _conn);
            command.Parameters.AddWithValue("name", name);
            command.Parameters.AddWithValue("login", login);
            command.Parameters.AddWithValue("password", password);
            command.Parameters.AddWithValue("phone", phone);
            command.Parameters.AddWithValue("role", role);
            try
            {
                var reader = command.ExecuteReader();
                var userId = -1;
                while (reader.Read())
                    userId = (int) reader[0];
                return userId;
            }
            finally
            {
                _conn.Close();
            }
        }

        public static User ExecuteUserAuthorization(string login, string password)
        {
            _conn.Open();
            var sql =
                "SELECT u.id, u.name, u.phone, r.role_name FROM \"user\" u INNER JOIN role r on r.id = u.id_role WHERE u.login = @login AND u.password = @password";
            var command = new NpgsqlCommand(sql, _conn);
            command.Parameters.AddWithValue("login", login);
            command.Parameters.AddWithValue("password", password);
            var reader = command.ExecuteReader();
            var user = new User();
            while (reader.Read())
            {
                var role = MVVM.Model.User.Role.NotAuthorizedUser;
                if (reader[3].ToString() == "user")
                    role = MVVM.Model.User.Role.User;
                else if (reader[3].ToString() == "admin")
                    role = MVVM.Model.User.Role.Admin;
                
                // возможно все что выше надо переместить куда то в другое место
                user = new User()
                {
                    UserId = (int) reader[0],
                    UserName = reader[1].ToString(),
                    UserPhone = reader[2].ToString(),
                    UserRole = role,
                };
            }
            _conn.Close();
            if (user.UserName is null)
                throw new Exception();
            return user;
        }
    }
}