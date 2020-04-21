using System;
using System.Threading.Tasks;
using LoremIpsum.Core;
using System.Windows;

namespace LoremIpsum.WpfApp
{
    /// <summary>
    /// The applications implementation of the <see cref="IUIManager"/>
    /// </summary>
    public class UIManager : IUIManager
    {
        /// <inheritdoc/>
        public Task ShowMessage(MessageBoxDialogViewModel viewModel)
        {
            // Create a task completion source
            var tcs = new TaskCompletionSource<bool>();

            // Run on UI thread
            Application.Current.Dispatcher.Invoke(async () =>
            {
                try
                {
                    // Show the dialog box
                    await new DialogMessageBox().ShowDialog(viewModel);
                }
                finally
                {
                    // Flag we are done
                    tcs.SetResult(true);
                }
            });

            // Return the task once complete
            return tcs.Task;
        }
    }
}
