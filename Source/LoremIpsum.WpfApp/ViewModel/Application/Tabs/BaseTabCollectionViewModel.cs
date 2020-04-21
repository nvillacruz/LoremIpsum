using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace LoremIpsum.WpfApp
{
    /// <summary>
    /// A custom collection 
    /// </summary>
    public class BaseTabCollectionViewModel : ObservableCollectionWithSelection<BaseTabItemViewModel>
    {
        /// <summary>
        /// Command to select
        /// </summary>
        public ICommand SelectCommand { get; set; }

        /// <summary>
        /// Command to close
        /// </summary>
        public ICommand CloseCommand { get; set; }

        /// <summary>
        /// Default constructor
        /// </summary>
        public BaseTabCollectionViewModel()
        {
            SelectCommand = new RelayParameterizedCommand(selected => Select(selected));
            CloseCommand = new RelayParameterizedCommand(selected => Close(selected));

            Collection = new ObservableCollection<BaseTabItemViewModel>();
            AfterSelectedAction = selected =>
            {
                if (selected == null)
                    return;

                selected.IsSelected = true;
                foreach (var item in Collection.Where(x => x.Id != selected.Id))
                {
                    item.IsSelected = false;
                }
            };
        }

        /// <summary>
        /// Constructor with collection
        /// </summary>
        /// <param name="collection"></param>
        public BaseTabCollectionViewModel(List<BaseTabItemViewModel> collection): this()
        {
            Collection = new ObservableCollection<BaseTabItemViewModel>(collection);

        }

        private void Select(object selected)
        {
            if (!(selected is BaseTabItemViewModel))
                return;

            Selected = (BaseTabItemViewModel)selected;
        }


        private void Close(object selected)
        {
            if (!(selected is BaseTabItemViewModel))
                return;

            var toRemove = Collection.FirstOrDefault(x => x.Id == ((BaseTabItemViewModel)selected).Id);

            Remove(toRemove);
        }


    }
}
