using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;

namespace Bookmarker.Application
{
    /// <summary>
    /// Interaction logic for BookmarkNameWindow.xaml
    /// </summary>
    public partial class BookmarkNameWindow : Window
    {
        #region Events

        public event EventHandler<string> Saved;

        #endregion

        #region Constructor

        public BookmarkNameWindow()
        {
            InitializeComponent();
        }

        #endregion

        #region Event Methods

        private void OnSave(string bookmarkName)
        { 
            if (this.Saved != null)
                Saved(this, bookmarkName);
        }

        #endregion
        
        #region UI Events

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            OnSave(txtBookmarkName.Text);
            this.Close();
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        #endregion
    }
}
