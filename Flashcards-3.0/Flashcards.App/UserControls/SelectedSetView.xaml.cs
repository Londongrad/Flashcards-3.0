using System.Windows.Controls;
using System.Windows.Input;

namespace Flashcards.App.UserControls
{
    /// <summary>
    /// Логика взаимодействия для SelectedSetView.xaml
    /// </summary>
    public partial class SelectedSetView : UserControl
    {
        public SelectedSetView()
        {
            InitializeComponent();
            listWords.SelectedIndex++;
        }

        #region [ Вариант с CollectionViewSource ]

        //private void OnBackCanExecute(object sender, CanExecuteRoutedEventArgs e)
        //{
        //    Selector collectionView = (Selector)e.Parameter;
        //    e.CanExecute = collectionView is not null && collectionView.CurrentPosition > 0;
        //}

        //private void OnForwardCanExecute(object sender, CanExecuteRoutedEventArgs e)
        //{
        //    ICollectionView collectionView = (ICollectionView)e.Parameter;
        //    e.CanExecute = collectionView is not null && collectionView.CurrentPosition < collectionView.Cast<object>().Count() - 1;
        //}

        //private void OnBackExecute(object sender, ExecutedRoutedEventArgs e)
        //{
        //    ICollectionView collectionView = (ICollectionView)e.Parameter;
        //    collectionView.MoveCurrentToPrevious();
        //}

        //private void OnForwardExecute(object sender, ExecutedRoutedEventArgs e)
        //{
        //    ICollectionView collectionView = (ICollectionView)e.Parameter;
        //    collectionView.MoveCurrentToNext();
        //}

        #endregion [ Вариант с CollectionViewSource ]

        #region [ Вариант с Hidden List Box ]

        private void OnBackCanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = listWords.SelectedIndex > 0;
        }

        private void OnForwardCanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = listWords.SelectedIndex < listWords.Items.Count - 1;
        }

        private void OnBackExecute(object sender, ExecutedRoutedEventArgs e)
        {
            listWords.SelectedIndex--;
        }

        private void OnForwardExecute(object sender, ExecutedRoutedEventArgs e)
        {
            listWords.SelectedIndex++;
        }

        #endregion [ Вариант с Hidden List Box ]
    }
}