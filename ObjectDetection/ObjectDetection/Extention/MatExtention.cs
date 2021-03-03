﻿using Emgu.CV;
using ObjectDetection.Utility;
using System.Windows.Media.Imaging;

namespace ObjectDetection.Extention
{
    public static class MatExtention
    {
        public static void CopyToWriteableBitmap(this Mat mat, WriteableBitmap bitmap)
        {
            var length = mat.Step * mat.Rows;
            try
            {
                bitmap.Lock();
                if (mat.Cols % 4 == 0)
                {
                    NativeCaller.CopyMemory(bitmap.BackBuffer, mat.DataPointer, (uint)length);
                }
                else
                {
                    for (int row = 0; row < mat.Rows; row++)
                    {
                        for (int col = 0; col < mat.Cols; col++)
                        {
                            var matStep = mat.Step * row;
                            var wbStep = bitmap.BackBufferStride * row;
                            NativeCaller.CopyMemory(bitmap.BackBuffer + wbStep, mat.DataPointer + matStep, (uint)mat.Step);
                        }
                    }
                }

                bitmap.AddDirtyRect(new System.Windows.Int32Rect(0, 0, bitmap.PixelWidth, bitmap.PixelHeight));
            }
            finally
            {
                bitmap.Unlock();
            }
        }
    }

}
