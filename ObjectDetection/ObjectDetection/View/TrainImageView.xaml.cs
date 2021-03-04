using ObjectDetection.ViewModel;
using System.Windows;

namespace ObjectDetection.View
{
    public partial class TrainImageView
    {
        private TrainImageTabViewModel _vm;

        public TrainImageView()
        {
            InitializeComponent();
            DataContextChanged += OnDataContextChanged;
        }

        private void OnDataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (e.NewValue is TrainImageTabViewModel vm)
            {
                _vm = vm;
            }
        }

        private void OnImageMouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (_vm.ImageInfo.ImageWidth == 0 || (int)img.Source.Width != _vm.ImageInfo.ImageWidth)
            {
                _vm.ImageInfo.Initialize(img.ActualWidth, img.ActualHeight, (int)img.Source.Width, (int)img.Source.Height);
            }

            SetPosition(sender, e);
        }

        private void OnImageMouseMove(object sender, System.Windows.Input.MouseEventArgs e)
        {
            if (e.LeftButton == System.Windows.Input.MouseButtonState.Pressed)
            {
                SetPosition(sender, e);
            }
        }

        private void SetPosition(object sender, System.Windows.Input.MouseEventArgs e)
        {
            var point = e.GetPosition(sender as IInputElement);
            _vm.ImageInfo.ClickX = (int)point.X;
            _vm.ImageInfo.ClickY = (int)point.Y;
            _vm.RefreshImage();
        }
    }
}
