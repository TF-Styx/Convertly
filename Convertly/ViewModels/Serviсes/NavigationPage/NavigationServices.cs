using Convertly.Views.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Convertly.ViewModels.Serviсes.NavigationPage
{
    internal class NavigationServices : INavigationServices
    {
        private readonly Frame _frame;

        public NavigationServices(Frame frame)
        {
            _frame = frame;
        }

        public void NavigateTo(string namePage, object parametr)
        {
            switch (namePage)
            {
                case "GIFPage":
                    _frame.Navigate(new GIFPage());
                    break;

                case "ImageConvertPage":
                    _frame.Navigate(new ImageConvertPage());
                    break;

                case "DocumentConverterPage":
                    _frame.Navigate(new DocumentConverterPage());
                    break;

                case "PDFPage":
                    _frame.Navigate(new PDFPage());
                    break;

                default:
                    throw new ArgumentException("Страница не найдена");
            }
        }
    }
}
