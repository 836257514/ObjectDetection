using System;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;

namespace ObjectDetection.ViewModel
{
    public class NotificationObject : INotifyPropertyChanged
    {
        /// <summary>
        /// Property change handler
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        public virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }

        public void OnPropertyChanged<T>(Expression<Func<T>> propertyExpression)
        {
            var propertyName = GetPropertyName(propertyExpression);
            OnPropertyChanged(propertyName);
        }

        public void OnPropertyChanged(params Expression<Func<object>>[] propertyExpressions)
        {
            foreach (var expression in propertyExpressions)
            {
                OnPropertyChanged<object>(expression);
            }
        }

        private static string GetPropertyName<T>(Expression<Func<T>> propertyExpression)
        {
            if (propertyExpression == null)
            {
                throw new ArgumentNullException("propertyExpression");
            }

            var memberExpression = propertyExpression.Body as MemberExpression;
            if (memberExpression != null)
            {
                return memberExpression.Member.Name;
            }

            var unaryExpression = propertyExpression.Body as UnaryExpression;
            if (unaryExpression != null)
            {
                var memberInfo = (MemberExpression)unaryExpression.Operand;
                return memberInfo.Member.Name;
            }

            return string.Empty;
        }
    }
}
