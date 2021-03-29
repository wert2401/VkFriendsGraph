using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using VkFriendsGraph.BussinesLogic.Vk;
using VkFriendsGraph.Graph;
using VkFriendsGraph.Helpers;
using VkFriendsGraph.Pages;

namespace VkFriendsGraph.ViewModels
{
    public class FriendsPageViewModel
    {
        public Action<List<Node<Person>>> PeopleUpdated { get; set; }
        public Action ErrorOcured { get; set; }

        private List<Node<Person>> rootNodes = new List<Node<Person>>();
        readonly VkLogic vk;


        public FriendsPageViewModel()
        {
            vk = new VkLogic(false);
        }

        public async Task SearchFriendsByNodeAsync(Node<Person> node)
        {
            try
            {
                Node<Person> nodeToAdd = await GetPersonNodeAsync(node.MainObject.Id);
                Node<Person> lastRootNode = rootNodes[rootNodes.Count - 1];
                lastRootNode.ChildrenNodes.Clear();
                lastRootNode.ChildrenNodes.Add(nodeToAdd);
                rootNodes.Add(nodeToAdd);

                PeopleUpdated?.Invoke(rootNodes);
            }
            catch (Exception)
            {
                ErrorOcured?.Invoke();
            }
        }

        public async Task SearchFriendsByAddressAsync(string address)
        {
            try
            {
                rootNodes.Add(await GetPersonNodeAsync(address));

                PeopleUpdated?.Invoke(rootNodes);
            }
            catch (Exception)
            {
                ErrorOcured?.Invoke();
            }
        }

        private async Task<Node<Person>> GetPersonNodeAsync(string address)
        {
            List<Person> people = await vk.GetPersonFriendsAsync(address);
            Person p = await vk.GetPersonAsync(address);
            Node<Person> node = new Node<Person>(p, people);
            return node;
        }

        private async Task<Node<Person>> GetPersonNodeAsync(int address)
        {
            List<Person> people = await vk.GetPersonFriendsAsync(address);
            Person p = await vk.GetPersonAsync(address);
            Node<Person> node = new Node<Person>(p, people);
            return node;
        }
    }
}
