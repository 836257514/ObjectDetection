using Emgu.CV;
using Emgu.CV.Structure;
using ObjectDetection.Extention;
using ObjectDetection.Interface;
using ObjectDetection.MachineLearning;
using ObjectDetection.Model;
using ObjectDetection.Utility;
using ObjectDetection.ViewModel.Command;
using System;
using System.IO;
using System.Windows.Media.Imaging;

namespace ObjectDetection.ViewModel
{
    class PredictImageTabViewModel : NotificationObject
    {
        private MCvScalar _mCvScalar = new MCvScalar(0, 255, 0);
        private IDialog _dialog;
        private SVMPredict _svmPredict;
        private WriteableBitmap _writeableBitmap;
        public RelayCommand SelectImageCommand { get; }
        public RelayCommand TrainSvmCommand { get; }

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

        public PredictImageTabViewModel()
        {
            _dialog = ElementContainer.Instance.GetElement<IDialog>();
            _svmPredict = new SVMPredict();
            TrainSvmCommand = new RelayCommand(OnTrainSvmCommandExecute, "Train");
            SelectImageCommand = new RelayCommand(OnSelectImageExecute, "Select");
        }

        private void OnSelectImageExecute(object obj)
        {
            if (!File.Exists(HogConstant.SavePath))
            {
                return;
            }

            if (!_svmPredict.IsInitialized)
            {
                _svmPredict.Initialize(HogConstant.SavePath);
            }

            var imagePath = _dialog.ShowOpenFileDialog();
            if (!string.IsNullOrWhiteSpace(imagePath))
            {
                using (var mat = CvInvoke.Imread(imagePath))
                {
                    ImageSource = mat.ConvertToWriteableBitmap();
                    var result = _svmPredict.Predict(mat);
                    if (result.IsSuccess)
                    {
                        CvInvoke.Rectangle(mat, result.Detection.Rect, _mCvScalar, ImageWrapper.RectThinkness);
                        int baseLine = 0;
                        var size = CvInvoke.GetTextSize(result.Detection.Score.ToString(), Emgu.CV.CvEnum.FontFace.HersheySimplex, 1, 2, ref baseLine);
                        var textPoint = result.Detection.Rect.Location;
                        textPoint.Y -= size.Height;
                        CvInvoke.PutText(mat, result.Detection.Score.ToString(), textPoint, Emgu.CV.CvEnum.FontFace.HersheySimplex, 1, _mCvScalar, 2);
                        mat.CopyToWriteableBitmap(ImageSource);
                    }
                }
            }
        }

        private void OnTrainSvmCommandExecute(object obj)
        {
            var svmTrain = new SVMTrain();
            try
            {
                svmTrain.Train(HogConstant.PositiveFolderName, HogConstant.NegtiveFolderName, HogConstant.SavePath);
                _dialog.ShowMessage("Train finished");
            }
            catch (Exception ex)
            {
                _dialog.ShowErrorMessage(ex.StackTrace.ToString());
            }
        }
    }
}
