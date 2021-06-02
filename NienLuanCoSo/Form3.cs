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
    public partial class Form3 : Form
    {

        public string point;
        public Form3()
        {
             InitializeComponent();
        }
        public Form3(string newPoint)
        {
            InitializeComponent();
            point = newPoint;
            foreach (Control c in this.Controls)
            {
                if (c is TextBox && c.Name == "Diem")
                {
                    c.Text = point;
                    c.Enabled = false;
                }

            }

            InitializeComponent();
        }

        private void restart(object sender, EventArgs e)
        {
            var from1 = new Form1();
            from1.Show();
            this.Hide();
        }

      
    }
}
