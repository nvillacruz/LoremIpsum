using System.Security;

namespace LoremIpsum.WpfApp
{
    /// <summary>
    /// Interaction logic for LoginPage.xaml
    /// </summary>
    public partial class LoginView : BaseUserControl<LoginViewModel>, IHavePassword
    {
        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        public LoginView()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Constructor with specific view model
        /// </summary>
        public LoginView(LoginViewModel specificViewModel) : base(specificViewModel)
        {
            InitializeComponent();
        }

        #endregion

        /// <summary>
        /// The secure password for this login page
        /// </summary>
        public SecureString SecurePassword => PasswordText.SecurePassword;
    }
}
