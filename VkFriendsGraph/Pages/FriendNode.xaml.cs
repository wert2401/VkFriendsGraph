using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using VkFriendsGraph.BussinesLogic.Vk;
using VkFriendsGraph.Graph;
using VkFriendsGraph.ViewModels;

namespace VkFriendsGraph.Pages
{
    /// <summary>
    /// Логика взаимодействия для FriendNode.xaml
    /// </summary>
    public partial class FriendNode : UserControl
    {

        private Node<Person> personNode;
        public Storyboard Storyboard { get; private set; }
        public Node<Person> PersonNode { 
            get { return personNode; } 
            set
            {
                personNode = value;
                ImageUrl = personNode.MainObject.PhotoUrl;
            } 
        }

        public string ImageUrl
        {
            get { return (string)GetValue(ImageUrlProperty); }
            set { SetValue(ImageUrlProperty, value); }
        }

        public Point Position { get; private set; }

        // Using a DependencyProperty as the backing store for ImageUrl.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ImageUrlProperty =
            DependencyProperty.Register("ImageUrl", typeof(string), typeof(FriendNode), new PropertyMetadata("https://sun4-15.userapi.com/s/v1/if1/rNS4wd4pzI-Szx8pOqcKnoXluFcC66SaekEGVi-6DcgtO6ie8ItEhFgkW9HIXuch33haOchp.jpg?size=200x0&quality=96&crop=32,221,1113,1567&ava=1"));

        public FriendNode()
        {
            InitializeComponent();
            Storyboard = new Storyboard();
            HorizontalAlignment = HorizontalAlignment.Left;
            VerticalAlignment = VerticalAlignment.Top;
            Width = 50;
            Height = 50;
        }

        public void SetPosition(Point point)
        {
            Position = GetCenterPosition(point);
            Canvas.SetLeft(this, Position.X);
            Canvas.SetTop(this, Position.Y);
        }

        //Returns Point that is offsetted by widht and height, so element is in the center of Point
        private Point GetCenterPosition(Point point)
        {
            return new Point(point.X - Width / 2d, point.Y - Height / 2d);
        }

        public void Move(Point beginPosition, Point endPosition, double duration)
        {
            Storyboard.Children.Clear();

            beginPosition = GetCenterPosition(beginPosition);
            endPosition = GetCenterPosition(endPosition);

            DoubleAnimation xAnimation = new DoubleAnimation(beginPosition.X, endPosition.X, new Duration(TimeSpan.FromSeconds(duration)));
            Storyboard.SetTarget(xAnimation, this);
            Storyboard.SetTargetProperty(xAnimation, new PropertyPath(Canvas.LeftProperty));

            DoubleAnimation yAnimation = new DoubleAnimation(beginPosition.Y, endPosition.Y, new Duration(TimeSpan.FromSeconds(duration)));
            Storyboard.SetTarget(yAnimation, this);
            Storyboard.SetTargetProperty(yAnimation, new PropertyPath(Canvas.TopProperty));

            Storyboard.Children.Add(xAnimation);
            Storyboard.Children.Add(yAnimation);

            Storyboard.Begin();
        }

        public void SetZIndex(int position)
        {
            Panel.SetZIndex(this, position);
        }
    }
}
