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
        public Action<(Person, List<Person>)> PeopleUpdated { get; set; }

        private List<Person> people;
        readonly VkLogic vk;


        public FriendsPageViewModel()
        {
            vk = new VkLogic();
        }

        public async void GetFriends(string address)
        {
            people = await vk.GetPersonFriends(address);
            Person p = await vk.GetPerson(address);
            PeopleUpdated?.Invoke((p, people));
        }
    }
}
