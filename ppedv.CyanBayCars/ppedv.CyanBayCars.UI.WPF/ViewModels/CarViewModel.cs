using ppedv.CyanBayCars.Data.EfCore;
using ppedv.CyanBayCars.Models;
using ppedv.CyanBayCars.Models.Contracts;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace ppedv.CyanBayCars.UI.WPF.ViewModels
{
    public class CarViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<Car> CarList { get; set; }

        public Car SelectedCar
        {
            get => selectedCar;
            set
            {
                selectedCar = value;
                PropertyChanged?.Invoke(this,new PropertyChangedEventArgs(nameof(SelectedCar)));
                PropertyChanged?.Invoke(this,new PropertyChangedEventArgs(nameof(KW)));
            }
        }

        private IRepository repo;
        private Car selectedCar;

        public event PropertyChangedEventHandler? PropertyChanged;

        public string KW
        {
            get
            {
                if (SelectedCar == null)
                    return "?";
                return (SelectedCar.PS / 1.36).ToString();
            }
        }

        //todo kill it
        public CarViewModel() : this(new EfRepository("Server=(localdb)\\mssqllocaldb;Database=CyanBayCars_Dev;Trusted_Connection=true;"))
        { }

        public CarViewModel(IRepository repo)
        {
            this.repo = repo;
            CarList = new ObservableCollection<Car>(repo.Query<Car>());
        }
    }
}
