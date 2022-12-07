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
    public partial class EditServiceForm : Form
    {
        public EditServiceForm()
        {
            InitializeComponent();
        }

        private void EditServiceForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ServiceForm From = new ServiceForm();
            this.Visible = false;
            From.ShowDialog();
        }
    }
}
