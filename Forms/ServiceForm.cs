using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
namespace _Леарн_.Forms
{
    using SQLCLass = Modules.SQLClass;
    public partial class ServiceForm : Form
    {
        public static bool ch = true;
  
        public ServiceForm()
        {
            InitializeComponent();
            if (AutorizationForm.UsersInfo[3] == "admin") {
                button3.Visible = true;
                button2.Visible = true;

                button1.Visible = true;

            }

            comboBox2.Items.Add("Без фильтра");
            comboBox2.Items.Add("от 0 до 5%");
            comboBox2.Items.Add("от 5% до 15%");
            comboBox2.Items.Add("от 15% до 30%");
            comboBox2.Items.Add("от 30% до 70%");
            comboBox2.Items.Add("от 70% до 100%");

            comboBox2.SelectedIndex = 0;
            comboBox1.Items.Add("по возрастанию");
            comboBox1.Items.Add("по убыванию");
            comboBox1.SelectedIndex = 0;

            SQLCLass.GetServise(dataGridView1, comboBox2.Text, comboBox1.Text, textBox1.Text);
            

        }

        private void ServiceForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            EditServiceForm From = new EditServiceForm();
            this.Visible = false;
            From.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            OrderForm From = new OrderForm();
            this.Visible = false;
            From.ShowDialog();
        }

        public  void Pictures() {
            string cmd = "SELECT * FROM newschema.Сервис;";
            MySqlCommand Command = new MySqlCommand(cmd, SQLCLass.Connect());
            MySqlDataReader Reader = Command.ExecuteReader();
            
                var imageColumn = new DataGridViewImageColumn();
                imageColumn.ImageLayout = DataGridViewImageCellLayout.Zoom;
                dataGridView1.Columns.Add(imageColumn);
           

            int i = 0;
            while (Reader.Read())
            {

                var path = AppDomain.CurrentDomain.BaseDirectory;
                if (Reader[6].ToString() != "")
                {
                    path += "Resurse\\" + Reader[6].ToString().TrimStart();
                }
                else
                {
                    path += "\\essons.jpg";
                }
                //path += "\\Resurse\\Услуги школы\\Китайский язык.jpg";
                Image img = Image.FromFile(path);
                dataGridView1.Rows[i].Cells[dataGridView1.ColumnCount - 1].Value = img;
                i++;

            }
            Command.Connection.Close();
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            
            SQLCLass.GetServise(dataGridView1, comboBox2.Text, comboBox1.Text, textBox1.Text);
            
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            SQLCLass.GetServise(dataGridView1, comboBox2.Text, comboBox1.Text, textBox1.Text);
           
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            SQLCLass.GetServise(dataGridView1, comboBox2.Text, comboBox1.Text, textBox1.Text);
           
        }

        private void ServiceForm_Load(object sender, EventArgs e)
        {
            Pictures();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            AddServiceForm From = new AddServiceForm();
            this.Visible = false;
            From.ShowDialog();
        }

       
    }
}
