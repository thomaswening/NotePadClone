using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

using Microsoft.Win32;

using NotePadClone.Utilities;
using NotePadClone.ViewModels;

using WpfEssentials.Base;

namespace NotePadClone
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// inherits from WindowChromeCommands
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            this.SubscribeToWindowEvents((WindowVm)DataContext);

            var vm = (MainWindowVm)DataContext;
            vm.OpenFileFunc = ShowOpenFileDialog;
            vm.SaveFileFunc = OpenSaveFileDialog;
        }

        private string? OpenSaveFileDialog()
        {
            var saveFileDialog = new SaveFileDialog();

            if (saveFileDialog.ShowDialog() == true)
            {
                return saveFileDialog.FileName;
            }

            return null;
        }

        private string? ShowOpenFileDialog()
        {
            var openFileDialog = new OpenFileDialog();

            if (openFileDialog.ShowDialog() == true)
            {
                return openFileDialog.FileName;
            }

            return null;
        }
    }
}