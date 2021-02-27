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
        private List<Person> list { get; set; }
        public RelayCommand GetFriendsCommand { get; private set; }

        readonly VkLogic vk;

        public SearchPageViewModel()
        {
            vk = new VkLogic();
            GetFriendsCommand = new RelayCommand(getFriends);
        }

        private async void getFriends(object adress)
        {
            List<Person> list = await vk.GetPersonFriends((string)adress);
            //MessageBox.Show(list.Count.ToString());
        }
    }
}
