namespace LoremIpsum.WpfApp
{
    /// <summary>
    /// Interaction logic for EnterpriseSettingView.xaml
    /// </summary>
    public partial class EnterpriseSettingView : BaseUserControl<EnterpriseSettingsViewModel>
    {
        #region Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        public EnterpriseSettingView()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Constructor with specific view model
        /// </summary>
        public EnterpriseSettingView(EnterpriseSettingsViewModel specificViewModel) : base(specificViewModel)
        {
            InitializeComponent();
        } 

        #endregion
    }
}
