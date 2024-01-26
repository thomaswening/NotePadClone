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

using MaterialDesignThemes.Wpf;

using Microsoft.Win32;

using NotePadClone.Services;
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
        IWindowService _windowService;
        MainWindowVm _viewModel;

        public MainWindow()
        {
            InitializeComponent();
            _windowService = new WindowService();
            _viewModel = new MainWindowVm(_windowService);
            DataContext = _viewModel;
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            _windowService.SubscribeToWindowEvents(this);
            _viewModel.SwitchThemeAction = () => _windowService.SwitchWindowTheme(this);
            _viewModel.OpenFileSelectionDialogHandler = () => OpenFileDialog(new OpenFileDialog());
            _viewModel.OpenSaveFileDialogHandler = () => OpenFileDialog(new SaveFileDialog());
        }

        private string? OpenFileDialog(FileDialog dialog)
        {
            return dialog.ShowDialog() == true ? dialog.FileName : null;
        }
    }
}