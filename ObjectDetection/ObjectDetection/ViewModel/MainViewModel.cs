namespace ObjectDetection.ViewModel
{
    class MainViewModel
    {
        public TrainImageTabViewModel TrainImageTabViewModel { get; }

        public MainViewModel()
        {
            TrainImageTabViewModel = new TrainImageTabViewModel();
        }
    }
}
