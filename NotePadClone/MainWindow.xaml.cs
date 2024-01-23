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
            var viewModel = (MainWindowVm)DataContext;

            viewModel.CloseWindowRequestedEvent += (s, e) => Close();
            viewModel.MinimizeWindowRequestedEvent += (s, e) => this.Minimize();
            viewModel.MaximizeWindowRequestedEvent += (s, e) => this.Maximize();
            viewModel.RestoreWindowRequestedEvent += (s, e) => this.Restore();
        }
    }
}