using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NienLuanCoSo
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
            GetNewHighScore();
        }
        public void GetNewHighScore()
        {
            int counter = 0;
            string line;
            // Add this textbox to form
            string FilePath = @"C:\Users\User\OneDrive\Desktop\NienLuan\NienLuan\NienLuanCoSo\bin\Debug\HighScoce.txt";
            System.IO.StreamReader file = new System.IO.StreamReader(@"C:\Users\User\OneDrive\Desktop\NienLuan\NienLuan\NienLuanCoSo\bin\Debug\HighScoce.txt");
            List<string> Lines = new List<string>();
            int count = 0;
            while ((line = file.ReadLine()) != null)
            {
                Lines.Add(line);
            }
            foreach (string eachline in Lines)
            {
                TextBox textBox = new TextBox();
                textBox.Location = new Point(500, 33);
                textBox.Left = 33;
                textBox.Top = 173 + count * 50;
                textBox.Text = "Submit";
                textBox.AutoSize = true;
                //textBox.BackColor = Color.LightBlue;
                textBox.Padding = new Padding(6);
                textBox.Font = new Font("French Script MT", 18);
                textBox.Size = new System.Drawing.Size(173, 100);
                textBox.Enabled = false;
                this.Controls.Add(textBox);
                //Button Mybutton = new Button();
                //Mybutton.Location = new Point(500, 33);
                //Mybutton.Left = 33;
                //Mybutton.Top = 173 + count * 50;
                //Mybutton.Text = "Submit";
                //Mybutton.AutoSize = true;
                //Mybutton.BackColor = Color.LightBlue;
                //Mybutton.Padding = new Padding(6);
                //Mybutton.Font = new Font("French Script MT", 18);
                //this.Controls.Add(Mybutton);
                count++;
            }
            file.Close();
        }





    }
}
