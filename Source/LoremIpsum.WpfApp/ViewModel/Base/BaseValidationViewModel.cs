using System;
using System.Collections;
using System.ComponentModel;

namespace LoremIpsum.WpfApp
{
    /// <summary>
    /// a base ViewModel that fires Property Changed events and Data Error Change events as needed
    /// </summary>
    /// <typeparam name="T">The type encapsulating the ViewModel</typeparam>
    public class BaseValidationViewModel<T> : BaseViewModel, IDataErrorInfo, INotifyDataErrorInfo
        where T : BaseViewModel

    {
        readonly ValidationTemplate<T> validationTemplate;

        /// <summary>
        /// Default constructor
        /// </summary>
        public BaseValidationViewModel()
        {
            var target = (this as T);
            validationTemplate = new ValidationTemplate<T>(target);
        }


        #region IDataErrorInfo Implementation

        ///<inheritdoc/>
        public string Error => validationTemplate.Error;

        ///<inheritdoc/>
        public string this[string propertyName] => validationTemplate[propertyName];

        #endregion

        #region INotifyDataErrorInfo

        ///<inheritdoc/>
        public IEnumerable GetErrors(string propertyName) =>
            validationTemplate.GetErrors(propertyName);

        ///<inheritdoc/>
        public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged
        {
            add => validationTemplate.ErrorsChanged += value;
            remove => validationTemplate.ErrorsChanged -= value;
        }

        ///<inheritdoc/>
        public bool HasErrors => validationTemplate.HasErrors;

        #endregion
    }
}
