namespace ObjectDetection.ViewModel
{
    class MainViewModel
    {
        public TrainImageTabViewModel TrainImageTabViewModel { get; }
        public PredictImageTabViewModel PredictImageTabViewModel { get; }

        public MainViewModel()
        {
            TrainImageTabViewModel = new TrainImageTabViewModel();
            PredictImageTabViewModel = new PredictImageTabViewModel();
        }
    }
}
