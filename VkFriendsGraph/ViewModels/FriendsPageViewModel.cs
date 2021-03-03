using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using VkFriendsGraph.BussinesLogic.Vk;

namespace VkFriendsGraph.ViewModels
{
    public class FriendsPageViewModel
    {
        public ObservableCollection<Person> People { get; set; } = new ObservableCollection<Person>();

        private List<Person> people;

        public FriendsPageViewModel(object friendsList)
        {
            people = friendsList as List<Person>;
        }

        public void UpdateFriends()
        {
            foreach (var person in people)
            {
                People.Add(person);
            }
        }
    }
}
