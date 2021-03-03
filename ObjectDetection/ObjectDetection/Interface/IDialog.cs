namespace ObjectDetection.Interface
{
    public interface IDialog : IElement
    {
        string ShowFolderBrowser();

        string ShowOpenFileDialog();

        void ShowErrorMessage(string msg);

        void ShowMessage(string msg);
    }
}
