using Convertly.Model.Data;
using Convertly.Utils;
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
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;

namespace Convertly.ViewModels
{
    class ImageConvertPageViewModel : BaseViewModel
    {
        public ImageConvertPageViewModel()
        {
            SetInfoImage();

            DefoultComboBoxValue();
        }

        #region ToolTip

        public string ToolTipQualituLableText { get; set; } = "Выберите подходящее качество изображения.\r\nЧем выше качество, тем больше весит файл.\r\nИ наоборот, чем ниже качество, тем меньше размер файла.";
        public string ToolTipTargetFormatLableText { get; set; } = "Формат, в который будет конвертировано изображение";
        public string ToolTipResizeLableText { get; set; } = "Изменениее размера изображения.\r\nСчитается в пикселях";
        public string ToolTipColorFilterLableText { get; set; } = "Применять цветовой фильтр к изображению";
        public string ToolTipAdditionalSettingsLableText { get; set; } = "Разнообразные доп. настройки";

        #endregion


        #region Свойства для ComboBox

        public List<string> ConvertFormat { get; set; } = ["Не выбран", "PNG", "JPEG"];
        public List<string> ColorFilter { get; set; } = ["Без фильтра", "Цветное", "Градиент серого", "Монохромное", "Инвертировать цвета", "Ретро", "Сепия"];
        public ObservableCollection<string> SortingSelection { get; set; } = ["Все файлы", "Выбрать все PNG", "Выбрать все JPG",];

        private string _selectConvertFormat;
        public string SelectConvertFormat 
        {
            get
            {
                return _selectConvertFormat;
            }

            set
            {
                _selectConvertFormat = value;
                OnPropertyChanged();
            }
        }

        private string _selectColorFilter;
        public string SelectColorFilter
        {
            get
            {
                return _selectColorFilter;
            }

            set
            {
                _selectColorFilter = value;
                OnPropertyChanged();
            }
        }

        private string _selectSortingSelection;
        public string SelectSortingSelection
        {
            get
            {
                return _selectSortingSelection;
            }

            set
            {
                _selectSortingSelection = value;
                OnPropertyChanged();
            }
        }

        #endregion


        #region Свойства

        public ObservableCollection<FileInformation> FilesInfo { get; set; } = [];

        private FileInformation _selectedFileInformation;
        public FileInformation SelectedFileInformation
        {
            get => _selectedFileInformation;

            set
            {
                _selectedFileInformation = value;
                OnPropertyChanged();
                SetInfoImage();
                SetHeightAndWidthValue();
            }
        }

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

        private string _inputImageSourse;
        public string InputImageSourse
        {
            get => _inputImageSourse;

            set
            {
                _inputImageSourse = value;
                OnPropertyChanged();
            }
        }

        private Bitmap Render { get; set; }

        private BitmapImage _outputImageSourse;
        public BitmapImage OutputImageSourse
        {
            get => _outputImageSourse;

            set
            {
                _outputImageSourse = value;
                OnPropertyChanged();
            }
        }

        private int _sliderValue;
        public int SliderValue
        {
            get
            {
                return _sliderValue;
            }

            set
            {
                if (value < 0 || value > 100)
                {
                    MessageBox.Show("Значение не должно быть меньше '0' или больше '100'");
                    SliderValue = 0;
                }
                else
                {
                    _sliderValue = value;
                }

                OnPropertyChanged();
            }
        }

        private int _widthValue;
        public int WidthValue
        {
            get
            {
                return _widthValue;
            }

            set
            {
                _widthValue = value;
                OnPropertyChanged();
            }
        }

        private int _heightValue;
        public int HeightValue
        {
            get
            {
                return _heightValue;
            }

            set
            {
                _heightValue = value;
                OnPropertyChanged();
            }
        }

        #endregion


        #region Отображекние информации у файла

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

        private string _fileCreatedTimeSelected;
        public string FileCreatedTimeSelected
        {
            get => _fileCreatedTimeSelected;

            set
            {
                _fileCreatedTimeSelected = value;
                OnPropertyChanged();
            }
        }

        private string _fileTimeOfChangeSelected;
        public string FileTimeOfChangeSelected
        {
            get => _fileTimeOfChangeSelected;

            set
            {
                _fileTimeOfChangeSelected = value;
                OnPropertyChanged();
            }
        }

        #endregion


        #region Команды

        private RelayCommand _selectFileCommand;
        public RelayCommand SelectFileCommand
        {
            get
            {
                return _selectFileCommand ?? (_selectFileCommand = new RelayCommand(async obj =>
                {
                    await SelectFile();
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

        private RelayCommand _previewRenderImageCommand;
        public RelayCommand PreviewRenderImageCommand
        {
            get
            {
                return _previewRenderImageCommand ??= new RelayCommand(async obj =>
                {
                    await PreviewRenderImageAsinc(SelectedFileInformation.FileUri);
                });
            }
        }

        private RelayCommand _saveImageCommand;
        public RelayCommand SaveImageCommand
        {
            get
            {
                return _saveImageCommand ??= new RelayCommand(obj =>
                {
                    SaveImage();
                });
            }
        }

        private RelayCommand _convertAndSaveImageCommand;
        public RelayCommand ConvertAndSaveAllImageCommand
        {
            get
            {
                return _convertAndSaveImageCommand ??= new RelayCommand(async obj =>
                {
                    await ConvertAndSaveAllImageAsync();
                });
            }
        }

        private RelayCommand _clearOutputImageCommand;
        public RelayCommand ClearOutputImageCommand
        {
            get
            {
                return _clearOutputImageCommand ??= new RelayCommand(obj =>
                {
                    OutputImageSourse = null;
                });
            }
        }

        #endregion


        #region Методы

        /// <summary>
        /// Значения по умолчанию ComboBox
        /// </summary>
        private void DefoultComboBoxValue()
        {
            SelectConvertFormat = ConvertFormat.First();
            SelectColorFilter = ColorFilter.First();
            SelectSortingSelection = SortingSelection.First();
        }

        /// <summary>
        /// Выбор файла из проводника и запись объекта в свойство FilesInfo со всей информацией об этом файле
        /// </summary>
        /// <returns></returns>
        private async Task SelectFile()
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
                        FileCreatTime = fileInfo.CreationTime.ToString("f"),
                        FileSize = (fileInfo.Length / 1024).ToString("n2") + " kb",
                        FIleTimeOfChange = fileInfo.LastWriteTime.ToString("f"),
                        FileResolution = await ImageProccesing.GetImageResolution(file)
                    });

                    NumberFiles = FilesInfo.Count;
                }
            }
        }

        /// <summary>
        /// Удалиение файлов из свойства FilesInfo
        /// </summary>
        private void DeleteFiles()
        {
            if (FilesInfo.Count == 0)
            {
                MessageBox.Show("Нечего удалять");
            }
            else if (MessageBox.Show("Вы точно хотите удалить все файлы?", "Точно?", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                FilesInfo.Clear();
                NumberFiles = 0;
            }
            else return;
        }

        /// <summary>
        /// Вывод информации изображения
        /// </summary>
        private void SetInfoImage()
        {
            if (_selectedFileInformation == null)
            {
                InputImageSourse = "Нет данных";
                FileNameSelected = "Нет данных";
                FileTypeSelected = "Нет данных";
                FileSizeSelected = "Нет данных";
                FileResolutionSelected = "Нет данных";
                FileCreatedTimeSelected = "Нет данных";
                FileTimeOfChangeSelected = "Нет данных";
            }
            else
            {
                InputImageSourse = _selectedFileInformation.FileUri;
                FileNameSelected = Path.GetFileNameWithoutExtension(_selectedFileInformation.FileName);
                FileTypeSelected = _selectedFileInformation.FileType;
                FileSizeSelected = _selectedFileInformation.FileSize;
                FileResolutionSelected = _selectedFileInformation.FileResolution;
                FileCreatedTimeSelected = _selectedFileInformation.FileCreatTime;
                FileTimeOfChangeSelected = _selectedFileInformation.FIleTimeOfChange;
            }
        }

        /// <summary>
        /// Вывод изображения в окно исходного изображения
        /// </summary>
        /// <returns></returns>
        private async Task PreviewRenderImageAsinc(string fileUri)
        {
            if (fileUri != null)
            {
                Bitmap bitmap = new Bitmap(fileUri);
                bitmap = await ImageProccesing.ChangingSiceImage(bitmap, WidthValue, HeightValue);
                //bitmap = await ImageProccesing.RemoveHalfPixelsAsync(bitmap);
                
                WidthValue = bitmap.Width;
                HeightValue = bitmap.Height;

                Render = bitmap;
                OutputImageSourse = await ImageProccesing.BitmapToBitmapImageAsync(bitmap);
            }
            else
            {
                MessageBox.Show("Вы не выбрали изображение!");
            }
        }

        /// <summary>
        /// Конвертация всех изображений в FilesInfo и их сохранение в папку на рабочем столе
        /// </summary>
        /// <returns></returns>
        public async Task ConvertAndSaveAllImageAsync()
        {
            if (SelectConvertFormat != "Не выбран")
            {
                foreach (var item in FilesInfo)
                {
                    var result = ImageProccesing.GetImageWidthAndHeight(item.FileUri);
                    WidthValue = result.width;
                    HeightValue = result.height;

                    await PreviewRenderImageAsinc(item.FileUri);

                    switch (SelectConvertFormat)
                    {
                        case "PNG":

                            string savePng = @$"C:\Users\texno\OneDrive\Рабочий стол\TestSaveConvertImage\{Path.GetFileNameWithoutExtension(item.FileName)}.png";
                            ImageProccesing.SaveFileToPNG(Render, savePng);

                            break;


                        case "JPEG":

                            string saveJpeg = @$"C:\Users\texno\OneDrive\Рабочий стол\TestSaveConvertImage\{Path.GetFileNameWithoutExtension(item.FileName)}.jpeg";
                            ImageProccesing.SaveFileToJPEG(Render, saveJpeg);

                            break;
                    }
                    OutputImageSourse = null;
                }
            }
            else MessageBox.Show("Не выбран формат фалйа!");
        }

        /// <summary>
        /// Сохранение изображения в выбранном формате со всеми проверками
        /// </summary>
        private void SaveImage()
        {
            if (SelectedFileInformation == null)
            {
                MessageBox.Show("Вы не загрузили!\nНе выбрали изображение!");
            }
            else if (OutputImageSourse == null)
            {
                MessageBox.Show("Нет измененного изображения!");
            }
            else if (SelectConvertFormat == null || SelectConvertFormat == "Не выбран")
            {
                MessageBox.Show("Вы не выбрали формат файла!");
            }
            else
            {
                switch (SelectConvertFormat)
                {
                    case "PNG":

                        string savePng = @$"C:\Users\texno\OneDrive\Рабочий стол\TestSaveConvertImage\{ Path.GetFileNameWithoutExtension(SelectedFileInformation.FileName)}.png";
                        ImageProccesing.SaveFileToPNG(Render, savePng);

                        break;


                    case "JPEG":

                        string saveJpeg = @$"C:\Users\texno\OneDrive\Рабочий стол\TestSaveConvertImage\{ Path.GetFileNameWithoutExtension(SelectedFileInformation.FileName)}.jpeg";
                        ImageProccesing.SaveFileToJPEG(Render, saveJpeg);

                        break;
                }
            }
        }

        /// <summary>
        /// Получение ширины и высоты по отдельности
        /// </summary>
        private void SetHeightAndWidthValue()
        {
            if (SelectedFileInformation != null)
            {
                var result = ImageProccesing.GetImageWidthAndHeight(SelectedFileInformation.FileUri);

                WidthValue = result.width;
                HeightValue = result.height;
            }
            else
            {
                WidthValue = 0; 
                HeightValue = 0;
            }
        }

        #endregion
    }
}