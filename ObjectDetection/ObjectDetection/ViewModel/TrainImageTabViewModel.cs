using Emgu.CV;
using Emgu.CV.Structure;
using ObjectDetection.Extention;
using ObjectDetection.Interface;
using ObjectDetection.Model;
using ObjectDetection.Utility;
using ObjectDetection.ViewModel.Command;
using System;
using System.IO;
using System.Linq;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace ObjectDetection.ViewModel
{
    class TrainImageTabViewModel : NotificationObject
    {
        private MCvScalar _mCvScalar = new MCvScalar(0, 255, 0);
        private WriteableBitmap _writeableBitmap;
        private Mat _srcMat;
        private string[] _imagePaths;
        private int _currentIndex;
        private bool _isPositive;

        public IImageWrapper ImageInfo { get; }

        public RelayCommand SelectFolderCommand { get; }

        public RelayCommand PreviousCommand { get; }

        public RelayCommand NextCommand { get; }

        public RelayCommand SaveCommand { get; }

        public WriteableBitmap ImageSource 
        {
            get => _writeableBitmap;
            set
            {
                if (_writeableBitmap != value)
                {
                    _writeableBitmap = value;
                    OnPropertyChanged();
                }
            }
        }

        public int CurrentIndex {
            get => _currentIndex;
            set { 
                if(_currentIndex != value)
                {
                    _currentIndex = value;
                    OnPropertyChanged();
                }
            }
        }

        public bool IsPositive
        {
            get => _isPositive;
            set
            {
                if (value != _isPositive)
                {
                    _isPositive = value;
                    OnPropertyChanged();
                }
            }
        }

        public TrainImageTabViewModel()
        {
            ImageInfo = new ImageWrapper();
            PreviousCommand = new RelayCommand(OnPreviousExecute, "Previous");
            NextCommand = new RelayCommand(OnNextExecute, "Next");
            SelectFolderCommand = new RelayCommand(OnSelectFolderExecute, "Folder");
            SaveCommand = new RelayCommand(OnSaveExecute, "Save");
        }

        /// <summary>
        /// Refresh image.
        /// </summary>
        /// <param name="imageWrapper">image wrapper</param>
        public void RefreshImage()
        {
            using (var mat = new Mat(_srcMat.Size, _srcMat.Depth, _srcMat.NumberOfChannels))
            {
                _srcMat.CopyTo(mat);
                CvInvoke.Rectangle(mat, ImageInfo.GetRoi(), _mCvScalar, ImageWrapper.RectThinkness);
                mat.CopyToWriteableBitmap(ImageSource);
            }
        }

        private void OnSaveExecute(object obj)
        {
            using (var mat = new Mat(_srcMat, ImageInfo.GetRoi()))
            {
                var jpegName = $"{DateTime.Now.ToString("yyyy-MM-dd hh-mm-ss")}.jpg";
                if (IsPositive)
                {
                    if (!Directory.Exists(HogConstant.PositiveFolderName))
                    {
                        Directory.CreateDirectory(HogConstant.PositiveFolderName);
                    }

                    var path = Path.Combine(HogConstant.PositiveFolderName, jpegName);
                    mat.Save(path);
                }
                else
                {
                    if (!Directory.Exists(HogConstant.NegtiveFolderName))
                    {
                        Directory.CreateDirectory(HogConstant.NegtiveFolderName);
                    }

                    var path = Path.Combine(HogConstant.NegtiveFolderName, jpegName);
                    mat.Save(path);
                }
            }
        }

        private void OnSelectFolderExecute(object obj)
        {
            var dialog = ElementContainer.Instance.GetElement<IDialog>();
            var folderPath = dialog.ShowFolderBrowser();
            if (!string.IsNullOrWhiteSpace(folderPath))
            {
                _imagePaths = Directory.GetFiles(folderPath);
                if (_imagePaths.Any())
                {
                    CurrentIndex = 0;
                    LoadImage();
                }
            }
        }

        private void OnNextExecute(object obj)
        {
            if (CurrentIndex < _imagePaths.Length - 1)
            {
                CurrentIndex++;
                LoadImage();
            }
        }

        private void OnPreviousExecute(object obj)
        {
            if (CurrentIndex > 0)
            {
                CurrentIndex--;
                LoadImage();
            }
        }

        private void LoadImage()
        {
            _srcMat = CvInvoke.Imread(_imagePaths[CurrentIndex]);
            var format = PixelFormats.Bgr24;
            if (_srcMat.NumberOfChannels == 4)
            {
                format = PixelFormats.Bgra32;
            }
            if (_srcMat.NumberOfChannels == 1)
            {
                format = PixelFormats.Gray8;
            }
            ImageSource = new WriteableBitmap(_srcMat.Width, _srcMat.Height, 96, 96, format, null);
            _srcMat.CopyToWriteableBitmap(ImageSource);
        }
    }
}
