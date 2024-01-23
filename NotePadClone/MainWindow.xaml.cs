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

            MinimizeCommand = new DelegateCommand(_ => Minimize());
            MaximizeCommand = new DelegateCommand(_ => Maximize());
            RestoreCommand = new DelegateCommand(_ => Restore());
            CloseCommand = new DelegateCommand(_ => Close());
        }

        public DelegateCommand MinimizeCommand { get; private set; }
        public DelegateCommand MaximizeCommand { get; private set; }
        public DelegateCommand RestoreCommand { get; private set; }
        public DelegateCommand CloseCommand { get; private set; }

        private static void Minimize()
        {
            Application.Current.MainWindow!.WindowState = WindowState.Minimized;
        }

        private static void Maximize()
        {
            Application.Current.MainWindow!.WindowState = WindowState.Maximized;
        }

        private static void Restore()
        {
            Application.Current.MainWindow!.WindowState = WindowState.Normal;
        }

        private new static void Close()
        {
            Application.Current.MainWindow!.Close();
        }
    }
}