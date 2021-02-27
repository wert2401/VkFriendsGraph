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
        public Frame CurrentFrame { get; set; }

        public MainWindowViewModel(Frame frame)
        {
            CurrentFrame = frame;
            CurrentFrame.Content = new SearchPage();
        }

        public void Navigate(Pages.Pages page, object parametr)
        {
            switch (page)
            {
                case Pages.Pages.SearchPage:
                    CurrentFrame.Content = new SearchPage();
                    break;
                case Pages.Pages.FriendsPage:
                    CurrentFrame.Content = new FriendsPage(parametr);
                    break;
                default:
                    break;
            }
        }
    }
}
