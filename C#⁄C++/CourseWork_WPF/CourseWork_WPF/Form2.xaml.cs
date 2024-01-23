using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace CourseWork_WPF
{
    /// <summary>
    /// Логика взаимодействия для Form2.xaml
    /// </summary>
    public partial class Form2 : Window
    {
        public Form2()
        {
            InitializeComponent();
            CreateInfoInComboBox();
/*            comboBox1.Items.Add("1");
            comboBox1.Items.Add("2");*/
        }
        private string LocalX;
        private string LocalY;
        private void button1_Click(object sender, EventArgs e)
        {
            //CreatInfo createinfo = new CreatInfo();
            if (textBox1.Text == "")
            {
                MessageBox.Show("Поле \"Название\" обязательно для заполнения ", "Ошибка", MessageBoxButton.OK);
            }
            else
            {
                if (textBox2.Text == "")
                {

                    MessageBoxResult result = MessageBox.Show(
                            "Вы точно хотите оставить описание пустым?",
                            "Сообщение",
                            MessageBoxButton.YesNo
                            );
                    if (result == MessageBoxResult.Yes)
                    {
                        Close();
                    }
                }
                else
                {
                    //Сделать добовление всей инфы в БД
                    Close();
                }
            }
        }

        void CreateInfoInComboBox()
        {
            //Сделать добовление оборудование которое будет храниться в БД
            try
            {
                connectBD con = new connectBD();
                string sqlQuery = $"select Name from Router";
                string sqlQuery1 = $"select Model from Router";

                using (SqlCommand command = new SqlCommand(sqlQuery, con.GetConnections()))
                {
                    using (SqlCommand command1 = new SqlCommand(sqlQuery1, con.GetConnections()))
                    {
                        con.OpenConnections();
                        SqlDataReader reader = command.ExecuteReader();
                        while (reader.Read())
                        {
                            int buin = 0;
                            /*comboBox1.Items.AddRange(new string[] { reader[buin].ToString() });*/
                            comboBox1.Items.Add(reader[buin].ToString());
                            buin++;
                        }
                        reader.Close();
                        SqlDataReader reader1 = command1.ExecuteReader();
                        while (reader1.Read())
                        {

                            int buin = 0;
                            string bufstr = comboBox1.Items[buin].ToString();
                            comboBox1.Items.RemoveAt(buin);
                            comboBox1.Items.Add(bufstr + " " + reader1[buin].ToString());
                            buin++;
                        }
                        reader1.Close();
                    }
                }
                con.CloseConnections();
            }
            catch
            {
                MessageBox.Show("Не удалось подключиться к базе данных", "Ошибка", MessageBoxButton.OK);
            }
        }
        public string DataName
        {
            get
            {
                return textBox1.Text;
            }
        }
        public string DataDescript
        {
            get
            {
                return textBox2.Text;
            }
        }
        public string DataModel
        {
            get
            {
                return comboBox1.Text;
            }
        }
        public string SetLocalX
        {
            set
            {
                LocalX = (string)(label6.Content = value);
            }
        }
        public string SetLocalY
        {
            set
            {
                LocalY = (string)(label7.Content = value);
            }
        }
    }
}
