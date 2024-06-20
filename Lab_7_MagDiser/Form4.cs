using System;
using System.Collections.Generic;
using System.ComponentModel;
//using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
// для работы с БД
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace Lab_7_MagDiser
{
    public partial class Form4 : Form
    {        
        private SqlConnection sqlConnection = null;

        int selectedRow;
        int IdTable;
        //int TableSelection;
        String TableName = null;
        public Form4()
        {
            Form1 form1 = new Form1();
            InitializeComponent();
            //sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["TestDB"].ConnectionString);
            string cs = @"Data Source = (LocalDB)\MSSQLLocalDB; AttachDbFilename = "+ form1.textBox9.Text + @"\Lab_7_MagDiser с БД (передел.)\Lab_7_MagDiser\TestDB.mdf; Integrated Security = True";
            sqlConnection = new SqlConnection(cs);
            sqlConnection.Open();
            StartAdmin();
            textBox1.Hide();
            textBox5.Hide();
        }
        
        private void ReadSingelRow(DataGridView dgv, IDataRecord record)
        {
            dgv.Rows.Add(record.GetInt32(0), record.GetString(1), record.GetFloat(2), record.GetFloat(3), record.GetFloat(4), record.GetFloat(5));
        }
        
        // Обновление таблицы
        private void UpdateTable(String TableName, DataGridView dataGridView)
        {
            SqlDataAdapter dataAdapter = new SqlDataAdapter("Select * FROM " + TableName, sqlConnection);
            DataSet dataSet = new DataSet();
            dataAdapter.Fill(dataSet);
            dataGridView.DataSource = dataSet.Tables[0];
        }

        // Поиск по таблицы
        private void SearchTable(DataGridView dgv)
        {
            String SqlText = null;
            if (TableName == "Frame_Table")
            {
                SqlText = "Select * FROM " + TableName + " WHERE CONCAT (id, Name, Length, Height, Width, Weight, Heat_Loss) LIKE N'%" + textBox11.Text + "%'";
            }
            if (TableName == "Boiler_Table")
            {
                SqlText = "Select * FROM " + TableName + " WHERE CONCAT (id, Name, Max_KPD, Heating_Temperature, Max_Overpressure, Thermal_Power, Height, Width, Depth, Weight) LIKE N'%" + textBox11.Text + "%'";
            }
            if (TableName == "Pump_Table")
            {
                SqlText = "Select * FROM " + TableName + " WHERE CONCAT (id, Name, Performance, Max_UP_Pressure, Max_Pressure, Operating_Voltage) LIKE N'%" + textBox11.Text + "%'";
            }
            if (TableName == "Assembled_Installations")
            {
                SqlText = "Select * FROM " + TableName + " WHERE CONCAT (id, Name, Frame, Boiler, Pump, Chimney_Table, Economizer_Table, Gas_Purifier_Table, Gas_Purifier_Table, Harness_Table, Noise_Reduction_System_Table, Regenerator_Table) LIKE N'%" + textBox11.Text + "%'";
            }
            if (TableName == "Chimney_Table")
            {
                SqlText = "Select * FROM " + TableName + " WHERE CONCAT (id, Name, Material, Diameter, Height, Thermal_Conductivity, Cleaning_System, Price) LIKE N'%" + textBox11.Text + "%'";
            }
            if (TableName == "Economizer_Table")
            {
                SqlText = "Select * FROM " + TableName + " WHERE CONCAT (id, Name, Type, Heat_Exchange_Area, Heat_Transfer_Coefficient, Material, Resistance, Price) LIKE N'%" + textBox11.Text + "%'";
            }
            if (TableName == "Gas_Purifier_Table")
            {
                SqlText = "Select * FROM " + TableName + " WHERE CONCAT (id, Name, Type, Cleaning_Efficiency, Productivity, Resistance, Price) LIKE N'%" + textBox11.Text + "%'";
            }
            if (TableName == "Harness_Table")
            {
                SqlText = "Select * FROM " + TableName + " WHERE CONCAT (id, Name, Material, Valves, Thermal_Conductivity, Price) LIKE N'%" + textBox11.Text + "%'";
            }
            if (TableName == "Noise_Reduction_System_Table")
            {
                SqlText = "Select * FROM " + TableName + " WHERE CONCAT (id, Name, Type, Noise_Reduction_Level, Material_Type, Area, Efficiency, Price) LIKE N'%" + textBox11.Text + "%'";
            }
            if (TableName == "Regenerator_Table")
            {
                SqlText = "Select * FROM " + TableName + " WHERE CONCAT (id, Name, Type, Heat_Exchange_Area, Heat_Transfer_Coefficient, Material, Resistance, Price) LIKE N'%" + textBox11.Text + "%'";
            }
            SqlDataAdapter dataAdapter = new SqlDataAdapter(SqlText, sqlConnection);
            DataSet dataSet = new DataSet();
            dataAdapter.Fill(dataSet);
            dgv.DataSource = dataSet.Tables[0];
        }       

        // Клик на ячейку БД
        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {           
            selectedRow = e.RowIndex;
            // Вывести название детали
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridView2.Rows[selectedRow];
                IdTable = Convert.ToInt32(row.Cells[0].Value);
                if (TableName == "Frame_Table" || TableName == "Pump_Table")
                {
                    textBox6.Text = row.Cells[1].Value.ToString();                   
                    textBox7.Text = row.Cells[2].Value.ToString().Replace(',', '.');
                    textBox8.Text = row.Cells[3].Value.ToString().Replace(',', '.');
                    textBox9.Text = row.Cells[4].Value.ToString().Replace(',', '.');
                    textBox10.Text = row.Cells[5].Value.ToString().Replace(',', '.');
                    textBox5.Text = row.Cells[6].Value.ToString().Replace(',', '.');
                }                              
                
                if (TableName == "Boiler_Table")
                {
                    textBox6.Text = row.Cells[1].Value.ToString();
                    textBox7.Text = row.Cells[2].Value.ToString().Replace(',', '.');
                    textBox8.Text = row.Cells[3].Value.ToString().Replace(',', '.');
                    textBox9.Text = row.Cells[4].Value.ToString().Replace(',', '.');
                    textBox10.Text = row.Cells[5].Value.ToString().Replace(',', '.');
                    textBox5.Text = row.Cells[6].Value.ToString().Replace(',', '.');
                    textBox4.Text = row.Cells[7].Value.ToString().Replace(',', '.');
                    textBox3.Text = row.Cells[8].Value.ToString().Replace(',', '.');
                    textBox2.Text = row.Cells[9].Value.ToString().Replace(',', '.');                    
                }
                if (TableName == "Economizer_Table")
                {
                    textBox6.Text = row.Cells[1].Value.ToString();
                    textBox7.Text = row.Cells[2].Value.ToString().Replace(',', '.');
                    textBox8.Text = row.Cells[3].Value.ToString().Replace(',', '.');
                    textBox9.Text = row.Cells[4].Value.ToString().Replace(',', '.');
                    textBox10.Text = row.Cells[5].Value.ToString().Replace(',', '.');
                    textBox5.Text = row.Cells[6].Value.ToString().Replace(',', '.');
                    textBox4.Text = row.Cells[7].Value.ToString().Replace(',', '.');                    
                }
                if (TableName == "Chimney_Table")
                {
                    textBox6.Text = row.Cells[1].Value.ToString();
                    textBox7.Text = row.Cells[2].Value.ToString().Replace(',', '.');
                    textBox8.Text = row.Cells[3].Value.ToString().Replace(',', '.');
                    textBox9.Text = row.Cells[4].Value.ToString().Replace(',', '.');
                    textBox10.Text = row.Cells[5].Value.ToString().Replace(',', '.');
                    textBox5.Text = row.Cells[6].Value.ToString().Replace(',', '.');
                    textBox4.Text = row.Cells[7].Value.ToString().Replace(',', '.');
                }
                if (TableName == "Gas_Purifier_Table")
                {
                    textBox6.Text = row.Cells[1].Value.ToString();
                    textBox7.Text = row.Cells[2].Value.ToString().Replace(',', '.');
                    textBox8.Text = row.Cells[3].Value.ToString().Replace(',', '.');
                    textBox9.Text = row.Cells[4].Value.ToString().Replace(',', '.');
                    textBox10.Text = row.Cells[5].Value.ToString().Replace(',', '.');
                    textBox5.Text = row.Cells[6].Value.ToString().Replace(',', '.');                    
                }
                if (TableName == "Harness_Table")
                {
                    textBox6.Text = row.Cells[1].Value.ToString();
                    textBox7.Text = row.Cells[2].Value.ToString().Replace(',', '.');
                    textBox8.Text = row.Cells[3].Value.ToString().Replace(',', '.');
                    textBox9.Text = row.Cells[4].Value.ToString().Replace(',', '.');
                    textBox10.Text = row.Cells[5].Value.ToString().Replace(',', '.');                    
                }
                if (TableName == "Noise_Reduction_System_Table")
                {
                    textBox6.Text = row.Cells[1].Value.ToString();
                    textBox7.Text = row.Cells[2].Value.ToString().Replace(',', '.');
                    textBox8.Text = row.Cells[3].Value.ToString().Replace(',', '.');
                    textBox9.Text = row.Cells[4].Value.ToString().Replace(',', '.');
                    textBox10.Text = row.Cells[5].Value.ToString().Replace(',', '.');
                    textBox5.Text = row.Cells[6].Value.ToString().Replace(',', '.');
                    textBox4.Text = row.Cells[7].Value.ToString().Replace(',', '.');
                }
                if (TableName == "Regenerator_Table")
                {
                    textBox6.Text = row.Cells[1].Value.ToString();
                    textBox7.Text = row.Cells[2].Value.ToString().Replace(',', '.');
                    textBox8.Text = row.Cells[3].Value.ToString().Replace(',', '.');
                    textBox9.Text = row.Cells[4].Value.ToString().Replace(',', '.');
                    textBox10.Text = row.Cells[5].Value.ToString().Replace(',', '.');
                    textBox5.Text = row.Cells[6].Value.ToString().Replace(',', '.');
                    textBox4.Text = row.Cells[7].Value.ToString().Replace(',', '.');
                }
                if (TableName == "Assembled_Installations")
                {
                    textBox6.Text = row.Cells[1].Value.ToString();
                    textBox7.Text = row.Cells[2].Value.ToString().Replace(',', '.');
                    textBox8.Text = row.Cells[3].Value.ToString().Replace(',', '.');
                    textBox9.Text = row.Cells[4].Value.ToString().Replace(',', '.');
                    textBox10.Text = row.Cells[5].Value.ToString().Replace(',', '.');
                    textBox5.Text = row.Cells[6].Value.ToString().Replace(',', '.');
                    textBox4.Text = row.Cells[7].Value.ToString().Replace(',', '.');
                    textBox3.Text = row.Cells[8].Value.ToString().Replace(',', '.');
                    textBox2.Text = row.Cells[9].Value.ToString().Replace(',', '.');
                    textBox1.Text = row.Cells[10].Value.ToString().Replace(',', '.');
                }
            }
        }

        // Очистить все поля ввода
        private void button7_Click(object sender, EventArgs e)
        {
            textBox1.Text = null;
            textBox2.Text = null;
            textBox3.Text = null;
            textBox4.Text = null;
            textBox5.Text = null;
            textBox6.Text = null;
            textBox7.Text = null;
            textBox8.Text = null;
            textBox9.Text = null;
            textBox10.Text = null;
        }

        // Добавить запись
        private void button3_Click(object sender, EventArgs e)
        {
            if (textBox6.Text == "" || textBox7.Text == "" || textBox8.Text == "" || textBox9.Text == "" || textBox10.Text == "")
            {
                MessageBox.Show("Не все поля заполнены");
            }
            else
            {
                if (TableName == "Frame_Table")
                {
                    SqlCommand command = new SqlCommand("INSERT INTO [Frame_Table] (Name, Length, Height, Width, Weight, Heat_Loss) VALUES (@Name, @Length, @Height, @Width, @Weight, @Heat_Loss)", sqlConnection);
                    command.Parameters.AddWithValue("Name", textBox6.Text);
                    command.Parameters.AddWithValue("Length", textBox7.Text);
                    command.Parameters.AddWithValue("Height", textBox8.Text);
                    command.Parameters.AddWithValue("Width", textBox9.Text);
                    command.Parameters.AddWithValue("Weight", textBox10.Text);
                    command.Parameters.AddWithValue("Heat_Loss", textBox11.Text);                    
                    command.ExecuteNonQuery();                    
                }
                if (TableName == "Boiler_Table")
                {
                    SqlCommand command = new SqlCommand("INSERT INTO [Boiler_Table] (Name, Max_KPD, Heating_Temperature, Max_Overpressure, Thermal_Power, Height, Width, Depth, Weight) VALUES (@Name, @Max_KPD, @Heating_Temperature, @Max_Overpressure, @Thermal_Power, @Height, @Width, @Depth, @Weight)", sqlConnection);
                    command.Parameters.AddWithValue("Name",textBox6.Text);
                    command.Parameters.AddWithValue("Max_KPD", textBox7.Text);
                    command.Parameters.AddWithValue("Heating_Temperature", textBox8.Text);
                    command.Parameters.AddWithValue("Max_Overpressure", textBox9.Text);
                    command.Parameters.AddWithValue("Thermal_Power", textBox10.Text);
                    command.Parameters.AddWithValue("Height", textBox5.Text);
                    command.Parameters.AddWithValue("Width", textBox4.Text);
                    command.Parameters.AddWithValue("Depth", textBox3.Text);
                    command.Parameters.AddWithValue("Weight", textBox2.Text);                    
                    command.ExecuteNonQuery();                          
                }
                if (TableName == "Pump_Table")
                {
                    SqlCommand command = new SqlCommand("INSERT INTO [Pump_Table] (Name, Performance, Max_UP_Pressure, Max_Pressure, Operating_Voltage) VALUES (@Name, @Performance, @Max_UP_Pressure, @Max_Pressure, @Operating_Voltage)", sqlConnection);
                    command.Parameters.AddWithValue("Name", textBox6.Text);
                    command.Parameters.AddWithValue("Performance", textBox7.Text);
                    command.Parameters.AddWithValue("Max_UP_Pressure", textBox8.Text);
                    command.Parameters.AddWithValue("Max_Pressure", textBox9.Text);
                    command.Parameters.AddWithValue("Operating_Voltage", textBox10.Text);
                    command.ExecuteNonQuery();
                }
                if (TableName == "Chimney_Table")
                {
                    SqlCommand command = new SqlCommand("INSERT INTO [Chimney_Table] (Name, Material, Diameter, Height, Thermal_Conductivity, Cleaning_System, Price) VALUES (@Name, @Material, @Diameter, @Height, @Thermal_Conductivity, @Cleaning_System, @Price)", sqlConnection);
                    command.Parameters.AddWithValue("Name", textBox6.Text);
                    command.Parameters.AddWithValue("Material", textBox7.Text);
                    command.Parameters.AddWithValue("Diameter", textBox8.Text);
                    command.Parameters.AddWithValue("Height", textBox9.Text);
                    command.Parameters.AddWithValue("Thermal_Conductivity", textBox10.Text);
                    command.Parameters.AddWithValue("Cleaning_System", textBox5.Text);
                    command.Parameters.AddWithValue("Price", textBox4.Text);                    
                    command.ExecuteNonQuery();
                }
                if (TableName == "Economizer_Table")
                {
                    SqlCommand command = new SqlCommand("INSERT INTO [Economizer_Table] (Name, Type, Heat_Exchange_Area, Heat_Transfer_Coefficient, Material, Resistance, Price) VALUES (@Name, @Type, @Heat_Exchange_Area, @Heat_Transfer_Coefficient, @Material, @Resistance, @Price)", sqlConnection);
                    command.Parameters.AddWithValue("Name", textBox6.Text);
                    command.Parameters.AddWithValue("Type", textBox7.Text);
                    command.Parameters.AddWithValue("Heat_Exchange_Area", textBox8.Text);
                    command.Parameters.AddWithValue("Heat_Transfer_Coefficient", textBox9.Text);
                    command.Parameters.AddWithValue("Material", textBox10.Text);
                    command.Parameters.AddWithValue("Resistance", textBox5.Text);
                    command.Parameters.AddWithValue("Price", textBox4.Text);
                    command.ExecuteNonQuery();
                }
                if (TableName == "Gas_Purifier_Table")
                {
                    SqlCommand command = new SqlCommand("INSERT INTO [Gas_Purifier_Table] (Name, Type, Heat_Exchange_Area, Heat_Transfer_Coefficient, Material, Resistance, Price) VALUES (@Name, @Type, @Heat_Exchange_Area, @Heat_Transfer_Coefficient, @Material, @Resistance, @Price)", sqlConnection);
                    command.Parameters.AddWithValue("Name", textBox6.Text);
                    command.Parameters.AddWithValue("Type", textBox7.Text);
                    command.Parameters.AddWithValue("Heat_Exchange_Area", textBox8.Text);
                    command.Parameters.AddWithValue("Heat_Transfer_Coefficient", textBox9.Text);
                    command.Parameters.AddWithValue("Material", textBox10.Text);
                    command.Parameters.AddWithValue("Resistance", textBox5.Text);
                    command.Parameters.AddWithValue("Price", textBox4.Text);
                    command.ExecuteNonQuery();
                }
                if (TableName == "Gas_Purifier_Table")
                {
                    SqlCommand command = new SqlCommand("INSERT INTO [Gas_Purifier_Table] (Name, Type, Heat_Exchange_Area, Heat_Transfer_Coefficient, Material, Resistance, Price) VALUES (@Name, @Type, @Heat_Exchange_Area, @Heat_Transfer_Coefficient, @Material, @Resistance, @Price)", sqlConnection);
                    command.Parameters.AddWithValue("Name", textBox6.Text);
                    command.Parameters.AddWithValue("Type", textBox7.Text);
                    command.Parameters.AddWithValue("Heat_Exchange_Area", textBox8.Text);
                    command.Parameters.AddWithValue("Heat_Transfer_Coefficient", textBox9.Text);
                    command.Parameters.AddWithValue("Material", textBox10.Text);
                    command.Parameters.AddWithValue("Resistance", textBox5.Text);
                    command.Parameters.AddWithValue("Price", textBox4.Text);
                    command.ExecuteNonQuery();
                }
                if (TableName == "Harness_Table")
                {
                    SqlCommand command = new SqlCommand("INSERT INTO [Harness_Table] (Name, Material, Valves, Thermal_Conductivity, Price) VALUES (@Name, @Material, @Valves, @Thermal_Conductivity, @Price)", sqlConnection);
                    command.Parameters.AddWithValue("Name", textBox6.Text);
                    command.Parameters.AddWithValue("Material", textBox7.Text);
                    command.Parameters.AddWithValue("Valves", textBox8.Text);
                    command.Parameters.AddWithValue("Thermal_Conductivity", textBox9.Text);
                    command.Parameters.AddWithValue("Price", textBox10.Text);                    
                    command.ExecuteNonQuery();
                }
                if (TableName == "Regenerator_Table")
                {
                    SqlCommand command = new SqlCommand("INSERT INTO [Regenerator_Table] (Name, Type, Heat_Exchange_Area, Heat_Transfer_Coefficient, Material, Resistance, Price) VALUES (@Name, @Type, @Heat_Exchange_Area, @Heat_Transfer_Coefficient, @Material, @Resistance, @Price)", sqlConnection);
                    command.Parameters.AddWithValue("Name", textBox6.Text);
                    command.Parameters.AddWithValue("Type", textBox7.Text);
                    command.Parameters.AddWithValue("Heat_Exchange_Area", textBox8.Text);
                    command.Parameters.AddWithValue("Heat_Transfer_Coefficient", textBox9.Text);
                    command.Parameters.AddWithValue("Material", textBox10.Text);
                    command.Parameters.AddWithValue("Resistance", textBox5.Text);
                    command.Parameters.AddWithValue("Price", textBox4.Text);
                    command.ExecuteNonQuery();
                }

                UpdateTable(TableName, dataGridView2);
                MessageBox.Show("Элемент добавлен");
            }           

        }

        // Поиск по таблице
        private void textBox11_TextChanged(object sender, EventArgs e)
        {                        
            SearchTable(dataGridView2);            
        }

        // Удаление записи
        private void button4_Click(object sender, EventArgs e)
        {                
            SqlCommand command = new SqlCommand("DELETE " + TableName + " WHERE id = " + IdTable, sqlConnection);
            command.ExecuteNonQuery();
            UpdateTable(TableName, dataGridView2);
            MessageBox.Show("Элемент удален");
        }

        // Изменить запись
        private void button5_Click(object sender, EventArgs e)
        {
            if (IdTable <= 0)
            {
                MessageBox.Show("На выбрана запись для изменения");
            }
            else
            {
                if (textBox6.Text == "" || textBox7.Text == "" || textBox8.Text == "" || textBox9.Text == "" || textBox10.Text == "")
                {
                    MessageBox.Show("Не все поля заполнены");
                }
                else
                {
                    if (TableName == "Frame_Table")
                    {
                        SqlCommand command = new SqlCommand("UPDATE [Frame_Table] SET Name = N'" + textBox6.Text + "', Length = " + textBox7.Text + ", Height = " + textBox8.Text + ", Width = " +textBox9.Text + ", Weight = " + textBox10.Text + " WHERE [id] = " + IdTable, sqlConnection);
                        command.ExecuteNonQuery();
                    }
                    if (TableName == "Boiler_Table")
                    {
                        SqlCommand command = new SqlCommand("UPDATE [Boiler_Table] SET Name = N'" + textBox6.Text + "', Max_KPD = " + textBox7.Text + ", Heating_Temperature = " + textBox8.Text + ", Max_Overpressure = " + textBox9.Text + ", Thermal_Power = " + textBox10.Text + ", Height = " + textBox5.Text + ", Width = " + textBox4.Text + ", Depth = " + textBox3.Text + ", Weight = " + textBox2.Text + " WHERE [id] = " + IdTable, sqlConnection);
                        command.ExecuteNonQuery();
                    }
                    if (TableName == "Pump_Table")
                    {
                        SqlCommand command = new SqlCommand("UPDATE [Pump_Table] SET Name = N'" + textBox6.Text + "', Performance = " + textBox7.Text + ", Max_UP_Pressure = " + textBox8.Text + ", Max_Pressure = " + textBox9.Text + ", Operating_Voltage = " + textBox10.Text + " WHERE [id] = " + IdTable, sqlConnection);
                        command.ExecuteNonQuery();
                    }
                    // новые таблицы
                    if (TableName == "Chimney_Table")
                    {
                        SqlCommand command = new SqlCommand("UPDATE [Chimney_Table] SET Name = N'" + textBox6.Text + "', Material = " + textBox7.Text + ", Diameter = " + textBox8.Text + ", Height = " + textBox9.Text + ", Thermal_Conductivity = " + textBox10.Text + ", Cleaning_System = " + textBox5.Text + ", Price = " + textBox4.Text + " WHERE [id] = " + IdTable, sqlConnection);
                        command.ExecuteNonQuery();
                    }
                    if (TableName == "Economizer_Table")
                    {
                        SqlCommand command = new SqlCommand($"UPDATE [Economizer_Table] SET Name = N'{textBox6.Text}', Type = {textBox7.Text}, Heat_Exchange_Area = {textBox8.Text}, Heat_Transfer_Coefficient = {textBox9.Text}, Material = {textBox10.Text}, Resistance = {textBox5.Text}, Price = {textBox4.Text} WHERE [id] = " + IdTable, sqlConnection);
                        command.ExecuteNonQuery();
                    }
                    if (TableName == "Gas_Purifier_Table")
                    {
                        SqlCommand command = new SqlCommand($"UPDATE [Gas_Purifier_Table] SET Name = N'{textBox6.Text}', Type = {textBox7.Text}, Cleaning_Efficiency = {textBox8.Text}, Productivity = {textBox9.Text}, Resistance = {textBox10.Text}, Price = {textBox5.Text} WHERE [id] = " + IdTable, sqlConnection);
                        command.ExecuteNonQuery();
                    }
                    if (TableName == "Harness_Table")
                    {
                        SqlCommand command = new SqlCommand($"UPDATE [Harness_Table] SET Name = N'{textBox6.Text}', Material = {textBox7.Text}, Valves = {textBox8.Text}, Thermal_Conductivity = {textBox9.Text}, Price = {textBox10.Text} WHERE [id] = " + IdTable, sqlConnection);
                        command.ExecuteNonQuery();
                    }
                    if (TableName == "Regenerator_Table")
                    {
                        SqlCommand command = new SqlCommand($"UPDATE [Regenerator_Table] SET Name = N'{textBox6.Text}', Type = {textBox7.Text}, Heat_Exchange_Area = {textBox8.Text}, Heat_Transfer_Coefficient = {textBox9.Text}, Material = {textBox10.Text}, Resistance = {textBox5.Text}, Price = {textBox4.Text} WHERE [id] = " + IdTable, sqlConnection);
                        command.ExecuteNonQuery();
                    }
                   

                    UpdateTable(TableName, dataGridView2);
                    MessageBox.Show("Запись изменена");
                }
            }                   
        }        

        // Выбор таблицы
        private void button1_Click(object sender, EventArgs e)
        {
            StartAdmin();
            textBox5.Hide();                      
        }       
        private void button2_Click(object sender, EventArgs e)
        {
            TableName = "Boiler_Table";
            UpdateTable("Boiler_Table", dataGridView2);
            label2.Text = "Макс. КПД, %";
            label3.Text = "Темп. обогрева, °С";
            label4.Text = "Макс. Давление, Па";
            label5.Text = "Тепл. мощность, Дж";
            label11.Text = "Высота, м";
            label10.Text = "Длина, м";
            label9.Text = "Глубина, м";
            label8.Text = "Вес, кг";
            textBox1.Show();
            textBox2.Show();
            textBox3.Show();
            textBox4.Show();
            textBox5.Show();
            label11.Show();
            label10.Show();
            label9.Show();
            label8.Show();
            label7.Show();
            label7.Hide();
            textBox1.Hide();
        }
        private void button6_Click(object sender, EventArgs e)
        {
            TableName = "Pump_Table";
            UpdateTable("Pump_Table", dataGridView2);
            label2.Text = "Производительность, кВт";
            label3.Text = "Макс. напор, м";
            label4.Text = "Макс. давление,Па";
            label5.Text = "Раб. напряжение, В";
            Hide_All();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            TableName = "Chimney_Table";
            UpdateTable("Chimney_Table", dataGridView2);
            label2.Text = "Материал";
            label3.Text = "Диаметр, м";
            label4.Text = "Высота, м";
            label5.Text = "Теплопровод., Вт/м·К";
            label11.Show();
            label10.Show();
            textBox4.Show();
            textBox5.Show();
            label11.Text = "Система отчист.";
            label10.Text = "Цена, руб.";
        }

        private void button10_Click(object sender, EventArgs e)
        {
            TableName = "Economizer_Table";
            UpdateTable("Economizer_Table", dataGridView2);
            label2.Text = "Тип";
            label3.Text = "Площадь, м²";
            label4.Text = "Коэф. теплопер, Вт/(м2°C)";
            label5.Text = "Материал";
            label11.Show();
            label10.Show();
            textBox4.Show();
            textBox5.Show();
            label11.Text = "Сопр., м²·K/Вт";
            label10.Text = "Цена, руб.";
        }

        private void button11_Click(object sender, EventArgs e)
        {
            TableName = "Gas_Purifier_Table";
            UpdateTable("Gas_Purifier_Table", dataGridView2);
            label2.Text = "Тип";
            label3.Text = "Эффективность, мг/л";
            label4.Text = "Производительн., м3/сут";
            label5.Text = "Сопротивление";
            label11.Show();
            label10.Hide();
            textBox4.Hide();
            textBox5.Show();
            label11.Text = "Цена, руб.";            
        }

        private void button12_Click(object sender, EventArgs e)
        {
            TableName = "Harness_Table";
            UpdateTable("Harness_Table", dataGridView2);
            label2.Text = "Материал";
            label3.Text = "Клапаны";
            label4.Text = "Теплопровод., Вт/м·К";
            label5.Text = "Цена, руб.";
            label11.Hide();
            label10.Hide();
            textBox4.Hide();
            textBox5.Hide();
            
        }

        private void button13_Click(object sender, EventArgs e)
        {
            TableName = "Regenerator_Table";
            UpdateTable("Regenerator_Table", dataGridView2);
            label2.Text = "Тип";
            label3.Text = "Площадь, м²";
            label4.Text = "Коэф. теплоперед, Вт/(м2°C)";
            label5.Text = "Материал";
            label11.Show();
            label10.Show();
            textBox4.Show();
            textBox5.Show();
            label11.Text = "Сопр., м²·K/Вт";
            label10.Text = "Цена, руб.";
        }

        private void Hide_All()
        {
            textBox1.Hide();
            textBox2.Hide();
            textBox3.Hide();
            textBox4.Hide();
            textBox5.Hide();
            label11.Hide();
            label10.Hide();
            label9.Hide();
            label8.Hide();
            label7.Hide();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            TableName = "Assembled_Installations";
            UpdateTable("Assembled_Installations", dataGridView2);           
        }

        private void StartAdmin()
        {
            TableName = "Frame_Table";
            UpdateTable("Frame_Table", dataGridView2);
            label2.Text = "Длина, м";
            label3.Text = "Высота, м";
            label4.Text = "Ширина, м";
            label10.Text = "Вес, кг";
            label5.Text = "Тепл. потери, кг";

            label11.Show();
            textBox5.Show();
            
            textBox2.Hide();
            textBox3.Hide();
            textBox4.Hide();            
            label11.Hide();
            label10.Hide();
            label9.Hide();
            label8.Hide();
            label7.Hide();
        }

        // Ввод только цифр
        private void EnteringNumbersOnly(KeyPressEventArgs e)
        {
            char ch = e.KeyChar;
            if (!Char.IsDigit(ch) && ch != 8 && ch != '.')
            {
                e.Handled = true;
            }
        }
        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            EnteringNumbersOnly(e);
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            EnteringNumbersOnly(e);
        }

        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            EnteringNumbersOnly(e);
        }

        private void textBox4_KeyPress(object sender, KeyPressEventArgs e)
        {
            EnteringNumbersOnly(e);
        }

        private void textBox5_KeyPress(object sender, KeyPressEventArgs e)
        {
            EnteringNumbersOnly(e);
        }
        private void textBox7_KeyPress(object sender, KeyPressEventArgs e)
        {
            EnteringNumbersOnly(e);
        }

        private void textBox8_KeyPress(object sender, KeyPressEventArgs e)
        {
            EnteringNumbersOnly(e);
        }

        private void textBox9_KeyPress(object sender, KeyPressEventArgs e)
        {
            EnteringNumbersOnly(e);
        }

        private void textBox10_KeyPress(object sender, KeyPressEventArgs e)
        {
            EnteringNumbersOnly(e);
        }

        
    }
}
