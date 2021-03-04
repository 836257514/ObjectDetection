using Emgu.CV;
using ObjectDetection.Interface;
using System.Drawing;

namespace ObjectDetection.MachineLearning
{
    class HogBase
    {
        protected HOGDescriptor _hogDescriptor;

        protected HogBase()
        {
            //窗口大小，样本大小
            var winSize = new Size(HogConstant.HogWindowSize, HogConstant.HogWindowSize);
            //blockSize,块大小
            var blockSize = new Size(16, 16);
            //滑块大小，每次滑动的尺寸
            var blockStride = new Size(8, 8);
            //胞元大小
            var cellSize = new Size(8, 8);
            // nBins表示在一个胞元（cell）中统计梯度的方向数目，例如nBins=9时，在一个胞元内统计9个方向的梯度直方图，每个方向为180/9=20度
            var nBins = 9;
            //hog描述子
            _hogDescriptor = new HOGDescriptor(winSize, blockSize, blockStride, cellSize, nBins);
        }

        private int CreateDimension(Size winSize, Size blockSize, Size blockStride, Size cellSize, int nBins)
        {
            var a = (winSize.Width - blockSize.Width) / blockStride.Width + 1;
            var b = (winSize.Height - blockSize.Height) / blockStride.Height + 1;
            var c = blockSize.Width / cellSize.Width;
            var d = blockSize.Height / cellSize.Height;

            return a * b * c * d * nBins;
        }
    }
}
