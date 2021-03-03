using ObjectDetection.Interface;
using System;

namespace ObjectDetection.View
{
    class Dialog : IDialog
    {
        public void ShowErrorMessage(string msg)
        {
            throw new NotImplementedException();
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
            throw new NotImplementedException();
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
