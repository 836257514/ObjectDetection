using ObjectDetection.Interface;
using System.Drawing;

namespace ObjectDetection.Model
{
    class ImageWrapper : IImageWrapper
    {
        private int _imageRightEdge;
        private int _imageBottomEdge;
        private Rectangle _rect = new Rectangle(0, 0, HogConstant.HogWindowSize, HogConstant.HogWindowSize);
        public const int RectThinkness = 2;
        public int ClickX { get; set;  }
        public int ClickY { get; set; }
        public double XRatio { get; private set; }
        public double YRatio { get; private set; }
        public int ImageWidth { get; private set; }

        public ImageWrapper()
        {
        }

        public void Initialize(double controlW, double controlH, int imageW, int imageH)
        {
            ImageWidth = imageW;
            _imageRightEdge = imageW - HogConstant.HogWindowSize - RectThinkness;
            _imageBottomEdge = imageH - HogConstant.HogWindowSize - RectThinkness;
            XRatio = controlW / imageW;
            YRatio = controlH / imageH;
        }

        /// <summary>
        /// Get roi
        /// </summary>
        /// <returns>roi</returns>
        public Rectangle GetRoi()
        {
            _rect.X = (int)(ClickX / XRatio) - HogConstant.HogWindowSize / 2;
            _rect.Y = (int)(ClickY / YRatio) - HogConstant.HogWindowSize / 2;

            if (_rect.X < 0)
            {
                _rect.X = 0;
            }
            else if(_rect.X > _imageRightEdge)
            {
                _rect.X = _imageRightEdge;
            }

            if (_rect.Y < 0)
            {
                _rect.Y = 0;
            }
            else if (_rect.Y > _imageBottomEdge)
            {
                _rect.Y = _imageBottomEdge;
            }

            return _rect;
        }
    }
}
