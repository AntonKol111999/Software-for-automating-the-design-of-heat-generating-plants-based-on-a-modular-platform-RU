using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab_7_MagDiser
{
    public partial class Form3 : Form
    {        
        public Form3()
        {
            InitializeComponent();

            comboBox1.Items.AddRange(new string[] { "Добавить деталь", "Редактировать деталь", "Удалить деталь" });
            comboBox2.Items.AddRange(new string[] { "Каркас", "Котел", "Дитчик давления", "Датчик температуры", "Клапан", "Насос" });
            
        
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form1 form1 = new Form1();
            form1.Show();
        }
    }
}
