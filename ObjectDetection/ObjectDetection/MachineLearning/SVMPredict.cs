using Emgu.CV;
using Emgu.CV.ML;
using Emgu.CV.Structure;
using System.Linq;
namespace ObjectDetection.MachineLearning
{
    public class DetectContext
    {
        public bool IsSuccess { get; }

        public MCvObjectDetection Detection { get; }

        public DetectContext(MCvObjectDetection detection)
        {
            IsSuccess = detection.Score > 0.8;
            Detection = detection;
        }
    }

    class SVMPredict : HogBase
    {
        /// <summary>
        /// Initialize svm by xml.
        /// </summary>
        /// <param name="xmlPath">xml path</param>
        public void Initialize(string xmlPath)
        {
            var svm = new SVM();
            svm.Load(xmlPath);
            var supportVectors = svm.GetSupportVectors();
            var reader = new SvmReader();
            var context = reader.Read(supportVectors, xmlPath);
            _hogDescriptor.SetSVMDetector(context.GetSvmDescriptor());
        }

        /// <summary>
        /// Predict img
        /// </summary>
        /// <param name="mat">mat to detect.</param>
        /// <returns>detect context.</returns>
        public DetectContext Predict(Mat mat)
        {
            MCvObjectDetection[] detections = _hogDescriptor.DetectMultiScale(mat);
            var detection = detections.OrderByDescending(m => m.Score).FirstOrDefault();
            return new DetectContext(detection);
        }
    }
}
