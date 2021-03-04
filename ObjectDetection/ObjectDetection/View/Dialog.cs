using ObjectDetection.Interface;
using System.Windows;

namespace ObjectDetection.View
{
    class Dialog : IDialog
    {
        public void ShowErrorMessage(string msg)
        {
            MessageBox.Show(msg, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        public string ShowFolderBrowser()
        {
            System.Windows.Forms.FolderBrowserDialog openFileDialog = new System.Windows.Forms.FolderBrowserDialog();
            if (openFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                return openFileDialog.SelectedPath;
            }

            return string.Empty;
        }

        public void ShowMessage(string msg)
        {
            MessageBox.Show(msg);
        }

        public string ShowOpenFileDialog()
        {
            System.Windows.Forms.OpenFileDialog openFileDialog = new System.Windows.Forms.OpenFileDialog();
            if (openFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                return openFileDialog.FileName;
            }

            return string.Empty;
        }
    }
}
