using Dna;
using FluentValidation;
using FluentValidation.Results;
using System;
using System.Collections;
using System.Collections.Concurrent;
using System.ComponentModel;
using System.Linq;

namespace LoremIpsum.WpfApp
{
    /// <summary>
    /// General Template for ViewModels validation
    /// </summary>
    public class ValidationTemplate<T> : IDataErrorInfo, INotifyDataErrorInfo
        where T: INotifyPropertyChanged
    {
        readonly INotifyPropertyChanged target;
        readonly IValidator validator;
        ValidationResult validationResult;

        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="target"></param>
        public ValidationTemplate(T target)
        {
            this.target = target;

            var modelType = typeof(T);

            validator = Framework.Service<IValidator<T>>();

            validationResult = validator.Validate(target);


            target.PropertyChanged -= Validate;
            target.PropertyChanged += Validate;
        }

        #endregion

        private void Validate(object sender, PropertyChangedEventArgs e)
        {
            validationResult = validator.Validate(target);
            foreach (var error in validationResult.Errors)
            {
                RaiseErrorsChanged(error.PropertyName);
            }
        }
        private void RaiseErrorsChanged(string propertyName)
        {
            ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(propertyName));
        }

        #region IDataErrorInfo Implementation

        ///<inheritdoc/>
        public string Error
        {
            get
            {
                var strings = validationResult.Errors.Select(x => x.ErrorMessage)
                                              .ToArray();
                return string.Join(Environment.NewLine, strings);
            }
        }

        ///<inheritdoc/>
        public string this[string propertyName]
        {
            get
            {
                var strings = validationResult.Errors.Where(x => x.PropertyName == propertyName)
                                              .Select(x => x.ErrorMessage)
                                              .ToArray();
                return string.Join(Environment.NewLine, strings);
            }
        }

        #endregion

        #region INotifyDataErrorInfo

        ///<inheritdoc/>
        public IEnumerable GetErrors(string propertyName) =>
            validationResult.Errors
                .Where(x => x.PropertyName == propertyName)
                .Select(x => x.ErrorMessage);

        ///<inheritdoc/>
        public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;

        ///<inheritdoc/>
        public bool HasErrors => validationResult.Errors.Count > 0;

        #endregion
    }

    /// <summary>
    /// Validation Factory
    /// </summary>
    public static class ValidationFactory
    {
        static readonly ConcurrentDictionary<RuntimeTypeHandle, IValidator> Validators = new ConcurrentDictionary<RuntimeTypeHandle, IValidator>();

        ///<inheritdoc/>
        public static IValidator<T> GetValidator<T>()
            where T : INotifyPropertyChanged
        {
            var modelType = typeof(T);
            var modelTypeHandle = modelType.TypeHandle;
            if (!Validators.TryGetValue(modelTypeHandle, out var validator))
            {
                var typeName = $"{modelType.Namespace}.{modelType.Name}Validator";
                var type = modelType.Assembly.GetType(typeName, true);
                Validators[modelTypeHandle] = validator = (IValidator)Activator.CreateInstance(type);
            }

            return (IValidator<T>)validator;
        }
    }
}
