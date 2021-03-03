using Emgu.CV;
using ObjectDetection.ViewModel;
using System.Windows;

namespace ObjectDetection.View
{
    /// <summary>
    /// Interaction logic for TrainImageView.xaml
    /// </summary>
    public partial class TrainImageView
    {
        private TrainImageTabViewModel _viewModel;

        public TrainImageView()
        {
            InitializeComponent();
            DataContextChanged += OnDataContextChanged;
        }

        private void OnDataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (e.NewValue is TrainImageTabViewModel viewModel)
            {
                _viewModel = viewModel;
                _viewModel.ImageLoaded += OnImageLoaded;
            }
        }

        private void OnImageLoaded(object sender, Mat e)
        {
            
        }
    }
}
