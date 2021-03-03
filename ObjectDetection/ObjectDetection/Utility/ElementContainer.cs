using ObjectDetection.Interface;
using System;
using System.Collections.Generic;

namespace ObjectDetection.Utility
{
    class ElementContainer
    {
        private Dictionary<Type, IElement> _dic;

        private static ElementContainer _instance;

        public static ElementContainer Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new ElementContainer();
                }

                return _instance;
            }
        }

        private ElementContainer() 
        {
            _dic = new Dictionary<Type, IElement>();
        }

        public void RegistElement<T>(IElement element) where T : IElement 
        {
            var type = typeof(T);
            if (!_dic.ContainsKey(type))
            {
                _dic.Add(type, element);
            }
        }

        public T GetElement<T>() where T : class
        {
            var type = typeof(T);
            if (_dic.ContainsKey(type))
            {
                return _dic[type] as T;
            }

            return default(T);
        }
    }
}
