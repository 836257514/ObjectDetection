using System.Drawing;

namespace ObjectDetection.Interface
{
    public interface IImageWrapper
    {
        int ClickX { get; set; }
        int ClickY { get; set; }
        double XRatio { get;  }
        double YRatio { get; }
        int ImageWidth { get; }
        Rectangle GetRoi();
        void Initialize(double controlW, double controlH, int imageW, int imageH);
    }
}
