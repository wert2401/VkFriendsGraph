using System;
using System.Collections.Generic;
using System.Diagnostics;
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
        private string initAddress;

        public FriendsPage(object address)
        {
            InitializeComponent();

            //Initialize canvas that will be moving around
            MovingHelper.Canvas = MainCanvas;
            MainCanvas.Background = Brushes.Aqua;
            MainCanvas.Margin = new Thickness(SystemParameters.PrimaryScreenWidth / 2d, SystemParameters.PrimaryScreenHeight / 2d, 0, 0);

            var pageVm = new FriendsPageViewModel();
            pageVm.PeopleUpdated += OnElementsUpdated;
            pageVm.ErrorOcured += OnErrorOccured;
            DataContext = pageVm;
            initAddress = (string)address;

            Loaded += FriendsPage_Loaded;
        }

        private void OnElementsUpdated(List<Node<Person>> nodes)
        {
            MainCanvas.Children.Clear();

            Stopwatch sw = new Stopwatch();

            sw.Start();
            List<UIElement> graph = CreateGraph(nodes[0]);
            sw.Stop();
            tbCreatingTimer.Text = sw.ElapsedMilliseconds.ToString();

            sw.Restart();
            graph.ForEach((e) => { MainCanvas.Children.Add(e); });
            sw.Stop();
            tbRenderingTimer.Text = sw.ElapsedMilliseconds.ToString();
        }

        private void OnErrorOccured()
        {
            MessageBox.Show("Error occured, probably profile is closed");
        }

        private async Task<List<UIElement>> CreateGraphAsync(Node<Person> node)
        {
            return await Task.Run(() => CreateGraph(node));
        }

        private List<UIElement> CreateGraph(Node<Person> node, double animationDuration = 0)
        {
            List<UIElement> graph = new List<UIElement>();
            FriendNode rootNode = CreateFriendNode(node, new Point(0, 0));

            graph.Add(rootNode);

            List<UIElement> children = CreateChildren(rootNode, animationDuration);

            graph.AddRange(children);

            return graph;
        }

        private FriendNode CreateFriendNode(Node<Person> personNode, Point endPosition, Point beginPosition = new Point(), double duration = 0)
        {
            FriendNode fn = new FriendNode();
            fn.PersonNode = personNode;
            fn.SetZIndex(2);

            fn.MouseDown += FriendNode_MouseDown;
            if (duration <= 0)
            {
                fn.SetPosition(endPosition);
            }
            else
            {
                fn.Move(beginPosition, endPosition, duration);
            }

            return fn;
        }

        //Recursive method to create all children of node
        private List<UIElement> CreateChildren(FriendNode node, double duration)
        {
            List<UIElement> childrenElements = new List<UIElement>();
            List<Node<Person>> people = node.PersonNode.ChildrenNodes;
            double downOffset = 300;
            double betweenOffset = 60;

            Point rootNodePos = node.Position;
            double x = rootNodePos.X - ((people.Count - 1) * betweenOffset) / 2d;
            double y = rootNodePos.Y + downOffset;
            Point curPos = new Point(x, y);

            if (people == null)
            {
                return null;
            }

            foreach (var person in people)
            {
                Line line = CreateLine(rootNodePos, curPos, duration);
                FriendNode friendNode = CreateFriendNode(person, curPos, rootNodePos, duration);

                List<UIElement> elements = CreateChildren(friendNode, duration);

                curPos.X += betweenOffset;

                childrenElements.Add(line);
                childrenElements.Add(friendNode);
                childrenElements.AddRange(elements);
            }

            return childrenElements;
        }

        private Line CreateLine(Point beginPoint, Point endPoint, double duration = 0)
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
            return line;
        }

        private async void FriendNode_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (sender is FriendNode fn)
            {
                await (DataContext as FriendsPageViewModel).SearchFriendsByNodeAsync(fn.PersonNode);
            }
        }

        private async void FriendsPage_Loaded(object sender, RoutedEventArgs e)
        {
            await (DataContext as FriendsPageViewModel).SearchFriendsByAddressAsync(initAddress);
        }
    }
}
