using System;
using System.Collections.Generic;
using System.Windows;
using System.Text;
using VkFriendsGraph.Pages;
using System.Windows.Shapes;
using System.Windows.Media.Animation;

namespace VkFriendsGraph.Helpers
{
    public static class Extensions
    {
        public static Point GetPoint(this FriendNode fn)
        {
            return new Point((int)fn.Margin.Left, (int)fn.Margin.Top);
        }

        public static void Move(this Line line, Point beginPosition, Point endPosition, double duration)
        {
            line.X1 = beginPosition.X;
            line.Y1 = beginPosition.Y;

            DoubleAnimation xAnimation = new DoubleAnimation(beginPosition.X, endPosition.X, new Duration(TimeSpan.FromSeconds(duration)));
            Storyboard.SetTarget(xAnimation, line);
            Storyboard.SetTargetProperty(xAnimation, new PropertyPath(Line.X2Property));

            DoubleAnimation yAnimation = new DoubleAnimation(beginPosition.Y, endPosition.Y, new Duration(TimeSpan.FromSeconds(duration)));
            Storyboard.SetTarget(yAnimation, line);
            Storyboard.SetTargetProperty(yAnimation, new PropertyPath(Line.Y2Property));

            Storyboard sb = new Storyboard();
            sb.Children.Add(xAnimation);
            sb.Children.Add(yAnimation);

            sb.Begin();
        }
    }
}
