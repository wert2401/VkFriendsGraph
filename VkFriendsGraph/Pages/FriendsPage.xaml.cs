using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using VkFriendsGraph.BussinesLogic.Vk;
using VkFriendsGraph.Helpers;
using VkFriendsGraph.ViewModels;

namespace VkFriendsGraph.Pages
{
    /// <summary>
    /// Логика взаимодействия для FriendsPage.xaml
    /// </summary>
    public partial class FriendsPage : Page
    {
        private int indent = 60;
        private Thickness lastCoord = new Thickness(0);
        public FriendsPage(object friendsList)
        {
            InitializeComponent();
            MovingHelper.Grid = MainGrid;
            var pageVm = new FriendsPageViewModel(friendsList);
            pageVm.People.CollectionChanged += People_CollectionChanged;
            DataContext = pageVm;
            pageVm.UpdateFriends();
        }

        private void People_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == System.Collections.Specialized.NotifyCollectionChangedAction.Add)
            {
                foreach (Person person in e.NewItems)
                {
                    if (ContentGrid.Children.Count > 0)
                    {
                        FriendNode lastElement = (FriendNode)ContentGrid.Children[ContentGrid.Children.Count - 1];
                        lastCoord = lastElement.Margin;
                    }

                    FriendNode fn = new FriendNode();
                    fn.ImageUrl = person.PhotoUrl;
                    fn.HorizontalAlignment = HorizontalAlignment.Left;
                    fn.VerticalAlignment = VerticalAlignment.Top;
                    fn.Width = 50;
                    fn.Height = 50;
                    fn.Margin = new Thickness(lastCoord.Left + indent, 0, 0, 0);
                    ContentGrid.Children.Add(fn);
                }
            }
        }
    }
}
