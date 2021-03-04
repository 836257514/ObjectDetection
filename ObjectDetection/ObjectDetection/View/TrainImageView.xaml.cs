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
            _vm.ImageInfo.Initialize(img.ActualWidth, img.ActualHeight, (int)img.Source.Width, (int)img.Source.Height);
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

        private void OnKeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            switch (e.Key)
            {
                case System.Windows.Input.Key.S:
                    {
                        _vm.SaveCommand.Execute(null);
                        break;
                    }
                case System.Windows.Input.Key.A:
                    {
                        _vm.PreviousCommand.Execute(null);
                        break;
                    }
                case System.Windows.Input.Key.D:
                    {
                        _vm.NextCommand.Execute(null);
                        break;
                    }
            }
        }
    }
}
