using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using System.Collections.ObjectModel;
using VkFriendsGraph.BussinesLogic.Vk;
using System.Threading.Tasks;
using System.Windows;
using System.ComponentModel;
using System.Windows.Controls;
using VkFriendsGraph.Pages;

namespace VkFriendsGraph.ViewModels
{
    public class MainWindowViewModel
    {
        public MainWindowViewModel(Frame frame)
        {
            NavigationHelper.MainFrame = frame;
            NavigationHelper.Navigate(Pages.Pages.SearchPage);
        }
    }
}
