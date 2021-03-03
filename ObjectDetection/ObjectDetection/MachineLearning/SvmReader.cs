using Emgu.CV;
using System.IO;
using System.Text;
using System.Xml;

namespace ObjectDetection.MachineLearning
{
    public class SvmContext
    {
        public Mat SupportVector { get; }

        public float Rho { get; }

        public Matrix<float> Alpha { get; }

        public SvmContext(Mat sv, float rho, Matrix<float> alpha)
        {
            SupportVector = sv;
            Rho = rho;
            Alpha = alpha;
        }

        public float[] GetSvmDescriptor()
        {
            var svMatrix = new Matrix<float>(SupportVector.Rows, SupportVector.Cols, SupportVector.DataPointer);
            Matrix<float> tempMatrix = -1 * svMatrix * Alpha;
            var descriptorLength = SupportVector.Cols + 1;
            var descriptor = new float[descriptorLength];
            for (var i = 0; i < descriptorLength; ++i)
            {
                descriptor[i] = tempMatrix[0, i];
            }

            descriptor[descriptorLength - 1] = Rho;
            return descriptor;
        }
    }

    class SvmReader
    {
        private float _rho;
        private int _svCount;
        private string _alphaStr;

        /// <summary>
        /// Read xml file 
        /// </summary>
        /// <param name="supportVectorMat">support vector mat</param>
        /// <param name="xmlPath">xml path</param>
        /// <returns>SvmContext</returns>
        public SvmContext Read(Mat supportVectorMat, string xmlPath)
        {
            var doc = new XmlDocument();
            doc.Load(xmlPath);
            XmlNode nodes = doc.DocumentElement;
            SetRho(nodes);
            SetAlphaStr(nodes);
            SetSvCount(nodes);
            var alphaMatrix = GetAlpha();

            return new SvmContext(supportVectorMat, _rho, alphaMatrix);
        }

        private void SetRho(XmlNode nodes)
        {
            if (nodes.HasChildNodes)
            {
                foreach (XmlNode node in nodes.ChildNodes)
                {
                    if (nodes.Name == "rho")
                    {
                        _rho = float.Parse(nodes.InnerText);
                        return;
                    }
                    SetRho(node);
                }
            }
        }

        private void SetAlphaStr(XmlNode nodes)
        {
            if (nodes.HasChildNodes)
            {
                foreach (XmlNode node in nodes.ChildNodes)
                {
                    if (nodes.Name == "alpha")
                    {
                        _alphaStr = nodes.InnerText;
                        return;
                    }
                    SetAlphaStr(node);
                }
            }
        }

        private void SetSvCount(XmlNode nodes)
        {
            if (nodes.HasChildNodes)
            {
                foreach (XmlNode node in nodes.ChildNodes)
                {
                    if (nodes.Name == "sv_count")
                    {
                        _svCount = int.Parse(nodes.InnerText);
                        return;
                    }
                    SetSvCount(node);
                }
            }
        }

        private Matrix<float> GetAlpha()
        {
            var array = Encoding.ASCII.GetBytes(_alphaStr);
            var alpha = new Matrix<float>(1, _svCount);
            using (var stream = new MemoryStream(array))
            {
                using (var streamReader = new StreamReader(stream))
                {
                    streamReader.ReadLine();
                    var i = 0;
                    while (i < _svCount)
                    {
                        var tmp = streamReader.ReadLine();
                        if (string.IsNullOrEmpty(tmp))
                            continue;

                        var tmp2 = tmp.Split(' ');
                        foreach (var ele in tmp2)
                        {
                            if (!string.IsNullOrEmpty(ele))
                            {
                                alpha[0, i] = float.Parse(ele);
                                i++;
                            }
                        }
                    }
                }
            }
            return alpha;
        }

    }
}
