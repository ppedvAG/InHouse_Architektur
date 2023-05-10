using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ppedv.CyanBayCars.Models;
using ppedv.CyanBayCars.Models.Contracts;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace ppedv.CyanBayCars.UI.WPF.ViewModels
{
    public class CarViewModel : ObservableObject
    {
        public ObservableCollection<Car> CarList { get; set; }

        public Car SelectedCar
        {
            get => selectedCar;
            set
            {
                selectedCar = value;
                OnPropertyChanged(nameof(SelectedCar));
                OnPropertyChanged(nameof(KW));
                //PropertyChanged?.Invoke(this,new PropertyChangedEventArgs(nameof(SelectedCar)));
                //PropertyChanged?.Invoke(this,new PropertyChangedEventArgs(nameof(KW)));
            }
        }

        public ICommand SaveCommand { get; set; }
        public ICommand NewCommand { get; set; }
        public ICommand DeleteCommand { get; set; }
        public ICommand LoadCommand { get; set; }

        private IUnitOfWork unitOfWork;
        private Car selectedCar;

        public string KW
        {
            get
            {
                if (SelectedCar == null)
                    return "?";
                return (SelectedCar.PS / 1.36).ToString();
            }
        }

        public CarViewModel(IUnitOfWork unitOfWork )
        {
            this.unitOfWork = unitOfWork;
            CarList = new ObservableCollection<Car>(unitOfWork.CarRepo.Query());
            //CarList = new ObservableCollection<Car>();

            SaveCommand = new RelayCommand(() => unitOfWork.SaveChanges());

            NewCommand = new RelayCommand(UserWantsToAddNewCar);
        }

        private void UserWantsToAddNewCar()
        {
            var newCar = new Car() { Model="NEU", Color="Gelb" };
            unitOfWork.CarRepo.Add(newCar);
            CarList.Add(newCar);
            SelectedCar = newCar;
        }
    }
}
