using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using VkFriendsGraph.BussinesLogic.Vk;
using VkFriendsGraph.Graph;
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
            //Initialize canvas that will be moving around
            MovingHelper.Canvas = MainCanvas;
            center = new Point(SystemParameters.PrimaryScreenWidth / 2d, SystemParameters.PrimaryScreenHeight / 2d);

            var pageVm = new FriendsPageViewModel();
            pageVm.PeopleUpdated += OnPeopleUpdated;
            pageVm.ErrorOcured += OnErrorOccured;
            DataContext = pageVm;

            pageVm.OnFriendsSearchByAddressAsync((string)address);
        }

        private void OnPeopleUpdated(Node<Person> node)
        {
            MainCanvas.Children.Clear();
            ShowFriendNode(node, center);
            ShowChildrenInLine(node, center, 2);
        }

        private void OnErrorOccured()
        {
            MessageBox.Show("Error occured, probably profile is closed");
        }

        private void ShowChildrenInCircle(Node<Person> node, Point centerPoint)
        {
            List<Node<Person>> people = node.ChildrenNodes;

            double alphaStep = 2 * Math.PI / people.Count;
            double alpha = 0;
            double offset = people.Count * 10;


            double x = centerPoint.X + offset * Math.Sin(alpha);
            double y = centerPoint.Y + offset * Math.Cos(alpha);

            Point currentPos = new Point(x, y);

            foreach (Node<Person> p in people)
            {

                ShowLineAndAnimate(centerPoint, currentPos);
                ShowFriendNode(p, currentPos);

                alpha += alphaStep;
                x = centerPoint.X + offset * Math.Sin(alpha);
                y = centerPoint.Y + offset * Math.Cos(alpha);
                currentPos.X = x;
                currentPos.Y = y;
            }
        }

        private FriendNode ShowFriendNode(Node<Person> personNode, Point position)
        {
            FriendNode fn = new FriendNode();
            fn.PersonNode = personNode;
            fn.SetZIndex(2);

            fn.SetPosition(position);

            fn.MouseDown += FriendNode_MouseDown;

            MainCanvas.Children.Add(fn);

            return fn;
        }

        private FriendNode ShowFriendNode(Node<Person> personNode, Point beginPosition, Point endPosition, double duration)
        {
            FriendNode fn = new FriendNode();
            fn.PersonNode = personNode;
            fn.SetZIndex(2);

            fn.MouseDown += FriendNode_MouseDown;
            fn.Move(beginPosition, endPosition, duration);

            MainCanvas.Children.Add(fn);

            return fn;
        }

        private void ShowChildrenInLine(Node<Person> node, Point centerPoint, double duration)
        {
            List<Node<Person>> people = node.ChildrenNodes;
            double downOffset = 300;
            double betweenOffset = 60;

            double x = centerPoint.X - ((people.Count - 1) * betweenOffset) / 2d;
            double y = centerPoint.Y + downOffset; 
            Point curPos = new Point(x, y);

            if (people == null)
            {
                return;
            }

            foreach (var person in people)
            {
                ShowLineAndAnimate(centerPoint, curPos, duration);
                FriendNode friendNode = ShowFriendNode(person, centerPoint, curPos, duration);

                //Do not showing children, need to fix
                //friendNode.Storyboard.Completed += (object sender, EventArgs args) => { ShowChildrenInLine(person, curPos, duration); };

                ShowChildrenInLine(person, curPos, duration);
                curPos.X += betweenOffset;

            }
        }

        private void ShowLineAndAnimate(Point beginPoint, Point endPoint, double duration = 0)
        {
            Line line = new Line();
            if (duration <= 0)
            {
                line.X1 = beginPoint.X;
                line.Y1 = beginPoint.Y;
                line.X2 = endPoint.X;
                line.Y2 = endPoint.Y;
            }
            else
            {
                line.Move(beginPoint, endPoint, duration);
            }
            line.Stroke = Brushes.Black;
            MainCanvas.Children.Add(line);
        }

        private async void FriendNode_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (sender is FriendNode fn)
            {
                await (DataContext as FriendsPageViewModel).OnFriendSearchByNodeAsync(fn.PersonNode);
            }
        }
    }
}
