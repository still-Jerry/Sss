using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _Леарн_.Forms
{
    using SQLCLass = Modules.SQLClass;
    public partial class AutorizationForm : Form
    {
        public static List<string> UsersInfo = new List<string>();

        public AutorizationForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "" || textBox2.Text == "") {
                MessageBox.Show("Заполните пустые поля", "Информация");
            }
            else if (SQLCLass.GetUser(textBox1.Text, textBox2.Text, UsersInfo)) {
                ServiceForm From = new ServiceForm();
                this.Visible = false;
                From.ShowDialog();
            }
           
        }

        private void AutorizationForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }
    }
}
