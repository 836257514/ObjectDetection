using Emgu.CV;
using ObjectDetection.Interface;
using ObjectDetection.Utility;
using ObjectDetection.ViewModel.Command;
using System;
using System.IO;
using System.Linq;

namespace ObjectDetection.ViewModel
{
    class TrainImageTabViewModel : NotificationObject
    {
        private string[] _imagePaths;
        private int _currentIndex;
        private bool _isPositive;
        public event EventHandler<Mat> ImageLoaded;

        public RelayCommand SelectFolderCommand { get; }

        public RelayCommand PreviousCommand { get; }

        public RelayCommand NextCommand { get; }

        public RelayCommand SaveCommand { get; }

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
            PreviousCommand = new RelayCommand(OnPreviousExecute, "Previous");
            NextCommand = new RelayCommand(OnNextExecute, "Next");
            SelectFolderCommand = new RelayCommand(OnSelectFolderExecute, "Folder");
            SaveCommand = new RelayCommand(OnSaveExecute, "Save");
        }

        private void OnSaveExecute(object obj)
        {
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
            var mat = CvInvoke.Imread(_imagePaths[CurrentIndex]);
            ImageLoaded?.Invoke(this, mat);
        }
    }
}
