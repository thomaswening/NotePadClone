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
        readonly IWindowService? _windowService;
        readonly MainWindowVm? _viewModel;

        public MainWindow()
        {
            InitializeComponent();
        }

        public MainWindow(MainWindowVm viewModel, IWindowService windowService) : this()
        {
            _windowService = windowService;
            _viewModel = viewModel;
            DataContext = _viewModel;
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            if (_viewModel is null) throw new InvalidOperationException("View model is null.");
            if (_windowService is null) throw new InvalidOperationException("Window service is null.");

            _windowService.SubscribeToWindowEvents(this);
            _viewModel.SwitchThemeAction = () => _windowService.SwitchWindowTheme(this);
            _viewModel.OpenFileSelectionDialogHandler = () => OpenFileDialog(new OpenFileDialog());
            _viewModel.OpenSaveFileDialogHandler = () => OpenFileDialog(new SaveFileDialog());
        }

        private static string? OpenFileDialog(FileDialog dialog)
        {
            return dialog.ShowDialog() == true ? dialog.FileName : null;
        }
    }
}