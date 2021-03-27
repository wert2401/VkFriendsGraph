using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net;
using System.Text;
using System.Windows;
using VkFriendsGraph.BussinesLogic.Vk;

namespace VkFriendsGraph.ViewModels
{
    class SearchPageViewModel
    {
        public RelayCommand GetFriendsCommand { get; private set; }

        public SearchPageViewModel()
        {
            GetFriendsCommand = new RelayCommand(getFriends);
        }

        private void getFriends(object address)
        {
            NavigationHelper.Navigate(Pages.Pages.FriendsPage, address);
        }
    }
}
