using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using lab1.db.models;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Security.Cryptography;

namespace lab1
{

    public class OK_ButtonCommandExecute : ICommand
    {
        private MainWindowViewModel model;


        public OK_ButtonCommandExecute(MainWindowViewModel model)
        {
            this.model = model;
        }

        public bool CanExecute(object sender)
        {
            return true;
        }
        public event EventHandler CanExecuteChanged
        {
            add
            {
                
            }

            remove
            {
                
            }
        }
        public void Execute(object sender)
        {
            try
            {
                var model = this.model;
                var userExist = false;
                var user = new User();

                user.username = model.Login;
                user.password = model.Password;
                user.mail = model.Mail;

                if (model.Password != model.PasswordConfirm)
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

    public class MainWindowViewModel : INotifyPropertyChanged
    {
        private string  login;
        private string  password;
        private string  passwordConfirm;
        private string  mail;
        private bool    remember;

        public string   Login
        {
            get { return login; }
            set
            {
                login = value;
                OnPropertyChanged("Login");
            }
        }
        public string Password
        {
            get { return password; }
            set
            {
                password = value;
                OnPropertyChanged("Password");
            }
        }
        public string PasswordConfirm
        {
            get { return passwordConfirm; }
            set
            {
                passwordConfirm = value;
                OnPropertyChanged("PasswordConfirm");
            }
        }
        public string Mail
        {
            get { return mail; }
            set
            {
                mail = value;
                OnPropertyChanged("Mail");
            }
        }
        public bool Remember
        {
            get { return remember; }
            set
            {
                remember = value;
                OnPropertyChanged("Remember");
            }
        }

        private ICommand okButtonCMD;

        public ICommand OKButtonCMD
        {
            get
            {
                return okButtonCMD;
            }
            set
            {
                okButtonCMD = value;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string prop = "")
        {
            if (this.PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }

        public MainWindowViewModel()
        {
            login = "";
            password = "";
            passwordConfirm = "";
            mail = "";
            remember = true;

            okButtonCMD = new OK_ButtonCommandExecute(this); 
        }
    }
}
