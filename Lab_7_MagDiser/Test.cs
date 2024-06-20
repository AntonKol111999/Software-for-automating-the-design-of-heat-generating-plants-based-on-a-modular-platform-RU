using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
/*
namespace Lab_7_MagDiser
{
    internal class Test
    {
        static void Main(string[] args)
        {
            string connectionString = "server=localhost;user=root;password=your_password;database=boilerdetails";

            using (var connection = new MySqlConnection(connectionString))
            {
                connection.Open();

                // Создание таблицы, если она не существует
                string createTableQuery = "CREATE TABLE IF NOT EXISTS boiler (id INT AUTO_INCREMENT PRIMARY KEY, name VARCHAR(50), type VARCHAR(50), power INT)";
                var createTableCommand = new MySqlCommand(createTableQuery, connection);
                createTableCommand.ExecuteNonQuery();

                while (true)
                {
                    Console.WriteLine("1. Просмотреть данные");
                    Console.WriteLine("2. Добавить данные");
                    Console.WriteLine("3. Обновить данные");
                    Console.WriteLine("4. Удалить данные");
                    Console.WriteLine("5. Выйти");

                    Console.WriteLine("Выберите действие:");
                    string choice = Console.ReadLine();

                    switch (choice)
                    {
                        case "1":
                            ShowData(connection);
                            break;
                        case "2":
                            InsertData(connection);
                            break;
                        case "3":
                            UpdateData(connection);
                            break;
                        case "4":
                            DeleteData(connection);
                            break;
                        case "5":
                            return;
                        default:
                            Console.WriteLine("Неверный выбор.");
                            break;
                    }

                    Console.WriteLine("---------------------------");
                }
            }
        }

        static void ShowData(MySqlConnection connection)
        {
            string query = "SELECT * FROM boiler";
            var command = new MySqlCommand(query, connection);

            using (var reader = command.ExecuteReader())
            {
                Console.WriteLine("ID\tName\tType\tPower");
                Console.WriteLine("---------------------------");

                while (reader.Read())
                {
                    Console.WriteLine($"{reader["id"]}\t{reader["name"]}\t{reader["type"]}\t{reader["power"]}");
                }
            }
        }

        static void InsertData(MySqlConnection connection)
        {
            Console.Write("Введите имя котла: ");
            string name = Console.ReadLine();

            Console.Write("Введите тип котла: ");
            string type = Console.ReadLine();

            Console.Write("Введите мощность котла: ");
            int power = Convert.ToInt32(Console.ReadLine());

            string query = "INSERT INTO boiler (name, type, power) VALUES (@name, @type, @power)";
            var command = new MySqlCommand(query, connection);
            command.Parameters.AddWithValue("@name", name);
            command.Parameters.AddWithValue("@type", type);
            command.Parameters.AddWithValue("@power", power);

            int rowsAffected = command.ExecuteNonQuery();
            Console.WriteLine($"{rowsAffected} запись(ей) добавлено в таблицу.");
        }

        static void UpdateData(MySqlConnection connection)
        {
            Console.Write("Введите ID котла, который хотите обновить: ");
            int id = Convert.ToInt32(Console.ReadLine());

            Console.Write("Введите новое имя котла: ");
            string name = Console.ReadLine();

            Console.Write("Введите новый тип котла: ");
            string type = Console.ReadLine();

            Console.Write("Введите новую мощность котла: ");
            int power = Convert.ToInt32(Console.ReadLine());

            string query = "UPDATE boiler SET name = @name, type = @type, power = @power WHERE id = @id";
            var command = new MySqlCommand(query, connection);
            command.Parameters.AddWithValue("@name", name);
            command.Parameters.AddWithValue("@type", type);
            command.Parameters.AddWithValue("@power", power);
            command.Parameters.AddWithValue("@id", id);

            int rowsAffected = command.ExecuteNonQuery();
            Console.WriteLine($"{rowsAffected} запись(ей) обновлено.");
        }

        static void DeleteData(MySqlConnection connection)
        {
            Console.Write("Введите ID котла, который хотите удалить: ");
            int id = Convert.ToInt32(Console.ReadLine());

            string query = "DELETE FROM boiler WHERE id = @id";
            var command = new MySqlCommand(query, connection);
            command.Parameters.AddWithValue("@id", id);

            int rowsAffected = command.ExecuteNonQuery();
            Console.WriteLine($"{rowsAffected} запись(ей) удалено из таблицы.");
        }
    }
}

*/