using PatientManagerApp.Models;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace PatientManagerApp.ViewModels
{
    public class AddNewPatientViewModel
    {
        private readonly IDataAccess _db;

        public AddNewPatientViewModel()
        {
            this._db = new DataAccess();
        }

        private string _firstName;
        public string FirstName
        {
            get { return _firstName; }
            set
            {
                _firstName = value;
                NotifyPropertyChanged();
            }
        }

        private string _lastName;
        public string LastName
        {
            get { return _lastName; }
            set
            {
                _lastName = value;
                NotifyPropertyChanged();
            }
        }

        private DateTime? _dateOfBirth;
        public DateTime? DateOfBirth
        {
            get { return _dateOfBirth; }
            set
            {
                _dateOfBirth = value;
                NotifyPropertyChanged();
            }
        }

        private string _socialSecurityNumber;
        public string SocialSecurityNumber
        {
            get { return _socialSecurityNumber; }
            set
            {
                _socialSecurityNumber = value;
                NotifyPropertyChanged();
            }
        }

        private string _phoneNumber;
        public string PhoneNumber
        {
            get { return _phoneNumber; }
            set
            {
                _phoneNumber = value;
                NotifyPropertyChanged();
            }
        }

        private string _email;
        public string Email
        {
            get { return _email; }
            set
            {
                _email = value;
                NotifyPropertyChanged();
            }
        }

        private ICommand _savePatientCommand;
        public ICommand SavePatientCommand
        {
            get
            {
                if (_savePatientCommand == null)
                {
                    _savePatientCommand = new RelayCommand(
                        param => this.SavePatient(),
                        param => this.CanSave()
                        );
                }
                return _savePatientCommand;
            }
        }

        private void SavePatient()
        {
            var newPatient = new Patient()
            {
                FirstName = FirstName,
                LastName = LastName,
                DateOfBirth = DateOfBirth.Value,
                SocialSecurityNumber = SocialSecurityNumber,
                PhoneNumber = PhoneNumber,
                Email = Email
            };

            _db.AddNewPatient(newPatient);
        }

        private bool CanSave()
        {
            return String.IsNullOrWhiteSpace(FirstName) ||
                   String.IsNullOrWhiteSpace(LastName) ||
                   DateOfBirth == null ||
                   String.IsNullOrWhiteSpace(SocialSecurityNumber) ||
                   String.IsNullOrWhiteSpace(PhoneNumber) ||
                   String.IsNullOrWhiteSpace(Email) ||
                   String.IsNullOrWhiteSpace(FirstName) ? false : true;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
