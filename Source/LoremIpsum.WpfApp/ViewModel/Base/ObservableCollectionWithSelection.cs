using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace LoremIpsum.WpfApp
{
    /// <summary>
    /// A custom observable collection with selection 
    /// </summary>
    /// <typeparam name="T">Data type of the collection and selection</typeparam>
    public abstract class ObservableCollectionWithSelection<T> : BaseViewModel
    {
        private ObservableCollection<T> collection;
        private T selected;


        /// <summary>
        /// The collection of <typeparamref name="T"/>
        /// </summary>
        public ObservableCollection<T> Collection
        {
            get => collection;
            set
            {
                collection = value;
                if (value != null)
                    Selected = value.FirstOrDefault();

            }
        }

        /// <summary>
        /// Indicates if collection has items
        /// </summary>
        public bool HasItems => Collection?.Count > 0;

        /// <summary>
        /// The selected <typeparamref name="T"/> in the collection
        /// </summary>
        public T Selected
        {
            get => selected;
            set
            {
                selected = value;
                AfterSelectedAction?.Invoke(value);
            }
        }

        /// <summary>
        /// An action after the selection has change
        /// </summary>
        public Action<T> AfterSelectedAction { get; set; }

        /// <summary>
        /// Indicates if has selection
        /// </summary>
        public bool HasSelection => Selected != null;


        /// <summary>
        /// Adds a <typeparamref name="T"/> in the collection
        /// </summary>
        /// <param name="item">An instance of <typeparamref name="T"/></param>
        public void Add(T item)
        {
            Collection.Add(item);
            if (Selected == null)
                Selected = Collection.FirstOrDefault();
            else
                Selected = item;

            OnPropertyChanged(nameof(HasItems));
        }

        /// <summary>
        /// Removes a <typeparamref name="T"/> in the collection
        /// </summary>
        /// <param name="item">>An instance of <typeparamref name="T"/></param>
        public void Remove(T item)
        {
            Collection.Remove(item);

            if (Collection?.Count > 0 && Selected == null)
                Selected = Collection.FirstOrDefault();
            else if (Collection?.Count > 0 && item.Equals(selected))
                Selected = Collection.LastOrDefault();
            else if (!(Collection?.Count > 0))
                Selected = default(T);
              

            OnPropertyChanged(nameof(HasItems));
        }


        /// <summary>
        /// Clears the collection
        /// </summary>
        public void Clear()
        {
            Collection.Clear();
            Selected = default(T);

            OnPropertyChanged(nameof(HasItems));
        }


    }
}
