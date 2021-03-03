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
using System.Windows.Navigation;
using System.Windows.Shapes;
using VkFriendsGraph.Helpers;
using VkFriendsGraph.ViewModels;

namespace VkFriendsGraph
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new MainWindowViewModel(Frame);
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Left)
            {
                MovingHelper.Move(MoveDirection.Left);
            }

            if (e.Key == Key.Right)
            {
                MovingHelper.Move(MoveDirection.Right);
            }

            if (e.Key == Key.Up)
            {
                MovingHelper.Move(MoveDirection.Up);
            }

            if (e.Key == Key.Down)
            {
                MovingHelper.Move(MoveDirection.Down);
            }
        }
    }
}
