using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ppedv.CyanBayCars.Data.EfCore;
using ppedv.CyanBayCars.Models;
using ppedv.CyanBayCars.Models.Contracts;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
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

        private IRepository repo;
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

        public CarViewModel(IRepository repo)
        {
            this.repo = repo;
            CarList = new ObservableCollection<Car>(repo.Query<Car>());
            //CarList = new ObservableCollection<Car>();

            SaveCommand = new RelayCommand(() => repo.SaveChanges());

            NewCommand = new RelayCommand(UserWantsToAddNewCar);
        }

        private void UserWantsToAddNewCar()
        {
            var newCar = new Car() { Model="NEU", Color="Gelb" };
            repo.Add(newCar);
            CarList.Add(newCar);
            SelectedCar = newCar;
        }
    }
}
