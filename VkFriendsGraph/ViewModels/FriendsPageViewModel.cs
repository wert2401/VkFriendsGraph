using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using VkFriendsGraph.BussinesLogic.Vk;

namespace VkFriendsGraph.ViewModels
{
    public class FriendsPageViewModel
    {
        public ObservableCollection<Person> People { get; set; }

        public FriendsPageViewModel(object friendsList)
        {
            foreach (var person in friendsList as List<Person>)
            {
                People.Add(person);
            }
        }
    }
}
