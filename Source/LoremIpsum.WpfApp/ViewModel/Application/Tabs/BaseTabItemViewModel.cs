using Dna;
using LoremIpsum.Core;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace LoremIpsum.WpfApp
{

    /// <summary>
    /// Base ViewModel for item tab
    /// </summary>
    public class BaseTabItemViewModel : BaseViewModel, IViewModelTab
    {
        private object mContentViewModelObject;

        #region Public Properties

        ///<inheritdoc/>
        public string Id { get; private set; }

        ///<inheritdoc/>
        public bool IsSelected { get;  set; }

        ///<inheritdoc/>
        public string Label { get; set; }

        ///<inheritdoc/>
        public string Icon { get; set; }

        ///<inheritdoc/>
        public bool ShowIcon { get; set; }

        ///<inheritdoc/>
        public object ContentViewModelObject
        {
            get => mContentViewModelObject;
            set
            {
                // If nothing has changed, return
                if (mContentViewModelObject == value)
                    return;

                // Update the value
                mContentViewModelObject = value;
            }
        }

        #endregion

        /// <summary>
        /// Default constructor
        /// </summary>
        public BaseTabItemViewModel()
        {
            Id = GuidHelper.GenerateNewGuid();
        }



    }

    /// <summary>
    /// Base View Model for tab with specific content ViewModel
    /// </summary>
    /// <typeparam name="T">content ViewModel</typeparam>
    public class BaseTabItemViewModel<T> : BaseTabItemViewModel
        where T : BaseViewModel, new()
    {
        /// <summary>
        /// Specific content ViewModel
        /// </summary>
        public T ContentViewModel
        {
            get => (T)ContentViewModelObject;
            set => ContentViewModelObject = value;
        }

        /// <summary>
        /// Default Constructor
        /// </summary>
        public BaseTabItemViewModel() : base()
        {
            // Create a default view model
            ContentViewModel = Framework.Service<T>() ?? new T();
        }
    }
  
}
