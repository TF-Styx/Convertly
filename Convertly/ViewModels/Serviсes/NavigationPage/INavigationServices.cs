using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Convertly.ViewModels.Serviсes.NavigationPage
{
    interface INavigationServices
    {
        void NavigateTo(string namePage, object parametr);
    }
}
