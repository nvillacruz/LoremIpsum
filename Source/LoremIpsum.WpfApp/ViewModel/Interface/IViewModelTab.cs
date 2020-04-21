namespace LoremIpsum.WpfApp
{
    /// <summary>
    /// Base Interface of an item tab
    /// </summary>
    public interface IViewModelTab
    {
        /// <summary>
        /// Tab identification
        /// </summary>
        string Id { get; }

        /// <summary>
        /// Indicates if the tab is selected
        /// </summary>
        bool IsSelected { get; set; }

        /// <summary>
        /// Label of the tab
        /// </summary>
        string Label { get; set; }

        /// <summary>
        /// Icon string of the tab
        /// </summary>
        string Icon { get; set; }

        /// <summary>
        /// Indicates if the icon should be visible
        /// </summary>
        bool ShowIcon { get; set; }

        /// <summary>
        /// ViewModel of the tab content
        /// </summary>
        object ContentViewModelObject { get; set; }
    }
}
