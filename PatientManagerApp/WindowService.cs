using PatientManagerApp.Views;
using System.Windows;

namespace PatientManagerApp
{
    public class WindowService : IWindowService
    {
        public void ShowAddNewPatient(object viewModel)
        {
            var view = new AddNewPatientView();
            view.DataContext = viewModel;
            view.Owner = Application.Current.MainWindow;
            view.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            view.ShowDialog();
        }
    }
}
