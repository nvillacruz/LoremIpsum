using Dna;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace LoremIpsum.WpfApp
{
    /// <summary>
    /// The base control for all pages to gain base functionality
    /// </summary>
    public class BaseUserControl : UserControl
    {
        #region Private Member

        /// <summary>
        /// The View Model associated with this control
        /// </summary>
        private object mViewModel;

        #endregion

        #region Public Properties

        /// <summary>
        /// The animation the play when the control is first loaded
        /// </summary>
        public PageAnimation PageLoadAnimation { get; set; } 

        /// <summary>
        /// The animation the play when the control is unloaded
        /// </summary>
        public PageAnimation PageUnloadAnimation { get; set; } = PageAnimation.SlideAndFadeOutToLeft;

        /// <summary>
        /// The time any slide animation takes to complete
        /// </summary>
        public float SlideSeconds { get; set; } = 0.1f;

        /// <summary>
        /// A flag to indicate if this control should animate out on load.
        /// Useful for when we are moving the control to another frame
        /// </summary>
        public bool ShouldAnimateOut { get; set; }

        /// <summary>
        /// The View Model associated with this control
        /// </summary>
        public object ViewModelObject
        {
            get => mViewModel;
            set
            {
                // If nothing has changed, return
                if (mViewModel == value)
                    return;

                // Update the value
                mViewModel = value;

                // Fire the view model changed method
                OnViewModelChanged();

                // Set the data context for this control
                DataContext = mViewModel;
            }
        }

        #endregion

        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        public BaseUserControl()
        {
            // Don't bother animating in design time
            if (DesignerProperties.GetIsInDesignMode(this))
                return;

            // If we are animating in, hide to begin with
            if (PageLoadAnimation != PageAnimation.None)
                Visibility = Visibility.Collapsed;

            // Listen out for the control loading
            Loaded += BasePage_LoadedAsync;
        }

        #endregion

        #region Animation Load / Unload

        /// <summary>
        /// Once the control is loaded, perform any required animation
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void BasePage_LoadedAsync(object sender, RoutedEventArgs e)
        {
            // If we are setup to animate out on load
            if (ShouldAnimateOut)
                // Animate out the control
                await AnimateOutAsync();
            // Otherwise...
            else
                // Animate the control in
                await AnimateInAsync();
        }

        /// <summary>
        /// Animates the control in
        /// </summary>
        /// <returns></returns>
        public async Task AnimateInAsync()
        {
            // Make sure we have something to do
            if (PageLoadAnimation == PageAnimation.None)
                return;

            switch (PageLoadAnimation)
            {
                case PageAnimation.SlideAndFadeInFromRight:

                    // Start the animation
                    await this.SlideAndFadeInAsync(AnimationSlideInDirection.Right, false, SlideSeconds, size: (int)Application.Current.MainWindow.Width);

                    break;
            }
        }

        /// <summary>
        /// Animates the control out
        /// </summary>
        /// <returns></returns>
        public async Task AnimateOutAsync()
        {
            // Make sure we have something to do
            if (PageUnloadAnimation == PageAnimation.None)
                return;

            switch (PageUnloadAnimation)
            {
                case PageAnimation.SlideAndFadeOutToLeft:

                    // Start the animation
                    await this.SlideAndFadeOutAsync(AnimationSlideInDirection.Left, SlideSeconds);

                    break;
            }
        }

        #endregion

        /// <summary>
        /// Fired when the view model changes
        /// </summary>
        protected virtual void OnViewModelChanged()
        {

        }
    }

    /// <summary>
    /// A base control with added ViewModel support
    /// </summary>
    public class BaseUserControl<VM> : BaseUserControl
        where VM : BaseViewModel, new()
    {
        #region Public Properties

        /// <summary>
        /// The view model associated with this control
        /// </summary>
        public VM ViewModel
        {
            get => (VM)ViewModelObject;
            set => ViewModelObject = value;
        }

        #endregion

        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        public BaseUserControl() : base()
        {
            // If in design time mode...
            if (DesignerProperties.GetIsInDesignMode(this))
                // Just use a new instance of the VM
                ViewModel = new VM();
            else
                // Create a default view model
                ViewModel = Framework.Service<VM>() ?? new VM();
        }

        /// <summary>
        /// Constructor with specific view model
        /// </summary>
        /// <param name="specificViewModel">The specific view model to use, if any</param>
        public BaseUserControl(VM specificViewModel = null) : base()
        {
            // Set specific view model
            if (specificViewModel != null)
                ViewModel = specificViewModel;
            else
            {
                // If in design time mode...
                if (DesignerProperties.GetIsInDesignMode(this))
                    // Just use a new instance of the VM
                    ViewModel = new VM();
                else
                {
                    // Create a default view model
                    ViewModel = Framework.Service<VM>() ?? new VM();
                }
            }
        }

        #endregion
    }
}
