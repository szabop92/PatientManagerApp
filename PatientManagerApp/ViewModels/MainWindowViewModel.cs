using PatientManagerApp.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace PatientManagerApp.ViewModels
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        private readonly IDataAccess _db;
        private readonly IWindowService _service;

        private List<Patient> _patients;
        public List<Patient> Patients
        {
            get { return _patients; }
            set
            {
                _patients = value;
                NotifyPropertyChanged();
            }
        }

        private Patient _selectedPatient;
        public Patient SelectedPatient
        {
            get { return _selectedPatient; }
            set
            {
                _selectedPatient = value;
                NotifyPropertyChanged();
            }
        }

        public MainWindowViewModel()
        {
            this._db = new DataAccess();
            this._service = new WindowService();

            FillPatients();
        }

        private void FillPatients()
        {
            Patients = new List<Patient>();

            var patientRecords = _db.GetPatients();

            foreach (var p in patientRecords)
            {
                Patients.Add(new Patient()
                {
                    Id = p.Id,
                    FirstName = p.FirstName,
                    LastName = p.LastName,
                    DateOfBirth = p.DateOfBirth,
                    SocialSecurityNumber = p.SocialSecurityNumber,
                    PhoneNumber = p.PhoneNumber,
                    Email = p.Email
                });
            }
        }

        private ICommand _updatePatientCommand;
        public ICommand UpdatePatientCommand
        {
            get
            {
                if (_updatePatientCommand == null)
                {
                    _updatePatientCommand = new RelayCommand(
                        param => this.UpdatePatient(),
                        param => this.CanUpdateOrDelete()
                        );
                }
                return _updatePatientCommand;
            }
        }

        private void UpdatePatient()
        {
            if (SelectedPatient != null && SelectedPatient.Id != 0)
            {
                var newPatient = new Patient()
                {
                    Id = SelectedPatient.Id,
                    FirstName = SelectedPatient.FirstName,
                    LastName = SelectedPatient.LastName,
                    DateOfBirth = SelectedPatient.DateOfBirth,
                    PhoneNumber = SelectedPatient.PhoneNumber,
                    Email = SelectedPatient.Email,
                    SocialSecurityNumber = SelectedPatient.SocialSecurityNumber
                };

                _db.UpdatePatient(newPatient);
            }

            FillPatients();
        }

        private ICommand _deletePatientCommand;
        public ICommand DeletePatientCommand
        {
            get
            {
                if (_deletePatientCommand == null)
                {
                    _deletePatientCommand = new RelayCommand(
                        param => this.DeletePatient(),
                        param => this.CanUpdateOrDelete()
                        );
                }
                return _deletePatientCommand;
            }
        }

        private void DeletePatient()
        {
            if (SelectedPatient != null && SelectedPatient.Id != 0)
            {
                _db.DeletePatient(SelectedPatient.Id);
            }

            SelectedPatient = null;

            FillPatients();
        }

        private bool CanUpdateOrDelete()
        {
            return SelectedPatient == null ? false : true;
        }

        private ICommand _addNewPatientCommand;
        public ICommand AddNewPatientCommand
        {
            get
            {
                if (_addNewPatientCommand == null)
                {
                    _addNewPatientCommand = new RelayCommand(
                        param => this.AddNewPatient());
                }
                return _addNewPatientCommand;
            }
        }

        private void AddNewPatient()
        {
            _service.ShowAddNewPatient(new AddNewPatientViewModel());

            FillPatients();
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
