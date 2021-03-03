using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows;
using VkFriendsGraph.BussinesLogic.Vk;

namespace VkFriendsGraph.ViewModels
{
    class SearchPageViewModel
    {
        public RelayCommand GetFriendsCommand { get; private set; }

        readonly VkLogic vk;

        public SearchPageViewModel()
        {
            vk = new VkLogic();
            GetFriendsCommand = new RelayCommand(getFriends);
        }

        private async void getFriends(object address)
        {
            List<Person> list = await vk.GetPersonFriends((string)address);
            NavigationHelper.MainFrame = null;
            NavigationHelper.Navigate(Pages.Pages.FriendsPage, list);
        }
    }
}
