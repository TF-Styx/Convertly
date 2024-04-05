using Convertly.ViewModels.Commands;
using Convertly.ViewModels.Serviсes.NavigationPage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Convertly.ViewModels
{
    internal class MainWindowViewModel
    {
        private readonly INavigationServices _navigationServices;

        public MainWindowViewModel(NavigationServices navigationServices)
        {
            _navigationServices = navigationServices;
        }

        private RelayCommand _navigateToGIFPageCommand;
        public RelayCommand NavigateToGIFPageCommand
        {
            get
            {
                return _navigateToGIFPageCommand ?? (_navigateToGIFPageCommand = new RelayCommand(obj =>
                {
                    NavigatToGIFPage();
                }));
            }
        }

        private RelayCommand _navigateToImageConvertPageCommand;
        public RelayCommand NavigateToImageConvertPageCommand
        {
            get
            {
                return _navigateToImageConvertPageCommand ?? (_navigateToImageConvertPageCommand = new RelayCommand(obj =>
                {
                    NavigatToImageConvertPage();
                }));
            }
        }

        private void NavigatToGIFPage()
        {
            _navigationServices.NavigateTo("GIFPage", null);
        }

        private void NavigatToImageConvertPage()
        {
            _navigationServices.NavigateTo("ImageConvertPage", null);
        }
    }
}
