using Emgu.CV;
using Emgu.CV.ML;
using Emgu.CV.ML.MlEnum;
using Emgu.CV.Structure;
using System.IO;

namespace ObjectDetection.MachineLearning
{
    public class TrainContext
    {
        public Matrix<float> FeatureData { get; }

        public Matrix<int> LabelData { get; }

        public TrainContext(Matrix<float> features, Matrix<int> labels)
        {
            FeatureData = features;
            LabelData = labels;
        }
    }

    class SVMTrain : HogBase
    {
        /// <summary>
        /// Train svm
        /// </summary>
        /// <param name="positiveFolder">positive folder.</param>
        /// <param name="negtiveFolder">negtive folder.</param>
        /// <param name="xmlPath">Save path</param>
        public void Train(string positiveFolder, string negtiveFolder, string xmlPath)
        {
            var positiveContext = LoadImage(positiveFolder, 1);
            var negtiveContext = LoadImage(negtiveFolder, -1);
            var trainFeatures = positiveContext.FeatureData.ConcateVertical(negtiveContext.FeatureData);
            var labels = positiveContext.LabelData.ConcateVertical(negtiveContext.LabelData);
            TrainSample(xmlPath, trainFeatures, labels);
        }

        private TrainContext LoadImage(string path, int tag)
        {
            var files = Directory.GetFiles(path);
            var features = new Matrix<float>(files.Length, (int)_hogDescriptor.DescriptorSize);
            var labels = new Matrix<int>(files.Length, 1);
            for(int i = 0; i < files.Length; ++i)
            {
                using (var image = new Image<Gray, byte>(files[i]))
                {
                    var feature = _hogDescriptor.Compute(image);
                    for (var j = 0; j < _hogDescriptor.DescriptorSize; ++j)
                    {
                        features[i, j] = feature[j];
                    }

                    labels[i, 0] = tag;
                }
            }

            return new TrainContext(features, labels);
        }

        private void TrainSample(string xmlPath, Matrix<float> featureData, Matrix<int> labels)
        {
            var svm = new SVM() { Type = SVM.SvmType.CSvc };
            svm.SetKernel(SVM.SvmKernelType.Linear);
            svm.C = 1;
            svm.TermCriteria = new MCvTermCriteria(10000, 0.0001);
            svm.Train(featureData, DataLayoutType.RowSample, labels);
            svm.Save(xmlPath);
        }
    }
}
