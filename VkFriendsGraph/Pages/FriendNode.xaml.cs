using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using VkFriendsGraph.ViewModels;

namespace VkFriendsGraph.Pages
{
    /// <summary>
    /// Логика взаимодействия для FriendNode.xaml
    /// </summary>
    public partial class FriendNode : UserControl
    {
        public string ImageUrl
        {
            get { return (string)GetValue(ImageUrlProperty); }
            set { SetValue(ImageUrlProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ImageUrl.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ImageUrlProperty =
            DependencyProperty.Register("ImageUrl", typeof(string), typeof(FriendNode), new PropertyMetadata("https://sun4-15.userapi.com/s/v1/if1/rNS4wd4pzI-Szx8pOqcKnoXluFcC66SaekEGVi-6DcgtO6ie8ItEhFgkW9HIXuch33haOchp.jpg?size=200x0&quality=96&crop=32,221,1113,1567&ava=1"));

        public FriendNode()
        {
            InitializeComponent();
        }
    }
}
