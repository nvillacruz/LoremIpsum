using LoremIpsum.Core;
using System.Security;

namespace LoremIpsum.WpfApp
{
    /// <summary>
    /// Interaction logic for RegisterPage.xaml
    /// </summary>
    public partial class RegisterView : BaseUserControl<RegisterViewModel>, IHavePassword
    {
        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        public RegisterView()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Constructor with specific view model
        /// </summary>
        public RegisterView(RegisterViewModel specificViewModel) : base(specificViewModel)
        {
            InitializeComponent();
        }

        #endregion


        /// <summary>
        /// The secure password for this resgiter page
        /// </summary>
        public SecureString SecurePassword => PasswordText.SecurePassword;
    }
}
