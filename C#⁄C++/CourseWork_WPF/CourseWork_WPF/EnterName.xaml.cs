using System;
using System.Collections.Generic;
using System.Linq;
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
    /// Логика взаимодействия для EnterName.xaml
    /// </summary>
    public partial class EnterName : Window
    {
        public EnterName()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            if (textBox1.Text == "")
            {
                MessageBox.Show("Поле \"Название\" обязательно для заполнения ", "Ошибка", MessageBoxButton.OK);
            }
            else
            {
                Close();
            }
        }
        public string DataName
        {
            get
            {
                return textBox1.Text;
            }
        }
    }
}
