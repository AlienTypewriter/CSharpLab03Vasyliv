using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace WpfApp1
{
    public class VM : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        string name;
        string surname;
        string email;
        string birthdate;
        private LogDataCommand getResult;
        private Person entered;
        public string Name { get => name; set
            {
                name = value;
                NotifyPropertyChanged("Name");

            }
        }
        public string Surname { get => surname; set
            {
                surname = value;
                NotifyPropertyChanged("Surname");
            }
        }
        public string Email { get => email; set
            {
                email = value;
                NotifyPropertyChanged("Email");
            }
        }
        public string Birthdate { get => birthdate; set
            {
                birthdate = value;
                NotifyPropertyChanged("Birthdate");
            }
        }
        internal Person Entered { get => entered; set => entered = value; }
        public LogDataCommand GetResult { get => getResult; set => getResult = value; }

        public VM()
        {
            GetResult = new LogDataCommand(this);
        }
    }
    public class LogDataCommand : ICommand
    {
        VM parent;

        public LogDataCommand(VM parent)
        {
            this.parent = parent;
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public void RaiseCanExecuteChanged()
        {
            CommandManager.InvalidateRequerySuggested();
        }


        public bool CanExecute(object parameter)
        {
            if (string.IsNullOrEmpty(parent.Name)) return false;
            if (string.IsNullOrEmpty(parent.Surname)) return false;
            if (string.IsNullOrEmpty(parent.Email)) return false;
            if (string.IsNullOrEmpty(parent.Birthdate)) return false;
            return true;
        }

        public void Execute(object parameter)
        {
            DateTime selDate;
            try
            {
                if (!DateTime.TryParse(parent.Birthdate, out selDate))
                    throw new FormatException("String does not represent a valid date");
                TimeSpan dif = DateTime.Now - selDate;
                if (DateTime.Now.AddDays(dif.TotalDays) < DateTime.Now.AddYears(-120) || dif.Seconds < 0)
                    throw new ArgumentOutOfRangeException("Date should not be more than 120 years in the past or in the future");
                if (!IsValidEmail(parent.Email)) throw new ArgumentException("Invalid email address");
                parent.Entered = new Person(parent.Name, parent.Surname, parent.Email, selDate);
            }
            catch (Exception e)
            {
                System.Windows.MessageBox.Show(e.Message);
            }
        }

        private bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }
    }
}
