using System.Windows;

namespace NeutronGame
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            this.container.Children.Add(new InputUserControl()); // Create the sweaty getters and setters thing 
        }
    }
}
