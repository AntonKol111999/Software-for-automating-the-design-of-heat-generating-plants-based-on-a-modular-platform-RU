using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
//using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
// для работы с БД
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace Lab_7_MagDiser
{   
    public partial class Form1 : Form
    {
        double Frame_Volume;
        double Boiler_Thermal_Power;
        double Boiler_Performance;
        double Total_Thermal_Power;
        double Chimney_Thermal_Conductivity;
        double Harness_Thermal_Conductivity;
        double Economizer_Heat_Transfer_Coefficient;
        double Economizer_Resistance;
        double Regenerator_Heat_Transfer_Coefficient;
        double Regenerator_Thermal_Conductivity;
        double Regenerator_Resistance;
        double Gas_Purifier_Resistance;
        double Total_Resistance;
        double Total_Thermal_Conductivity;

        /*
        double totalPower;
        double pressure;
        double KPD;
        double vRoom;
        // Переменные для деталей:
        //Котел
        double BoilerPower;
        double BoilerPressure;
        double BoilerKPD;
        //Насос
        double PumpPower;
        */
        // Подключение к БД      
        private SqlConnection sqlConnection = null;
        //private SqlConnection sqlConnection = "Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=D:\С дисков C и D\C#\Lab_7_MagDiser с БД (передел.)\Lab_7_MagDiser\TestDB.mdf;Integrated Security=True";
        //String sqlConnection = "Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=D:\С дисков C и D\C#\Lab_7_MagDiser с БД (передел.)\Lab_7_MagDiser\TestDB.mdf;Integrated Security=True";
        //string constr = "server=localhost;port=3306;username=root;password=root;datebase=";   
        public String way = "";
        //public String sqlConnection = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=" + way + ";Integrated Security=True";
        int selectedRow;        
        String TableSelection;

        public Form1()
        {
            InitializeComponent();
            comboBox1.Items.AddRange(new string[] { "Самый легкий", "Самые малые габариты", "Наибольшая производительность", "Наибольшее максимальное давление котла" });
            string[] countries = { "Дымоход: ", "Материал", "Диаметр", "Высота", "Теплопроводимость", "Система отчисти", "",
            "Обвязка:", "Материал", "Клапан", "Теплопроводимость", "",
            "Экономайзкр:", "Тип", "Площадь теплообмена", "Коэффициент теплопередачи", "Материал", "Аэродинамическое cопротивление", "",
            "Регенератор:", "Тип", "Площадь теплообмена: ", "Коэффициент теплопередачи: ", "Материал: ","Аэродинамическое cопротивление: ", "",
            "Газоочиститель", "Тип", "Эффективность очистки", "Производительность", "Аэродинам. cопротивление", "",
            "Система подавления шума: ", "Тип", "Уровень шумоподавления", "Тип материала", "Площадь", "Эффективность", "",
            "", "", ""
            };                      

            listBox1.Items.AddRange(countries);
        }
        // тестовое подключение к БД (ощибка ()удалить)
                    
        //Вывод(обновление) данных из БД
        private void UpdateTable(String TableName, DataGridView dataGridView)
        {
            SqlDataAdapter dataAdapter = new SqlDataAdapter("Select * FROM " + TableName, sqlConnection);
            DataSet dataSet = new DataSet();
            dataAdapter.Fill(dataSet);
            dataGridView.DataSource = dataSet.Tables[0];
        }

        private void CreateColumns(DataGridView dgw, String TableName)
        {
            dgw.Columns.Clear();
            switch (TableName)
            {
                case ("Frame_Table"):
                    dataGridView1.Columns.Add("id", "id");
                    dataGridView1.Columns.Add("Name", "Наименование");
                    dataGridView1.Columns.Add("Length", "Длина");
                    dataGridView1.Columns.Add("Height", "Высота");
                    dataGridView1.Columns.Add("Width", "Ширина");
                    dataGridView1.Columns.Add("Weight", "Масса");
                    dataGridView1.Columns.Add("Heat_Loss", "Тепл. потери");
                    break;

                case ("Boiler_Table"):
                    dataGridView1.Columns.Add("id", "id");
                    dataGridView1.Columns.Add("Name", "Наименование");
                    dataGridView1.Columns.Add("Max_KPD", "Макс. КПД, %");
                    dataGridView1.Columns.Add("Heating_Temperature", "Температура обогрева, С");
                    dataGridView1.Columns.Add("Max_Overpressure", "Макс. Давление, бар");
                    dataGridView1.Columns.Add("Thermal_Power", "Тепловая мощность, Вт");
                    dataGridView1.Columns.Add("Height", "Высота, см");
                    dataGridView1.Columns.Add("Width", "Длина, см");
                    dataGridView1.Columns.Add("Depth", "Глубина, см");
                    dataGridView1.Columns.Add("Weight", "Вес, кг");
                    break;

                case ("Pump_Table"):
                    dataGridView1.Columns.Add("id", "id");
                    dataGridView1.Columns.Add("Name", "Наименование");
                    dataGridView1.Columns.Add("Performance", "Производительность");
                    dataGridView1.Columns.Add("Max_UP_Pressure", "Напор, м.");
                    dataGridView1.Columns.Add("Max_Overpressure", "Макс. Давление, бар");
                    dataGridView1.Columns.Add("Operating_Voltage", "Вольтаж, Вт");                    
                    break;

                    // Таблицы ниже еще не сделаны !позже !!!

                case ("Chimney_Table"): // Дымоход
                    dataGridView1.Columns.Add("id", "id");
                    dataGridView1.Columns.Add("Name", "Наименование");
                    dataGridView1.Columns.Add("Material", "Материал");
                    dataGridView1.Columns.Add("Diameter", "Диаметр, мм");
                    dataGridView1.Columns.Add("Height", "Высота, м");
                    dataGridView1.Columns.Add("Thermal_Conductivity", "Теплопроводность, Вт/(м·°С)");
                    dataGridView1.Columns.Add("Cleaning_System", "Система очистки");                    
                    break;

                case ("Harness_Table"): // Обвязка
                    dataGridView1.Columns.Add("id", "id");
                    dataGridView1.Columns.Add("Name", "Наименование");
                    dataGridView1.Columns.Add("Material", "Материал");
                    dataGridView1.Columns.Add("Valves", "Арматура");                    
                    dataGridView1.Columns.Add("Thermal_Conductivity", "Теплопроводность, Вт/(м·°С)");                    
                    break;

                    // Дополнительные элементы:

                case ("Economizer_Table"): // Экономайзер
                    dataGridView1.Columns.Add("id", "id");
                    dataGridView1.Columns.Add("Name", "Наименование");
                    dataGridView1.Columns.Add("Type", "Тип"); // Поверхностный, регенеративный.
                    dataGridView1.Columns.Add("Heat_Exchange_Area", "Площадь теплообмена, м²");
                    dataGridView1.Columns.Add("Heat_Transfer_Coefficient", "Коэффициент теплопередачи, Вт/(м²*°C)");
                    dataGridView1.Columns.Add("Material", "Материал");
                    dataGridView1.Columns.Add("Resistance", "Аэродинамическое cопротивление, МПа (мм.вод.ст.)"); // или Гидравлическое сопротивление МПа (кгс/см2)
                    break;

                case ("Regenerator_Table"): // Регениратор
                    dataGridView1.Columns.Add("id", "id");
                    dataGridView1.Columns.Add("Name", "Наименование");
                    dataGridView1.Columns.Add("Type", "Тип"); // Прямоточный, противоточный.
                    dataGridView1.Columns.Add("Heat_Exchange_Area", "Площадь теплообмена, м²");
                    dataGridView1.Columns.Add("Heat_Transfer_Coefficient", "Коэффициент теплопередачи, Вт/(м²*°C)");
                    dataGridView1.Columns.Add("Material", "Материал");
                    dataGridView1.Columns.Add("Resistance", "Аэродинам. cопротивление, МПа (мм.вод.ст.)"); // или Гидравлическое сопротивление МПа (кгс/см2)                    
                    break;

                case ("Gas_Purifier_Table"): // Газоочиститеть
                    dataGridView1.Columns.Add("id", "id");
                    dataGridView1.Columns.Add("Name", "Наименование");
                    dataGridView1.Columns.Add("Type", "Тип"); // Механическая, электростатическая, мокрая, каталитическая.
                    dataGridView1.Columns.Add("Cleaning_Efficiency", "Эффективность очистки, %"); // Прямоточный, противоточный.
                    dataGridView1.Columns.Add("Productivity", "Производительность, (м3/сут)");                    
                    dataGridView1.Columns.Add("Resistance", "Аэродинам. cопротивление, МПа (мм.вод.ст.)"); // или Гидравлическое сопротивление МПа (кгс/см2)                    
                    break;

                case ("Noise_Reduction_System_Table"): // Система подавления шума
                    dataGridView1.Columns.Add("id", "id");
                    dataGridView1.Columns.Add("Name", "Наименование");
                    dataGridView1.Columns.Add("Type", "Тип"); // Звукоизоляция, звукопоглощение.
                    dataGridView1.Columns.Add("Noise_Reduction_Level", "Уровень шумоподавления, Гц"); // Прямоточный, противоточный.
                    dataGridView1.Columns.Add("Material_Type", "Тип материала"); // Звукоизоляционные панели, звукопоглощающие материалы.
                    dataGridView1.Columns.Add("Area", "Площадь, м²");
                    dataGridView1.Columns.Add("Efficiency", "Эффективность, м²"); 
                    break;
            }            
        }
        
        private void ReadSingleRow(DataGridView dgw, IDataRecord record, String TableName)
        {
            switch (TableName)
            {
                case ("Frame_Table"):

                    dgw.Rows.Add(record.GetInt32(0), record.GetString(1), record.GetDouble(2), record.GetDouble(3), record.GetDouble(4), record.GetDouble(5), record.GetDouble(6));

                    break;

                case ("Boiler_Table"):

                    dgw.Rows.Add(record.GetInt32(0), record.GetString(1), record.GetDouble(2), record.GetDouble(3), record.GetDouble(4), record.GetDouble(5), record.GetDouble(6), record.GetDouble(7), record.GetDouble(8), record.GetDouble(9));

                    break;

                case ("Pump_Table"):

                    dgw.Rows.Add(record.GetInt32(0), record.GetString(1), record.GetDouble(2), record.GetDouble(3), record.GetDouble(4), record.GetDouble(5));

                    break;

                case ("Chimney_Table"):

                    dgw.Rows.Add(record.GetInt32(0), record.GetString(1), record.GetString(2), record.GetDouble(3), record.GetDouble(4), record.GetDouble(5), record.GetString(6));

                    break;

                case ("Harness_Table"):

                    dgw.Rows.Add(record.GetInt32(0), record.GetString(1), record.GetString(2), record.GetString(3), record.GetDouble(4));
                    
                    break;

                case ("Economizer_Table"):

                    dgw.Rows.Add(record.GetInt32(0), record.GetString(1), record.GetString(2), record.GetDouble(3), record.GetDouble(4), record.GetString(5), record.GetDouble(6));

                    break;

                case ("Regenerator_Table"):

                    dgw.Rows.Add(record.GetInt32(0), record.GetString(1), record.GetString(2), record.GetDouble(3), record.GetDouble(4), record.GetString(5), record.GetDouble(6));

                    break;

                case ("Gas_Purifier_Table"):

                    dgw.Rows.Add(record.GetInt32(0), record.GetString(1), record.GetString(2), record.GetDouble(3), record.GetDouble(4), record.GetDouble(5));

                    break;

                case ("Noise_Reduction_System_Table"):

                    dgw.Rows.Add(record.GetInt32(0), record.GetString(1), record.GetString(2), record.GetDouble(3), record.GetString(4), record.GetDouble(5), record.GetDouble(6));

                    break;
            }            
        }

        private void RefreshDataGrid(DataGridView dgw, String TableName)
        {            
            dgw.Rows.Clear();
            string queryString = $"select * from " + TableName;
            SqlCommand command = new SqlCommand(queryString, sqlConnection);
            //string cs = @"Data Source = (LocalDB)\MSSQLLocalDB; AttachDbFilename = " + textBox9.Text + @"\Lab_7_MagDiser с БД (передел.)\Lab_7_MagDiser\TestDB.mdf; Integrated Security = True";
            //SqlCommand command = new SqlCommand(cs);
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                ReadSingleRow(dgw, reader, TableName);
            }
            reader.Close();
        }       
        
        //Добавление Собраного котла в таблицу
        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "" || textBox2.Text == "" || textBox2.Text == "" || textBox3.Text == "" || textBox4.Text == "" || textBox5.Text == "" || textBox6.Text == "" || textBox7.Text == "" || textBox8.Text == "" || textBox10.Text == "" || textBox11.Text == "")
            {
                MessageBox.Show("Не все поля заполнены");
            }
            else if (label14.Text == "???" || label15.Text == "???" || label17.Text == "???" || label24.Text == "???" || label23.Text == "???" || label22.Text == "???" || label30.Text == "???" || label29.Text == "???" || label28.Text == "???")
            {
                MessageBox.Show("Не все детали выбраны");
            }
            else
            {
                SqlCommand command = new SqlCommand("INSERT INTO [Assembled_Installations] (Name, Frame, Boiler, Pump, Chimney_Table, Economizer_Table, Gas_Purifier_Table, Harness_Table, Noise_Reduction_System_Table, Regenerator_Table, Overall_Performance, Price) VALUES (@Name, @Frame, @Boiler, @Pump, @Chimney_Table, @Economizer_Table, @Gas_Purifier_Table, @Harness_Table, @Noise_Reduction_System_Table, @Regenerator_Table, @Overall_Performance, @Price)", sqlConnection);
                command.Parameters.AddWithValue("Name", textBox8.Text);
                command.Parameters.AddWithValue("Frame", label14.Text);
                command.Parameters.AddWithValue("Boiler", label15.Text);
                command.Parameters.AddWithValue("Pump", label17.Text);
                command.Parameters.AddWithValue("Chimney_Table", label24.Text);
                command.Parameters.AddWithValue("Economizer_Table", label22.Text);
                command.Parameters.AddWithValue("Gas_Purifier_Table", label29.Text);                
                command.Parameters.AddWithValue("Harness_Table", label23.Text);
                command.Parameters.AddWithValue("Noise_Reduction_System_Table", label28.Text);
                command.Parameters.AddWithValue("Regenerator_Table", label30.Text);

                string connectionString = @"Data Source = (LocalDB)\MSSQLLocalDB; AttachDbFilename = " + textBox9.Text + @"\Lab_7_MagDiser с БД (передел.)\Lab_7_MagDiser\TestDB.mdf; Integrated Security = True"; ;

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    
                    // расчет общей цены

                    float ValueCal(string Value, string tableName, string Name)
                    {               
                        string query = $@"SELECT {Value} FROM {tableName} WHERE Name = '{Name}'";
                        SqlCommand commandGet1_1 = new SqlCommand(query, connection);
                        float result = Convert.ToSingle(commandGet1_1.ExecuteScalar());

                        return result;
                    }

                    float Price1 = ValueCal("Price", "Frame_Table", label14.Text);
                    float Price2 = ValueCal("Price", "Boiler_Table", label15.Text);
                    float Price3 = ValueCal("Price", "Pump_Table", label17.Text);
                    float Price4 = ValueCal("Price", "Chimney_Table", label24.Text);
                    float Price5 = ValueCal("Price", "Economizer_Table", label22.Text);
                    float Price6 = ValueCal("Price", "Gas_Purifier_Table", label29.Text);
                    float Price7 = ValueCal("Price", "Harness_Table", label23.Text);
                    float Price8 = ValueCal("Price", "Noise_Reduction_System_Table", label28.Text);
                    float Price9 = ValueCal("Price", "Regenerator_Table", label30.Text);
                    
                    float totalPrice = Price1 + Price2 + Price3 + Price4 + Price5 + Price6 + Price7 + Price8 + Price9;

                    command.Parameters.AddWithValue("Price", totalPrice);

                    // расчет общей мощность

                    float Per1 = ValueCal("Performance", "Pump_Table", label17.Text);                    
                    float Per2 = ValueCal("Thermal_Power", "Boiler_Table", label15.Text);

                    float totalPer = Per1 + Per2;

                    command.Parameters.AddWithValue("Overall_Performance", totalPrice);
                }
                command.ExecuteNonQuery();
                MessageBox.Show("Новая установка создана");                
            }
        }

        public double totPower(double power1, double power2)
        {
            double total = power1 + power2;
            return total;
        }        
        public double totPowerCor(double totalPower)
        {
            double total = totalPower * 1000 / 40;
            return total;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form2 form2 = new Form2();
            form2.Show();
        }

        // Переход в редим администратора
        private void button3_Click(object sender, EventArgs e)
        {           
            Form4 form4 = new Form4();
            form4.Show();
        }      

        private void Form1_Load(object sender, EventArgs e)
        {
            /*
            //sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["TestDB"].ConnectionString);
            sqlConnection = new SqlConnection(@"Data Source = (LocalDB)\MSSQLLocalDB; AttachDbFilename = D:\С дисков C и D\C#\Lab_7_MagDiser с БД (передел.)\Lab_7_MagDiser\TestDB.mdf; Integrated Security = True");
            sqlConnection.Open();
            if (sqlConnection.State == ConnectionState.Open)
            {
                MessageBox.Show("База данных подключена");
            }
            */
        }

        // Кнопки для вывода таблиц БД в Data Grid View

        private void button4_Click(object sender, EventArgs e)
        {
            Show_Table_on_DataGridView("Frame_Table");            
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Show_Table_on_DataGridView("Boiler_Table");            
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Show_Table_on_DataGridView("Pump_Table");                       
        }

        private void button11_Click(object sender, EventArgs e)
        {
            Show_Table_on_DataGridView("Chimney_Table");            
        }

        private void button10_Click(object sender, EventArgs e)
        {
            Show_Table_on_DataGridView("Harness_Table");
        }

        private void button9_Click(object sender, EventArgs e)
        {
            Show_Table_on_DataGridView("Economizer_Table");
        }

        private void button14_Click(object sender, EventArgs e)
        {
            Show_Table_on_DataGridView("Regenerator_Table");
        }

        private void button13_Click(object sender, EventArgs e)
        {
            Show_Table_on_DataGridView("Gas_Purifier_Table");
        }

        private void button12_Click(object sender, EventArgs e)
        {
            Show_Table_on_DataGridView("Noise_Reduction_System_Table");
        }

        private void Show_Table_on_DataGridView(string Table_Name)
        {
            CreateColumns(dataGridView1, Table_Name);
            RefreshDataGrid(dataGridView1, Table_Name);
            TableSelection = Table_Name;
        }



        // Клик на ячеку БД
        double[] Weights = new double[2];
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            selectedRow = e.RowIndex;

            if (e.RowIndex >= 0)
            {         
                DataGridViewRow row = dataGridView1.Rows[selectedRow];
                //label11.Text = Convert.ToString(row);                               

                // Вывести название детали

                switch (TableSelection)
                {
                    case ("Frame_Table"):

                        label14.Text = Convert.ToString(row.Cells["Name"].Value);
                        textBox7.Text = Convert.ToString(Convert.ToDouble(row.Cells["Length"].Value) * Convert.ToDouble(row.Cells["Height"].Value) * Convert.ToDouble(row.Cells["Width"].Value));
                        textBox11.Text = Convert.ToString(row.Cells["Heat_Loss"].Value);
                        Frame_Volume = Convert.ToDouble(Convert.ToDouble(row.Cells["Length"].Value) * Convert.ToDouble(row.Cells["Height"].Value) * Convert.ToDouble(row.Cells["Width"].Value));
                        Weights[0] = Convert.ToDouble(row.Cells["Weight"].Value);                        
                        textBox10.Text = Convert.ToString(Weights.Sum());                                                  

                        break;

                    case ("Boiler_Table"):

                        label15.Text = Convert.ToString(row.Cells[1].Value);
                        textBox1.Text = Convert.ToString(row.Cells["Thermal_Power"].Value);
                        textBox2.Text = Convert.ToString(row.Cells["Max_Overpressure"].Value);
                        textBox3.Text = Convert.ToString(row.Cells["Max_KPD"].Value);
                        Weights[1] = Convert.ToDouble(row.Cells["Weight"].Value);
                        textBox10.Text = Convert.ToString(Weights.Sum());                        

                        // добавть в таблицу Котел теплопатери!!!

                        Boiler_Thermal_Power = Convert.ToDouble(row.Cells["Thermal_Power"].Value);

                        break;

                    case ("Pump_Table"):

                        label17.Text = Convert.ToString(row.Cells[1].Value);
                        textBox4.Text = Convert.ToString(row.Cells["Max_UP_Pressure"].Value);
                        textBox5.Text = Convert.ToString(row.Cells["Performance"].Value);
                        textBox6.Text = Convert.ToString(row.Cells["Operating_Voltage"].Value);

                        Boiler_Performance = Convert.ToDouble(row.Cells["Performance"].Value);
                        Total_Thermal_Power = Boiler_Thermal_Power * Boiler_Performance; // общая производительность

                        break;

                    case ("Chimney_Table"):

                        label24.Text = Convert.ToString(row.Cells[1].Value);
                        listBox1.Items[0] = "Дымоход: ";
                        listBox1.Items[1] = "Материал: " + Convert.ToString(row.Cells["Material"].Value);
                        listBox1.Items[2] = "Диаметр: " + Convert.ToString(row.Cells["Diameter"].Value) + " мм.";
                        listBox1.Items[3] = "Высота: " + Convert.ToString(row.Cells["Height"].Value) + " м.";
                        listBox1.Items[4] = "Теплопроводимость: " + Convert.ToString(row.Cells["Thermal_Conductivity"].Value) + " мВт/м·°C";
                        listBox1.Items[5] = "Система отчисти: " + Convert.ToString(row.Cells["Cleaning_System"].Value);

                        Chimney_Thermal_Conductivity = Convert.ToDouble(row.Cells["Thermal_Conductivity"].Value);

                        break;

                    case ("Harness_Table"):

                        label23.Text = Convert.ToString(row.Cells[1].Value);
                        listBox1.Items[6] = "";
                        listBox1.Items[7] = "Обвязка: ";
                        listBox1.Items[8] = "Материал: " + Convert.ToString(row.Cells["Material"].Value);
                        listBox1.Items[9] = "Клапан: " + Convert.ToString(row.Cells["Valves"].Value);
                        listBox1.Items[10] = "Теплопроводимость: " + Convert.ToString(row.Cells["Thermal_Conductivity"].Value) + " мВт/м·°C";

                        Harness_Thermal_Conductivity = Convert.ToDouble(row.Cells["Thermal_Conductivity"].Value);

                        break;

                    case ("Economizer_Table"):

                        label22.Text = Convert.ToString(row.Cells[1].Value);
                        listBox1.Items[11] = "";
                        listBox1.Items[12] = "Экономайзер: ";
                        listBox1.Items[13] = "Тип: " + Convert.ToString(row.Cells["Type"].Value);
                        listBox1.Items[14] = "Площадь теплообмена: " + Convert.ToString(row.Cells["Heat_Exchange_Area"].Value) + " м²";
                        listBox1.Items[15] = "Коэффициент теплопередачи: " + Convert.ToString(row.Cells["Heat_Transfer_Coefficient"].Value) + " Вт/(м²*°C)";
                        listBox1.Items[16] = "Материал: " + Convert.ToString(row.Cells["Material"].Value);
                        listBox1.Items[17] = "Аэродинамическое cопротивление: " + Convert.ToString(row.Cells["Resistance"].Value) + " МПа (мм.вод.ст.)";

                        Economizer_Heat_Transfer_Coefficient = Convert.ToDouble(row.Cells["Heat_Transfer_Coefficient"].Value);
                        Economizer_Resistance = Convert.ToDouble(row.Cells["Resistance"].Value);

                        break;

                    case ("Regenerator_Table"):

                        label30.Text = Convert.ToString(row.Cells[1].Value);
                        listBox1.Items[18] = "";
                        listBox1.Items[19] = "Регенератор: ";
                        listBox1.Items[20] = "Тип: " + Convert.ToString(row.Cells["Type"].Value);
                        listBox1.Items[21] = "Площадь теплообмена: " + Convert.ToString(row.Cells["Heat_Exchange_Area"].Value) + " м²";
                        listBox1.Items[22] = "Коэффициент теплопередачи: " + Convert.ToString(row.Cells["Heat_Transfer_Coefficient"].Value) + " Вт/(м²*°C)";
                        listBox1.Items[23] = "Материал: " + Convert.ToString(row.Cells["Material"].Value);
                        listBox1.Items[24] = "Аэродинамическое cопротивление: " + Convert.ToString(row.Cells["Resistance"].Value) + " МПа (мм.вод.ст.)";

                        Regenerator_Heat_Transfer_Coefficient = Convert.ToDouble(row.Cells["Heat_Transfer_Coefficient"].Value);
                        Total_Thermal_Conductivity = Chimney_Thermal_Conductivity + Harness_Thermal_Conductivity + Economizer_Heat_Transfer_Coefficient + Regenerator_Heat_Transfer_Coefficient; // Общие теплопатери (Сюда еще добавить теплопатери котла и возможно насоса)
                        Regenerator_Resistance = Convert.ToDouble(row.Cells["Resistance"].Value);

                        break;

                    case ("Gas_Purifier_Table"):

                        label29.Text = Convert.ToString(row.Cells[1].Value);
                        listBox1.Items[25] = "";
                        listBox1.Items[26] = "Регенератор: ";
                        listBox1.Items[27] = "Тип: " + Convert.ToString(row.Cells["Type"].Value);
                        listBox1.Items[28] = "Эффективность очистки: " + Convert.ToString(row.Cells["Cleaning_Efficiency"].Value) + " %";
                        listBox1.Items[29] = "Производительность: " + Convert.ToString(row.Cells["Productivity"].Value) + " (м3/сут)";                        
                        listBox1.Items[30] = "Аэродинамическое cопротивление: " + Convert.ToString(row.Cells["Resistance"].Value) + " МПа (мм.вод.ст.)";

                        Gas_Purifier_Resistance = Convert.ToDouble(row.Cells["Resistance"].Value);
                        Total_Resistance = Economizer_Resistance + Regenerator_Resistance + Gas_Purifier_Resistance; // Общее аэродинамическое сопротивление

                        break;

                    case ("Noise_Reduction_System_Table"):

                        label28.Text = Convert.ToString(row.Cells[1].Value);
                        listBox1.Items[31] = "";
                        listBox1.Items[32] = "Регенератор: ";
                        listBox1.Items[33] = "Тип: " + Convert.ToString(row.Cells["Type"].Value);
                        listBox1.Items[34] = "Уровень шумоподавления: " + Convert.ToString(row.Cells["Noise_Reduction_Level"].Value) + " Гц";
                        listBox1.Items[35] = "Тип материала: " + Convert.ToString(row.Cells["Material_Type"].Value);
                        listBox1.Items[36] = "Площадь: " + Convert.ToString(row.Cells["Area"].Value) + " м²";
                        listBox1.Items[37] = "Эффективность: " + Convert.ToString(row.Cells["Efficiency"].Value) + " м²";

                        break;
                }
            }
            // Вывод всех пощианных данных
            listBox1.Items[39] = "Общая производительность: " + Convert.ToString(Total_Thermal_Power);
            listBox1.Items[40] = "Общая теплопроводимость: " + Convert.ToString(Total_Thermal_Conductivity);
            listBox1.Items[41] = "Общая аэродинамическое сопротивление: " + Convert.ToString(Total_Resistance);
        }

        private void button7_Click(object sender, EventArgs e)
        {
            //string cs = @"Data Source = (LocalDB)\MSSQLLocalDB; AttachDbFilename = " + textBox9.Text + @"\Lab_7_MagDiser с БД (передел.)\Lab_7_MagDiser\TestDB.mdf; Integrated Security = True";
            string cs = @"Data Source = (LocalDB)\MSSQLLocalDB; AttachDbFilename = " + textBox9.Text + @"\TestDB.mdf; Integrated Security = True";
            
            sqlConnection = new SqlConnection(cs);

            sqlConnection.Open();
            if (sqlConnection.State == ConnectionState.Open)
            {
                MessageBox.Show("База данных подключена");
            }            
        }

        string comboBox1_Text;
        string Auto_Table_Name;
        int Id_T;

        private void button8_Click(object sender, EventArgs e) // Линейная оптимизация
        {
            switch (comboBox1_Text)
            {
                case "Самый легкий":                       
                    
                    ProcessCellClick("Frame_Table", "Weight", "MIN");

                    break;

                case "Самые малые габариты":

                    ProcessCellClick("Frame_Table", "Length * Height * Height", "MIN");

                    break;

                case "Наибольшая производительность":

                    ProcessCellClick("Pump_Table", "Performance", "MAX");

                    break;

                case "Наибольшее максимальное давление котла":

                    ProcessCellClick("Boiler_Table", "Max_Overpressure", "MAX");

                    break;

                default:
                    break;
            }           
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBox1_Text = comboBox1.Text;
        }

        // private void ProcessCellClick(int rowIndex, int columnIndex)

        // Метод для линейной оптимизации

        private void ProcessCellClick(string Table_Name, string Check_Value, string MIN_MAX)
        {
            string query = "SELECT * FROM "+ Table_Name + " WHERE "+ Check_Value + " = (SELECT "+ MIN_MAX + "("+ Check_Value + ") FROM "+ Table_Name + ")";
            
            using (SqlCommand command = new SqlCommand(query, sqlConnection))
            {
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        if (comboBox1_Text == "Самый легкий" || comboBox1_Text == "Самые малые габариты")
                        {
                            label14.Text = reader.GetString(1);
                            textBox7.Text = (reader.GetDouble(2) * reader.GetDouble(3) * reader.GetDouble(4)).ToString();
                            textBox10.Text = reader.GetDouble(5).ToString();
                        }
                        if (comboBox1_Text == "Наибольшая производительность")
                        {
                            label17.Text = reader.GetString(1);
                            textBox5.Text = reader.GetDouble(2).ToString();
                            textBox4.Text = reader.GetDouble(3).ToString();
                            textBox2.Text = reader.GetDouble(4).ToString();
                            textBox6.Text = reader.GetDouble(5).ToString();
                        }
                        if (comboBox1_Text == "Наибольшее максимальное давление котла")
                        {
                            label15.Text = reader.GetString(1);
                            
                            textBox1.Text = reader.GetDouble(5).ToString(); // Тепловая мощность, Вт
                            textBox2.Text = reader.GetDouble(4).ToString();
                            textBox3.Text = reader.GetDouble(2).ToString();
                        }                       
                    }                    
                }
            }
        }

        // Выбор ТГУ по производительности и цене
        private void SelectionByPerformanceAndPrice(double Performance, double minPrice, double maxPrice)
        {
            string connectionString = @"Data Source = (LocalDB)\MSSQLLocalDB; AttachDbFilename = " + textBox9.Text + @"\Lab_7_MagDiser с БД (передел.)\Lab_7_MagDiser\TestDB.mdf; Integrated Security = True"; ;            

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                
                string summPrice = "b.Price + p.Price + f.Price + c.Price + e.Price + g.Price + h.Price + r.Price + n.Price";

                string query = "SELECT b.Name AS BoilerName, p.Name AS PumpName, f.Name AS FrameName, "+
                               "c.Name AS ChimneyName, e.Name AS EconomizerName, g.Name AS Gas_PurifierName, " +
                               "h.Name AS HarnessName, r.Name AS RegeneratorName, n.Name AS Noise_Reduction_SystemName," +

                               "b.Thermal_Power AS BoilerPerformance, b.Price AS BoilerPrice, " +
                               "p.Performance AS PumpPerformance, p.Price AS PumpPrice, " +
                               "f.Price AS FramePrice, " +
                               "c.Price AS ChimneyPrice, " +
                               "e.Price AS EconomizerPrice, " +
                               "g.Price AS Gas_PurifierPrice, " +
                               "h.Price AS HarnessPrice, " +
                               "r.Price AS RegeneratorPrice, " +
                               "n.Price AS Noise_Reduction_SystemPrice " +
                               "FROM Boiler_Table b " +
                               $"JOIN Pump_Table p ON b.Thermal_Power + p.Performance >= {Performance} " +
                               $"JOIN Frame_Table f ON 1=1 " +
                               $"JOIN Chimney_Table c ON 1=1 " +
                               $"JOIN Economizer_Table e ON 1=1 " +
                               $"JOIN  Gas_Purifier_Table g ON 1=1 " +
                               $"JOIN  Harness_Table h ON 1=1 " +
                               $"JOIN  Regenerator_Table r ON 1=1 " +
                               $"JOIN  Noise_Reduction_System_Table n ON 1=1 " +
                               $"WHERE {summPrice} >= {minPrice} AND {summPrice} <= {maxPrice}";

                SqlCommand command = new SqlCommand(query, connection);
                
                SqlDataReader reader = command.ExecuteReader();

                string resultString = ""; // Создаем пустую строку для сбора результата
                int InstallationNumber = 1;
                ArrayList resultArrayList = new ArrayList();
                resultArrayList.Clear();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        string boilerName = reader["BoilerName"].ToString() + " Производительность: " + reader["BoilerPerformance"].ToString() + " Цена: " + reader["BoilerPrice"].ToString();                       
                        string pumpName = reader["PumpName"].ToString() + " Производительность: " + reader["PumpPerformance"].ToString() + " Цена: " + reader["PumpPrice"].ToString();
                        string frameName = reader["FrameName"].ToString() + " Цена: " + reader["FramePrice"].ToString();
                        string chimneyName = reader["ChimneyName"].ToString() + " Цена: " + reader["ChimneyPrice"].ToString();
                        string economizerName = reader["EconomizerName"].ToString() + " Цена: " + reader["EconomizerPrice"].ToString();
                        string Gas_PurifierName = reader["Gas_PurifierName"].ToString() + " Цена: " + reader["Gas_PurifierPrice"].ToString();
                        string HarnessName = reader["HarnessName"].ToString() + " Цена: " + reader["HarnessPrice"].ToString();
                        string RegeneratorName = reader["RegeneratorName"].ToString() + " Цена: " + reader["RegeneratorPrice"].ToString();
                        string Noise_Reduction_SystemName = reader["Noise_Reduction_SystemName"].ToString() + " Цена: " + reader["Noise_Reduction_SystemPrice"].ToString();
                        /*
                        resultString += "Установка №" + InstallationNumber + "\n" +
                                        $"Котел: {boilerName}\n" + // Добавляем данные каждой строки в строку результата
                                        $"Насос: {pumpName}\n" +
                                        $"Корпус: {frameName}\n" +
                                        $"//////Дымоход: {economizerName}\n" +
                                        //$"Экономайзер: {economizerName}\n" +
                                        $"Общая производительность: " + (Convert.ToDouble(reader["BoilerPerformance"]) + Convert.ToDouble(reader["PumpPerformance"])) + " кВт\n" +
                                        $"Общая цена: " + (Convert.ToDouble(reader["BoilerPrice"]) + Convert.ToDouble(reader["PumpPrice"]) +
                                        Convert.ToDouble(reader["FramePrice"]) + Convert.ToDouble(reader["ChimneyPrice"]) + 
                                        Convert.ToDouble(reader["EconomizerPrice"])) + " руб.\n\n"; 
                            */
                        resultArrayList.Add("Установка №" + InstallationNumber);
                        resultArrayList.Add($"Котел: {boilerName}");
                        resultArrayList.Add($"Насос: { pumpName}");
                        resultArrayList.Add($"Корпус: {frameName}");
                        resultArrayList.Add($"Дымоход: {chimneyName}");
                        resultArrayList.Add($"Экономайзер: {economizerName}");
                        resultArrayList.Add($"Газоочиститель: {Gas_PurifierName}");
                        resultArrayList.Add($"Обвязка: {HarnessName}");
                        resultArrayList.Add($"Регенератор: {RegeneratorName}");
                        resultArrayList.Add($"Система шумоподавления: {Noise_Reduction_SystemName}");
                        resultArrayList.Add($"Общая производительность: " + (Convert.ToDouble(reader["BoilerPerformance"]) + Convert.ToDouble(reader["PumpPerformance"])) + " кВт");
                        resultArrayList.Add($"Общая цена: " + (Convert.ToDouble(reader["BoilerPrice"]) + Convert.ToDouble(reader["PumpPrice"]) +
                                        Convert.ToDouble(reader["FramePrice"]) + Convert.ToDouble(reader["ChimneyPrice"]) +
                                        Convert.ToDouble(reader["EconomizerPrice"]) + Convert.ToDouble(reader["Gas_PurifierPrice"]) +
                                        Convert.ToDouble(reader["HarnessPrice"]) + Convert.ToDouble(reader["RegeneratorPrice"]) +
                                        Convert.ToDouble(reader["Noise_Reduction_SystemPrice"])) +
                                        " руб.");
                        resultArrayList.Add(" ");


                        InstallationNumber++;
                    }
                    // Добавляем элементы из ArrayList в ListBox
                    listBox1.Items.Clear();
                    foreach (var item in resultArrayList)
                    {
                        listBox1.Items.Add(item);
                    }
                }                
                else
                {
                    resultString = "Ничего не найдено";
                }
                //label38.Text = resultString;
                reader.Close();
                
            }
        }


        // расчест всех характеристик деталей в одиу теплогенерирующие установку (возможно что-то другое придумать)

        private void CalculateTotal(string Table_Name, string Check_Value, string MIN_MAX)
        {
            /*
            /*
            string query = "SELECT ваша_колонка FROM ваша_таблица WHERE условие";
            SqlCommand command = new SqlCommand(query, sqlConnection);
            string result = command.ExecuteScalar().ToString();
            

            DataGridViewRow row = dataGridView1.Rows[selectedRow];
            double totalEfficiency = Convert.ToDouble(row.Cells["Max_UP_Pressure"].Value) + Convert.ToDouble(row.Cells["Max_UP_Pressure"].Value);


            double totalEfficiency = Convert.ToDouble(textBox3.Text) * (1 - Convert.ToDouble(textBox11.Text));

            double TotalPaver = Boiler.Power + Pump.Power; // Мощность котла =  мощность котла + мощность насоса
            //double totalEfficiency = Boiler.Efficiency * (1 - Casing.HeatLossCoefficient); // КПД = КПД котла * (1 - тепловые потери корпуса)
            double CalculateTotalFuelConsumption = Boiler.CalculateFuelConsumption(heatOutput) + Pump.CalculatePowerConsumption(Casing.Volume); // Общий расход топлива = Котел Расчет расхода топлива (тепловая мощность) + Насос Расчет энергопотребления (объем корпуса)
            double Total_Heat_Loss = CalculateHeatLoss(temperatureDifference);
            double Total_Noise_Level = CalculateTotalNoiseLevel();

            double CalculateFuelConsumption(double heatOutput) // Расчет расхода топлива котла
            {
                return heatOutput / (Power * Efficiency); // тепловая мощность / (Мощность * КПД)
            }

            double CalculatePowerConsumption(double heatOutput) // Рассчитать энергопотребление насоса
            {
                return FlowRate * waterPressure / Power; // Расход * Давление воды / Мощность
            }

            double CalculateHeatLoss(double temperatureDifference) // Рассчитать теплопотери | temperatureDifference = Разница температур
            {
                return casing.HeatLossCoefficient * casing.Volume * temperatureDifference; // Коэффициент тепловых потерь корпуса * Объем корпуса * Разница температур
            }

            double CalculateTotalNoiseLevel() // Рассчитать общий уровень шума
            {
                // Реализация зависит от конкретных характеристик компонентов
                double boilerNoise = boiler.NoiseLevel;
                double pumpNoise = pump.NoiseLevel;
                // ... (уровень шума других компонентов, если применимо)

                // Суммирование или усреднение уровней шума компонентов
                return boilerNoise + pumpNoise; // (Пример простого суммирования)
            }
            */
        }

        private void button15_Click(object sender, EventArgs e)
        {
            SelectionByPerformanceAndPrice(Convert.ToDouble(textBox12.Text), Convert.ToDouble(textBox13.Text), Convert.ToDouble(textBox14.Text));
        }
    }
}
