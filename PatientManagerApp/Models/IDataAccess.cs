using System.Collections.Generic;

namespace PatientManagerApp.Models
{
    public interface IDataAccess
    {
        IEnumerable<Patient> GetPatients(int startIndex, int pageSize);

        int GetCountOfPatients();

        void AddNewPatient(Patient patient);

        void UpdatePatient(Patient patient);

        void DeletePatient(int id);
    }
}
