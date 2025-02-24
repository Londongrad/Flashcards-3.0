using System.ComponentModel;
using System.Printing;
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
        }

        private void OnBackCanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            ICollectionView collectionView = (ICollectionView)e.Parameter;
            e.CanExecute = collectionView is not null && collectionView.CurrentPosition > 0;
        }

        private void OnForwardCanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            ICollectionView collectionView = (ICollectionView)e.Parameter;
            e.CanExecute = collectionView is not null && collectionView.CurrentPosition < collectionView.Cast<object>().Count() - 1;
        }

        private void OnBackExecute(object sender, ExecutedRoutedEventArgs e)
        {
            ICollectionView collectionView = (ICollectionView)e.Parameter;
            collectionView.MoveCurrentToPrevious();
        }

        private void OnForwardExecute(object sender, ExecutedRoutedEventArgs e)
        {
            ICollectionView collectionView = (ICollectionView)e.Parameter;
            collectionView.MoveCurrentToNext();
        }
    }
}