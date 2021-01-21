using System;
using System.Windows;
using System.Windows.Controls;

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
            AddInputUserContorl();
            FocusTextBoxPlayer1Name();
        }

        private void FocusTextBoxPlayer1Name()
        {
            InputUserControl.Instance.TextBoxPlayer1Name.Focus();
            InputUserControl.Instance.TextBoxPlayer1Name.SelectAll();
        }

        private void AddInputUserContorl()
        {
            container.Children.Add(InputUserControl.Instance);
            Grid.SetColumn(InputUserControl.Instance, 1);
            Grid.SetRow(InputUserControl.Instance, 1);
        }
    }
}