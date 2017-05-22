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
using System.Security.Cryptography;
using lab1.db.models;

namespace lab1
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void buttonCancel_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Закрыть форму?", "Подтверждение", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                this.Close();
        }

        private void buttonOK_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var model = (MainWindowViewModel)this.DataContext;
                var userExist = false;
                var user = new User();

                user.username = model.Login;
                user.password = model.Password;
                user.mail = model.Mail;

                if (model.Password!=model.PasswordConfirm)
                {
                    MessageBox.Show("Пароль и подтверждение пароля не совпадают", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Exclamation, MessageBoxResult.None);
                    return;
                }

                if (!user.Check())
                {
                    MessageBox.Show("При заполнении данных формы допущены ошибки", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Exclamation, MessageBoxResult.None);
                    return;
                }

                using (var db = new DataBaseModel())
                {
                    userExist = db.userdata.Any(
                        usrdata => usrdata.mail.Equals(user.mail, StringComparison.OrdinalIgnoreCase) 
                            && usrdata.username.Equals(user.username) 
                            && usrdata.password.Equals(user.password));
                }

                if (!userExist)
                {
                    MessageBox.Show("Указанные идентификационные данные не верны", "Ошибка авторизации", MessageBoxButton.OK, MessageBoxImage.Information, MessageBoxResult.None);
                }
                else
                {
                    MessageBox.Show("Добро пожаловать, " + user.username, "Успешная авторизация", MessageBoxButton.OK, MessageBoxImage.Information, MessageBoxResult.None);
                }
            }
            catch (Exception exp)
            {
                var message = "В процессе авторизации произошла ошибка:\n" + exp.Message;
                MessageBox.Show(message, "Техническая ошибка",
                    MessageBoxButton.OK, MessageBoxImage.Error, MessageBoxResult.None);
            }
        }
    }
}
