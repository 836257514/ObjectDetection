using ObjectDetection.Interface;
using ObjectDetection.Utility;
using ObjectDetection.View;
using System.Windows;

namespace ObjectDetection
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            ElementContainer.Instance.RegistElement<IDialog>(new Dialog());
        }
    }
}
