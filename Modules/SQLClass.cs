using System;
using System.IO;
using System.Drawing;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Threading.Tasks;
using System.Windows.Forms;
namespace _Леарн_.Modules
{
    class SQLClass
    {
        public static MySqlConnection Connect() {
            try
            {
                string con = "host= localhost; uid=root; pwd= root; database = newschema";
                MySqlConnection Connecct = new MySqlConnection(con);
                Connecct.Open();
                return Connecct;
            }
            catch {
                MessageBox.Show("Ошибка подключения к бд", "Ошибка");
                return null;
            }
        }
        public static void GetServise(DataGridView dgv, string filter, string orer, string text)
        {
            try
            {
                string cmd = "SELECT * FROM newschema.Сервис ";
                switch (filter) { 
                    case("от 0 до 5%"):
                        cmd += "where Скидка < 0.05";
                        break;
                    case ("от 5% до 15%"):
                        cmd += "where Скидка >= 0.05 and Скидка < 0.15";
                        break;
                    case ("от 15% до 30%"):
                        cmd += "where Скидка >= 0.15 and Скидка < 0.3";
                        break;
                    case ("от 30% до 70%"):
                        cmd += "where Скидка >= 0.3 and Скидка < 0.7";
                        break;
                    case ("от 70% до 100%"):
                        cmd += "where Скидка >= 0.7 and Скидка <= 1";
                        break;
                    default:
                        break;
                }
                if (filter != "Без фильтра")
                {
                    cmd += " and Наименование like '"+text+"%'";
                }
                else {
                    cmd += " where Наименование like '" + text + "%'";

                }
                if (orer == "по возрастанию")
                {
                    cmd += " order by Цена asc;";
                }
                else {
                    cmd += " order by Цена desc;";
                
                }
                MySqlCommand Command = new MySqlCommand(cmd, Connect());
                MySqlDataAdapter adapt = new MySqlDataAdapter(Command);
                DataTable dt = new DataTable();
                adapt.Fill(dt);
                dgv.DataSource = dt;
                Command.ExecuteNonQuery();
                Command.Connection.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка");
            }
        }
        public static void GetPicturesServise(DataGridView dataGridView1)
        {
            try
            {
                //DataGridViewImageColumn imdcol = new DataGridViewImageColumn();
                //string path1 = Directory.GetCurrentDirectory();
                //path1 += "\\Resurse\\Услуги школы\\for company.jpg";
                //imdcol.Image = Image.FromFile(path1);
                //imdcol.ImageLayout = DataGridViewImageCellLayout.Zoom;
                //dgv.Columns.Add(imdcol);
           

                //string cmd = "SELECT * FROM newschema.service;";
                //MySqlCommand Command = new MySqlCommand(cmd, Connect());
                //MySqlDataReader reader = Command.ExecuteReader();
                //int i =0 ;
                //while (reader.Read())
                //{
                //    string path = Directory.GetCurrentDirectory();
                //    //var path = AppDomain.CurrentDomain.BaseDirectory;
                //    path += "Resurse\\" + reader[6].ToString().TrimStart();
                //    dgv.Rows[i].Cells[dgv.ColumnCount - 1].Value = Image.FromFile(path);
                //    i++;
                //    //var path = AppDomain.CurrentDomain.BaseDirectory + "essons.jpg";
                //    ////if (reader[6].ToString() != "")
                //    ////{
                //    ////    path +=  reader[6].ToString();
                //    ////}
                //    ////else
                //    ////{
                //    ////    path += "Услуги школы\\for company.jpg";
                //    ////}
                //    //Image img = Image.FromFile(path);
                //    //dgv.Rows[i].Cells[dgv.ColumnCount  -1].Value = img;
                //    //i++;
                //}
                //Command.Connection.Close();
                string cmd = "SELECT * FROM newschema.Сервис;";
                MySqlCommand Command = new MySqlCommand(cmd, Connect());
                MySqlDataReader Reader = Command.ExecuteReader();
                //var imageColumn = new DataGridViewImageColumn();
                //imageColumn.ImageLayout = DataGridViewImageCellLayout.Zoom;
                //dataGridView1.Columns.Add(imageColumn);

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
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка");
            }
        }
        public static  bool GetUser(string log, string  pws,
                  List <string >UsersInfo )
        {
            try
            {
                UsersInfo.Clear();
                string cmd = "SELECT * FROM newschema.users where login = '" + log + "' and pwd = '" + pws + "';";
                MySqlCommand Command = new MySqlCommand(cmd, Connect());
                MySqlDataReader reader = Command.ExecuteReader();
                while (reader.Read())
                {
                    UsersInfo.Add(reader[0].ToString());
                    UsersInfo.Add(reader[1].ToString());
                    UsersInfo.Add(reader[2].ToString());
                    UsersInfo.Add(reader[3].ToString());
                }
                Command.Connection.Close();
                if (UsersInfo.Count < 1)
                {
                    MessageBox.Show("Неверный логин или пароль", "Информация");

                    return false;
                }

                else
                {
                    MessageBox.Show("Статус: " + UsersInfo[3], "Информация");

                    return true;
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка");
                return false;
            }
        }
    }
}
