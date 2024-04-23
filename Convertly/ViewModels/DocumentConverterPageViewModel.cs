using Convertly.Model.Data;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Convertly.ViewModels
{
    internal class DocumentConverterPageViewModel
    {
        public List<string> ConvertFormat { get; set; } = ["Не выбран", "PDF", "Docx", "Doc", "fb2", "Мамонт", "Григорий", "Лепс", "Иванушки"];

        public ObservableCollection<FileInformation> FileList { get; set; } = new ObservableCollection<FileInformation>()
{
    new FileInformation(){ FileName = "Залупа 3д", FileUri = @"C:\Users\texno\OneDrive\Рабочий стол\013_res.png", FileSize = "34", FileType = "PDF"},
    new FileInformation(){ FileName = "Залупа", FileUri = @"C:\Users\texno\OneDrive\Рабочий стол\013_res.png", FileSize = "34", FileType = "Fb2"},
    new FileInformation(){ FileName = "Херня", FileUri = @"C:\Users\texno\OneDrive\Рабочий стол\013_res.png", FileSize = "34", FileType = "PDF"},
    new FileInformation(){ FileName = "Гном", FileUri = @"C:\Users\texno\OneDrive\Рабочий стол\013_res.png", FileSize = "34", FileType = "PDF"},
    new FileInformation(){ FileName = "Книга", FileUri = @"C:\Users\texno\OneDrive\Рабочий стол\013_res.png", FileSize = "34", FileType = "xls"},
    new FileInformation(){ FileName = "Залупа 3д", FileUri = @"C:\Users\texno\OneDrive\Рабочий стол\013_res.png", FileSize = "34", FileType = "PDF"},
    new FileInformation(){ FileName = "Властелин колец", FileUri = @"C:\Users\texno\OneDrive\Рабочий стол\013_res.png", FileSize = "34", FileType = "Fb2"},
    new FileInformation(){ FileName = "Гном", FileUri = @"C:\Users\texno\OneDrive\Рабочий стол\013_res.png", FileSize = "34", FileType = "PDF"},
    new FileInformation(){ FileName = "Гарри Поттер", FileUri = @"C:\Users\texno\OneDrive\Рабочий стол\013_res.png", FileSize = "34", FileType = "Word"},
    new FileInformation(){ FileName = "Челюсти 3д", FileUri = @"C:\Users\texno\OneDrive\Рабочий стол\013_res.png", FileSize = "34", FileType = "PDF"},
    new FileInformation(){ FileName = "ЧВК Вагнер", FileUri = @"C:\Users\texno\OneDrive\Рабочий стол\013_res.png", FileSize = "34", FileType = "PDF"},
    new FileInformation(){ FileName = "Путин", FileUri = @"C:\Users\texno\OneDrive\Рабочий стол\013_res.png", FileSize = "34", FileType = "xls"},
    new FileInformation(){ FileName = "Маша и медведь", FileUri = @"C:\Users\texno\OneDrive\Рабочий стол\013_res.png", FileSize = "34", FileType = "Word"},
    new FileInformation(){ FileName = "Война и Мир", FileUri = @"C:\Users\texno\OneDrive\Рабочий стол\013_res.png", FileSize = "34", FileType = "PDF"},
    new FileInformation(){ FileName = "Под куполом", FileUri = @"C:\Users\texno\OneDrive\Рабочий стол\013_res.png", FileSize = "34", FileType = "xls"},
    new FileInformation(){ FileName = "Гном", FileUri = @"C:\Users\texno\OneDrive\Рабочий стол\013_res.png", FileSize = "34", FileType = "PDF"},
    new FileInformation(){ FileName = "Воинственный бох асура", FileUri = @"C:\Users\texno\OneDrive\Рабочий стол\013_res.png", FileSize = "34", FileType = "PDF"},
    new FileInformation(){ FileName = "Извевающийся дракон", FileUri = @"C:\Users\texno\OneDrive\Рабочий стол\013_res.png", FileSize = "34", FileType = "xls"},
};
    }
}
