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
        private Point center;
        public FriendsPage(object address)
        {
            InitializeComponent();
            MovingHelper.Canvas = MainCanvas;
            center = new Point(SystemParameters.PrimaryScreenWidth / 2d, SystemParameters.PrimaryScreenHeight / 2d - 60);

            var pageVm = new FriendsPageViewModel();
            pageVm.PeopleUpdated += OnPeopleUpdated;
            DataContext = pageVm;

            pageVm.GetFriends((string)address);
        }

        private void OnPeopleUpdated((Person, List<Person>) dict)
        {
            (Person person, List<Person> people) = dict;

            FriendNode centralFn = new FriendNode();
            centralFn.Width = 100;
            centralFn.Height = 100;
            centralFn.ImageUrl = person.PhotoUrl;
            centralFn.SetPosition(center);
            centralFn.SetZIndex(2);
            MainCanvas.Children.Add(centralFn);

            ShowPeople(people);
        }

        private void ShowPeople(List<Person> people)
        {
            double alphaStep = 2 * Math.PI / people.Count;
            double alpha = 0;
            double offset = people.Count * 10;

            double x = center.X + offset * Math.Sin(alpha);
            double y = center.Y + offset * Math.Cos(alpha);

            Point currentPos = new Point(x, y);

            foreach (Person p in people)
            {
                Line line = new Line();
                line.X1 = center.X ;
                line.Y1 = center.Y;
                line.X2 = x;
                line.Y2 = y;
                line.Stroke = Brushes.Black;

                FriendNode fn = new FriendNode();
                fn.ImageUrl = p.PhotoUrl;
                fn.SetZIndex(2);

                fn.SetPosition(currentPos);

                MainCanvas.Children.Add(line);
                MainCanvas.Children.Add(fn);

                alpha += alphaStep;
                x = center.X + offset * Math.Sin(alpha);
                y = center.Y + offset * Math.Cos(alpha);
                currentPos.X = x;
                currentPos.Y = y;
            }
        }
    }
}
