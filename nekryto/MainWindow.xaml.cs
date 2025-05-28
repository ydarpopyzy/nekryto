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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace nekryto
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        user36015Entities db = new user36015Entities();
        public MainWindow()
        {

            InitializeComponent();
        }

        private void Login_Click(object sender, RoutedEventArgs e)
        {

            var enter = db.Users.FirstOrDefault(x => x.UserName == UsernameTextBox.Text && x.Password == PasswordBox.Password);
            try
            {
                if (enter == null)
                {
                    MessageBox.Show("Такого пользователя нет!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else
                {
                    MessageBox.Show($"Добро пожаловать, {UsernameTextBox.Text}!", "Успешный вход", MessageBoxButton.OK, MessageBoxImage.Information);
                    var productsWindow = new Products(UsernameTextBox.Text);
                    productsWindow.Show();
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}", "Попробуйте потом", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Guest_Click(object sender, RoutedEventArgs e)
        {
            var productsWindow = new Products(null);
            productsWindow.Show();
            this.Close();
        }
    }
}