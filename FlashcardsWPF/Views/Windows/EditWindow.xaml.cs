using System.Windows;
using System.Windows.Input;

namespace Flashcards.Views.Windows
{
    public partial class EditWindow : Window
    {
        public EditWindow()
        {
            InitializeComponent();
        }
        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }
    }
}
