using Convertly.Model.Data;
using Convertly.ViewModels.Commands;
using Microsoft.Win32;
using Microsoft.Windows.Themes;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Navigation;

namespace Convertly.ViewModels
{
    class ImageConvertPageViewModel : BaseViewModel
    {
        public ImageConvertPageViewModel()
        {
            
        }


        #region Свойства для ComboBox

        private List<string> _sortingSelection;
        public List<string> SortingSelection { get; set; } = ["Выбрать все PNG", "Выбрать все JPG",];

        public List<string> ConvertFormat { get; set; } = ["PNG", "JPG", "ICO", "HDR/EXP", "BMP", "ESP", "SVG", "TGA", "TIFF", "WBMP", "WebP"];

        public List<string> ColorFilter { get; set; } = ["Цветное", "Градиент серого", "Монохромное", "Инвертировать цвета", "Ретро", "Сепия"];

        #endregion


        #region Свойства

        public ObservableCollection<FileInformation> FilesInfo { get; set; } = [];

        private int _numberFiles;
        public int NumberFiles
        {
            get
            {
                return _numberFiles;
            }

            set
            {
                _numberFiles = value;
                OnPropertyChanged();
            }
        }

        #region Свйоства для TextBlock с информацией о изображении

        private FileInformation _selectedFileInformation;
        public FileInformation SelectedFileInformation
        {
            get => _selectedFileInformation;

            set
            {
                _selectedFileInformation = value;
                OnPropertyChanged();
                SetInfoImage();
            }
        }

        private string _imageSourse;
        public string ImageSourse
        {
            get => _imageSourse;

            set
            {
                _imageSourse = value;
                OnPropertyChanged();
            }
        }

        private string _fileNameSelected;
        public string FileNameSelected
        {
            get => _fileNameSelected;

            set
            {
                _fileNameSelected = value;
                OnPropertyChanged();
            }
        }

        private string _fileTypeSelected;
        public string FileTypeSelected
        {
            get => _fileTypeSelected;

            set
            {
                _fileTypeSelected = value;
                OnPropertyChanged();
            }
        }

        private string _fileSizeSelected;
        public string FileSizeSelected
        {
            get => _fileSizeSelected;

            set
            {
                _fileSizeSelected = value;
                OnPropertyChanged();
            }
        }

        private string _fileResolutionSelected;
        public string FileResolutionSelected
        {
            get => _fileResolutionSelected;

            set
            {
                _fileResolutionSelected = value;
                OnPropertyChanged();
            }
        }



        #endregion



        #endregion


        #region Команды

        private RelayCommand _selectFileCommand;
        public RelayCommand SelectFileCommand
        {
            get
            {
                return _selectFileCommand ?? (_selectFileCommand = new RelayCommand(obj =>
                {
                    SelectFile();
                }));
            }
        }

        private RelayCommand _deleteFilesCommand;
        public RelayCommand DeleteFilesCommand
        {
            get
            {
                return _deleteFilesCommand ?? (_deleteFilesCommand = new RelayCommand(obj =>
                {
                    DeleteFiles();
                }));
            }
        }

        #endregion


        #region Методы

        private void SelectFile()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Multiselect = true;
            openFileDialog.Filter = "Изображения (.jpg; .jpeg; .png)|*.jpg; *.jpeg; *.png";
            openFileDialog.InitialDirectory = @"D:\Files\Картинки\1 - Скрины\Destiny 2";

            if (openFileDialog.ShowDialog() == true)
            {
                foreach (var file in openFileDialog.FileNames)
                {
                    FileInfo fileInfo = new FileInfo(file);
                    FilesInfo.Add(new FileInformation
                    {
                        FileUri = file,
                        FileName = fileInfo.Name,
                        FileType = fileInfo.Extension,
                        FileCreatTime = fileInfo.CreationTime.ToString(),
                        FileSize = (fileInfo.Length / 1024).ToString("n2") + " kb",
                        FIleTimeOfChange = fileInfo.LastWriteTime.ToString(),
                        FileResolution = GetImageResolution(file)
                    });

                    NumberFiles = FilesInfo.Count;
                }
            }
        }


        private void DeleteFiles()
        {
            if (MessageBox.Show("Вы точно хотите удалить все файлы?", "Точно?", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                FilesInfo.Clear();
            }
            else return;
        }


        private string GetImageResolution(string Url)
        {
            try
            {
                using (var image = Image.FromFile(Url))
                {
                    string width = image.Width.ToString();
                    string height = image.Height.ToString();

                    return width + "x" + height;
                }
            }
            catch (Exception)
            {

                return string.Empty;
            }
        }

        public void SetInfoImage()
        {
            if (_selectedFileInformation == null)
            {
                ImageSourse = null;
                FileNameSelected = "Нету данных";
                FileTypeSelected = "Нету данных";
                FileSizeSelected = "Нету данных";
                FileResolutionSelected = "Нету данных";
            }
            else
            {
                ImageSourse = _selectedFileInformation.FileUri;
                FileNameSelected = Path.GetFileNameWithoutExtension(_selectedFileInformation.FileName);
                FileTypeSelected = _selectedFileInformation.FileType;
                FileSizeSelected = _selectedFileInformation.FileSize;
                FileResolutionSelected = _selectedFileInformation.FileResolution;
            }
        }

        #endregion
    }
}
