using Microsoft.Extensions.DependencyInjection;
using ppedv.CyanBayCars.UI.WPF.ViewModels;
using System.Windows.Controls;

namespace ppedv.CyanBayCars.UI.WPF.Views
{
    /// <summary>
    /// Interaction logic for CarView.xaml
    /// </summary>
    public partial class CarView : UserControl
    {
        public CarView()
        {
            InitializeComponent();
            this.DataContext = App.Current.Services.GetService<CarViewModel>();
        }
    }
}
