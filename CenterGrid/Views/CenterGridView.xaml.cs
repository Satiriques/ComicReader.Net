using ComicReader.Net.CenterGrid.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ComicReader.Net.CenterGrid.Views
{
    /// <summary>
    /// Interaction logic for CenterView.xaml
    /// </summary>
    public partial class CenterGridView : UserControl
    {
        public CenterGridView()
        {
            InitializeComponent();
        }

        private void TextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                if (sender is TextBox textBox)
                {
                    var binding = textBox.GetBindingExpression(TextBox.TextProperty);
                    binding.UpdateSource();
                }
            }
        }

        private void ItemsControl_CleanUpVirtualizedItem(object sender, CleanUpVirtualizedItemEventArgs e)
        {
            var books = e.Value as ObservableCollection<BookViewModel>;
            foreach (var book in books)
            {
                book.Cleanup();
            }
        }
    }
}