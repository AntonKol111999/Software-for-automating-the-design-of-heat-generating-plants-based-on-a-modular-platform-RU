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
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form1 form1 = new Form1();
            form1.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            double square = Convert.ToDouble(textBox1.Text);
            double wallheight = Convert.ToDouble(textBox2.Text);            
            int windows = Convert.ToInt32(textBox3.Text);
            int doors = Convert.ToInt32(textBox4.Text);
            double coefficient1 = Convert.ToDouble(textBox5.Text);
            double coefficient2 = Convert.ToDouble(textBox6.Text);
            double power = powerCal(square, wallheight, windows, doors, coefficient1, coefficient2);
            textBox7.Text = "" + power;
        }
        public double powerCal(double square, double wallheight, int windows, int doors, double coefficient1, double coefficient2)
        {
            double power = (square * wallheight * 40 + (windows * 100) + (doors * 200)) * coefficient2 * coefficient1;
            power = power / 1000;
            return power;
        }
      
    }
}
