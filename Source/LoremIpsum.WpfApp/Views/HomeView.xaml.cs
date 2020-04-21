namespace LoremIpsum.WpfApp
{
    /// <summary>
    /// Interaction logic for HomePage.xaml
    /// </summary>
    public partial class HomeView : BaseUserControl<HomeViewModel>
    {
        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        public HomeView()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Constructor with specific view model
        /// </summary>
        public HomeView(HomeViewModel specificViewModel) : base(specificViewModel)
        {
            InitializeComponent();
        }

        #endregion

       
    }
}
