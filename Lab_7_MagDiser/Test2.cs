using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
/*
namespace Lab_7_MagDiser
{
    internal class Test2
    {
        static void Main(string[] args)
        {
            // Строка подключения к базе данных MySQL
            string connectionString = "server=localhost;user=root;password=your_password;database=boilerdetails";

            using (var connection = new MySqlConnection(connectionString))
            {
                connection.Open();

                // Создание таблицы, если она не существует
                string createTableQuery = "CREATE TABLE IF NOT EXISTS boiler (id INT AUTO_INCREMENT PRIMARY KEY, name VARCHAR(50), type VARCHAR(50), power DOUBLE, pressure DOUBLE, KPD DOUBLE)";
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
            // Запрос для выборки всех данных из таблицы
            string query = "SELECT * FROM boiler";
            var command = new MySqlCommand(query, connection);

            using (var reader = command.ExecuteReader())
            {
                Console.WriteLine("ID\tName\tType\tPower\tPressure\tKPD ");
                Console.WriteLine("---------------------------");

                while (reader.Read())
                {
                    Console.WriteLine($"{reader["id"]}\t{reader["name"]}\t{reader["type"]}\t{reader["power"]}\t{reader["pressure"]}\t{reader["KPD"]}");
                }
            }
        }

        static void InsertData(MySqlConnection connection)
        {
            Console.Write("Введите имя котла: ");
            string name = Console.ReadLine();

            Console.Write("Введите тип котла: ");
            string type = Console.ReadLine();

            Console.Write("Введите мощность детали: ");
            double power = Convert.ToDouble(Console.ReadLine());

            Console.Write("Введите даление деталли: ");
            double pressure = Convert.ToDouble(Console.ReadLine());

            Console.Write("Введите КПД детали: ");
            double KPD = Convert.ToDouble(Console.ReadLine());

            // Запрос для вставки новых данных в таблицу
            string query = "INSERT INTO boiler (name, type, power, pressure, KPD) VALUES (@name, @type, @power, @pressure, @KPD)";
            var command = new MySqlCommand(query, connection);
            command.Parameters.AddWithValue("@name", name); // Добавление параметра @name
            command.Parameters.AddWithValue("@type", type); // Добавление параметра @type
            command.Parameters.AddWithValue("@power", power); // Добавление параметра @power
            command.Parameters.AddWithValue("@pressure", pressure); // Добавление параметра @pressure
            command.Parameters.AddWithValue("@KPD", KPD); // Добавление параметра @KPD

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

            Console.Write("Введите новую мощность детали: ");
            double power = Convert.ToDouble(Console.ReadLine());

            Console.Write("Введите новое давление детали: ");
            double pressure = Convert.ToDouble(Console.ReadLine());

            Console.Write("Введите новое КПД детали: ");
            double KPD = Convert.ToDouble(Console.ReadLine());

            // Запрос для обновления данных в таблице
            string query = "UPDATE boiler SET name = @name, type = @type, power = @power, pressure = @pressure, KPD = @KPD WHERE id = @id";
            var command = new MySqlCommand(query, connection);
            command.Parameters.AddWithValue("@name", name); // Добавление параметра @name
            command.Parameters.AddWithValue("@type", type); // Добавление параметра @type
            command.Parameters.AddWithValue("@power", power); // Добавление параметра @power
            command.Parameters.AddWithValue("@type", pressure); // Добавление параметра @type
            command.Parameters.AddWithValue("@power", KPD); // Добавление параметра @power
            command.Parameters.AddWithValue("@id", id); // Добавление параметра @id

            int rowsAffected = command.ExecuteNonQuery();
            Console.WriteLine($"{rowsAffected} запись(ей) обновлено.");
        }

        static void DeleteData(MySqlConnection connection)
        {
            Console.Write("Введите ID котла, который хотите удалить: ");
            int id = Convert.ToInt32(Console.ReadLine());

            // Запрос для удаления данных из таблицы
            string query = "DELETE FROM boiler WHERE id = @id";
            var command = new MySqlCommand(query, connection);
            command.Parameters.AddWithValue("@id", id); // Добавление параметра @id

            int rowsAffected = command.ExecuteNonQuery();
            Console.WriteLine($"{rowsAffected} запись(ей) удалено из таблицы.");
        }
    }
}
*/