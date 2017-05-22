using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab1
{
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
        }
    }
}
